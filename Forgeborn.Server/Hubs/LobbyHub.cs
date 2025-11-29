using Forgeborn.Server.Data;
using Forgeborn.Server.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Forgeborn.Server.Hubs
{
    public class LobbyHub : Hub
    {
        private readonly ApplicationDbContext _context;
        private static readonly Dictionary<string, string> UserToLobby = new();
        private static readonly Dictionary<string, string> LobbyToHost = new();
        private static readonly Dictionary<string, string> ConnectionToUsername = new();

        public LobbyHub(ApplicationDbContext context)
        {
            _context = context;
        }

        private string GenerateCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 4)
                .Select(s => s[RandomNumberGenerator.GetInt32(s.Length)]).ToArray());
        }

        public async Task CreateLobby(string username)
        {
            var code = GenerateCode();

            var lobby = new Lobbys
            {
                Name = code,
                CreatedOn = DateTime.UtcNow
            };
            _context.Lobbys.Add(lobby);
            await _context.SaveChangesAsync();

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                await Clients.Caller.SendAsync("JoinFailed", "User does not exist.");
                return;
            }

            var host = new Players
            {
                UserId = user.Id,
                LobbyId = lobby.Id,
                IsHost = true
            };
            _context.Players.Add(host);
            await _context.SaveChangesAsync();

            LobbyToHost[code] = Context.ConnectionId;
            UserToLobby[Context.ConnectionId] = code;
            ConnectionToUsername[Context.ConnectionId] = username;

            await Groups.AddToGroupAsync(Context.ConnectionId, code);
            await Clients.Caller.SendAsync("LobbyCreated", code);
            await BroadcastPlayerList(code);
        }

        public async Task JoinLobby(string code, string username)
        {
            var lobby = await _context.Lobbys.FirstOrDefaultAsync(l => l.Name == code);
            if (lobby == null)
            {
                await Clients.Caller.SendAsync("JoinFailed", "Lobby does not exist.");
                return;
            }

            var host = await _context.Players.Include(p => p.User).FirstOrDefaultAsync(p => p.LobbyId == lobby.Id && p.IsHost);
            if (host == null)
            {
                await Clients.Caller.SendAsync("JoinFailed", "This lobby has no host.");
                return;
            }

            if (!LobbyToHost.TryGetValue(code, out var hostConnectionId))
            {
                await Clients.Caller.SendAsync("JoinFailed", "Host is offline. Lobby is closed.");
                return;
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
            {
                await Clients.Caller.SendAsync("JoinFailed", "User does not exist.");
                return;
            }

            var player = new Players
            {
                UserId = user.Id,
                LobbyId = lobby.Id,
                IsHost = false
            };

            var defaultCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.UserId == user.Id && c.Name == "Default");

            if (defaultCharacter != null)
            {
                player.CharacterId = defaultCharacter.Id;
            }
            else
            {
                var autoCreated = new Characters
                {
                    UserId = user.Id,
                    Name = "Default",
                    Class = "None",
                    Race = "Human",
                    MaxHP = 10,
                    CurrentHP = 10,
                    Strength = 10,
                    Dexterity = 10,
                    Constitution = 10,
                    Intelligence = 10,
                    Wisdom = 10,
                    Charisma = 10,
                    Background = "",
                    Journal = ""
                };
                _context.Characters.Add(autoCreated);
                await _context.SaveChangesAsync();

                player.CharacterId = autoCreated.Id;
            }

            _context.Players.Add(player);
            await _context.SaveChangesAsync();

            await Groups.AddToGroupAsync(Context.ConnectionId, code);
            UserToLobby[Context.ConnectionId] = code;
            ConnectionToUsername[Context.ConnectionId] = username;

            await Clients.Group(code).SendAsync("PlayerJoined", username);
            await BroadcastPlayerList(code);
        }

        public async Task ReconnectHost(string code, string username)
        {
            var lobby = await _context.Lobbys.FirstOrDefaultAsync(l => l.Name == code);
            if (lobby == null) return;

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return;

            var isHost = await _context.Players.AnyAsync(p => p.UserId == user.Id && p.LobbyId == lobby.Id && p.IsHost);

            if (!isHost) return;

            LobbyToHost[code] = Context.ConnectionId;
            ConnectionToUsername[Context.ConnectionId] = username;
            await Groups.AddToGroupAsync(Context.ConnectionId, code);

            await Clients.Caller.SendAsync("HostReconnected", code);
            await BroadcastPlayerList(code);
        }

        public async Task<bool> IsPlayerInLobby(string code, string username)
        {
            var lobby = await _context.Lobbys.FirstOrDefaultAsync(l => l.Name == code);
            if (lobby == null) return false;

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return false;

            return await _context.Players.AnyAsync(p => p.LobbyId == lobby.Id && p.UserId == user.Id);
        }

        private async Task BroadcastPlayerList(string code)
        {
            var lobby = await _context.Lobbys.Include(l => l.Players).ThenInclude(p => p.User).FirstOrDefaultAsync(l =>l.Name == code);
            if (lobby == null) return;

            var players = lobby.Players
                .Select(p => new
                {
                    username = p.User?.Username ?? "(unknown)",
                    isHost = p.IsHost
                })
                .ToList();

            await Clients.Group(code).SendAsync("PlayerListUpdated", players);
        }

        public async Task LeaveLobby(string code, string username)
        {
            var lobby = await _context.Lobbys.FirstOrDefaultAsync(l => l.Name == code);
            if (lobby == null) return;

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user != null)
            {
                var player = await _context.Players.FirstOrDefaultAsync(p => p.UserId == user.Id && p.LobbyId == lobby.Id);
                if (player != null)
                {
                    _context.Players.Remove(player);
                    await _context.SaveChangesAsync();
                }
            }

            UserToLobby.Remove(Context.ConnectionId);
            ConnectionToUsername.Remove(Context.ConnectionId);
            await BroadcastPlayerList(code);

            if (LobbyToHost.TryGetValue(code, out var hostId) && hostId == Context.ConnectionId)
            {
                LobbyToHost.Remove(code);
                await Clients.Group(code).SendAsync("LobbyClosed", "Host left the lobby.");
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, code);
            }
            else
            {
                await Clients.Group(code).SendAsync("PlayerLeft", username);
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, code);
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (UserToLobby.TryGetValue(Context.ConnectionId, out var code))
            {
                UserToLobby.Remove(Context.ConnectionId);

                string username = "Unknown";

                if (ConnectionToUsername.TryGetValue(Context.ConnectionId, out var storedUsername))
                {
                    username = storedUsername;
                    ConnectionToUsername.Remove(Context.ConnectionId);
                }

                if (LobbyToHost.TryGetValue(code, out var hostId) && hostId == Context.ConnectionId)
                {
                    LobbyToHost.Remove(code);
                    await Clients.Group(code).SendAsync("LobbyClosed", "Host disconnected.");
                }
                else
                {
                    await Clients.Group(code).SendAsync("PlayerLeft", username);
                }

                await BroadcastPlayerList(code);
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
