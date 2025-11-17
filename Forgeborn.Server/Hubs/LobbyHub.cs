using Microsoft.AspNetCore.SignalR;
using Forgeborn.Server.Data;
using Forgeborn.Server.Models;
using System.Security.Cryptography;

namespace Forgeborn.Server.Hubs
{
    public class LobbyHub : Hub
    {
        private readonly ApplicationDbContext _context;
        private static readonly Dictionary<string, string> UserToLobby = new();
        private static readonly Dictionary<string, string> LobbyToHost = new();

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
                Name = $"{username}'s Lobby",
                CreatedOn = DateTime.UtcNow
            };

            _context.Lobbys.Add(lobby);
            await _context.SaveChangesAsync();

            LobbyToHost[code] = Context.ConnectionId;
            UserToLobby[Context.ConnectionId] = code;

            await Groups.AddToGroupAsync(Context.ConnectionId, code);
            await Clients.Caller.SendAsync("LobbyCreated", code, lobby.Name);
        }

        public async Task JoinLobby(string code, string username)
        {
            if (!LobbyToHost.ContainsKey(code))
            {
                await Clients.Caller.SendAsync("Error", "Lobby not found.");
                return;
            }

            await Groups.AddToGroupAsync(Context.ConnectionId, code);
            UserToLobby[Context.ConnectionId] = code;

            await Clients.Group(code).SendAsync("PlayerJoined", username);
        }

        public async Task SendMessage(string message)
        {
            if (UserToLobby.TryGetValue(Context.ConnectionId, out var code))
                await Clients.Group(code).SendAsync("ReceiveMessage", message);
        }

        public async Task LeaveLobby(string code, string username)
        {
            if (!UserToLobby.ContainsKey(Context.ConnectionId))
                return;

            UserToLobby.Remove(Context.ConnectionId);

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

            Console.WriteLine($"{username} left the lobby {code}");
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            if (UserToLobby.TryGetValue(Context.ConnectionId, out var code))
            {
                UserToLobby.Remove(Context.ConnectionId);

                if (LobbyToHost.TryGetValue(code, out var hostId) && hostId == Context.ConnectionId)
                {
                    LobbyToHost.Remove(code);
                    await Clients.Group(code).SendAsync("LobbyClosed", "Host disconnected.");
                }
                else
                {
                    await Clients.Group(code).SendAsync("PlayerLeft", Context.ConnectionId);
                }

                await Groups.RemoveFromGroupAsync(Context.ConnectionId, code);
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
