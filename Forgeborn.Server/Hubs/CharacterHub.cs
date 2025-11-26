using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

namespace Forgeborn.Server.Hubs
{
    public class CharacterHub : Hub
    {
        public async Task SubscribeToCharacter(int characterId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"{characterId}");
        }

        public async Task UnsubscribeFromCharacter(int characterId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"{characterId}");
        }
    }
}
