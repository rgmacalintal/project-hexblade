using Forgeborn.Server.Data;
using Forgeborn.Server.Hubs;
using Forgeborn.Server.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Forgeborn.Server.Services
{
    public class CharacterService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<CharacterHub> _hub;

        public CharacterService(ApplicationDbContext context, IHubContext<CharacterHub> hub)
        {
            _context = context;
            _hub = hub;
        }

        // UTILITY
        private static readonly Random _rng = new Random();

        public int RollDice(int numDice, int numSides, int modifier = 0)
        {
            int result = 0;

            for (int i = 0; i < numDice; i++)
                result += _rng.Next(1, numSides + 1);

            return result + modifier;
        }

        // PERMISSION LOGIC
        private async Task<Players> GetPlayerContext(int userId, int characterId)
        {
            var player = await _context.Players
                .Include(p => p.Character)
                .FirstOrDefaultAsync(p => p.UserId == userId && p.CharacterId == characterId);

            if (player == null)
                throw new Exception("You are not associated with this character.");

            return player;
        }

        private async Task<Lobbys> GetLobbyForCharacter(int characterId)
        {
            var lobby = await _context.Lobbys
                .Include(l => l.Players)
                .FirstOrDefaultAsync(
                    l => l.Players.Any(p => p.CharacterId == characterId)
                );

            if (lobby == null)
                throw new Exception("Character is not part of any lobby.");

            return lobby;
        }

        // CORE HP LOGIC
        public async Task<int> DealDamage(int characterId, int amount, int userId)
        {
            var character = await _context.Characters.FindAsync(characterId)
                ?? throw new Exception("Character not found.");

            var player = await GetPlayerContext(userId, characterId);

            // Only DM can damage other players
            if (!player.IsHost)
                throw new Exception("Only the Dungeon Master can damage other characters.");

            character.CurrentHP -= amount;
            if (character.CurrentHP < 0)
                character.CurrentHP = 0;

            await _context.SaveChangesAsync();
            await BroadcastHP(characterId);

            return character.CurrentHP;
        }

        public async Task<int> Heal(int characterId, int amount, int userId)
        {
            var character = await _context.Characters.FindAsync(characterId)
                ?? throw new Exception("Character not found.");

            var player = await GetPlayerContext(userId, characterId);

            // Player can heal themselves OR DM can heal anyone
            if (!player.IsHost && character.UserId != userId)
                throw new Exception("You may only heal your own character.");

            character.CurrentHP += amount;
            if (character.CurrentHP > character.MaxHP)
                character.CurrentHP = character.MaxHP;

            await _context.SaveChangesAsync();
            await BroadcastHP(characterId);

            return character.CurrentHP;
        }

        public async Task<int> ApplyRollDamage(int characterId, int diceCount, int diceSides, int modifier, int userId)
        {
            int dmg = RollDice(diceCount, diceSides, modifier);
            return await DealDamage(characterId, dmg, userId);
        }

        public async Task<int> ApplyRollHeal(int characterId, int diceCount, int diceSides, int modifier, int userId)
        {
            int heal = RollDice(diceCount, diceSides, modifier);
            return await Heal(characterId, heal, userId);
        }

        // SIGNALR BROADCASTING
        private async Task BroadcastHP(int characterId)
        {
            var character = await _context.Characters
                .FirstOrDefaultAsync(c => c.Id == characterId);

            var lobby = await GetLobbyForCharacter(characterId);

            await _hub.Clients.Group($"{lobby.Name}")
                .SendAsync("HPUpdated", new
                {
                    characterId = character?.Id,
                    currentHP = character?.CurrentHP
                });
        }
    }
}
