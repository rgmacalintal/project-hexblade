using Forgeborn.Server.Data;
using Forgeborn.Server.Hubs;
using Forgeborn.Server.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

public class CharacterService
{
    private readonly ApplicationDbContext _context;
    private readonly IHubContext<CharacterHub> _hubContext;

    public CharacterService(ApplicationDbContext context, IHubContext<CharacterHub> hubContext)
    {
        _context = context;
        _hubContext = hubContext;
    }

    public async Task<int> ApplyDamageAsync(int characterId, string diceExpression)
    {
        var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == characterId);
        if (character == null) throw new Exception("Character not found.");

        int damage = DiceRoller.Roll(diceExpression);
        character.CurrentHP = Math.Max(0, character.CurrentHP - damage);
        await _context.SaveChangesAsync();

        // Notify connected clients
        await _hubContext.Clients.All.SendAsync("HPUpdated", character.Id, character.CurrentHP);
        return character.CurrentHP;
    }

    public async Task<int> HealAsync(int characterId, string diceExpression)
    {
        var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == characterId);
        if (character == null) throw new Exception("Character not found.");

        int heal = DiceRoller.Roll(diceExpression);
        character.CurrentHP = Math.Min(character.MaxHP, character.CurrentHP + heal);
        await _context.SaveChangesAsync();

        await _hubContext.Clients.All.SendAsync("HPUpdated", character.Id, character.CurrentHP);
        return character.CurrentHP;
    }
}
