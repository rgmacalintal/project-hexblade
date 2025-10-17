using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forgeborn.Server.Models
{
    public class Players
    {
        public int Id { get; set; }
        public bool IsHost { get; set; }


        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public Users? User { get; set; }
        public int CharacterId { get; set; }
        [ForeignKey(nameof(CharacterId))]
        public Characters? Character { get; set; }
        public int LobbyId { get; set; }
        [ForeignKey(nameof(LobbyId))]
        public Lobbys? Lobby { get; set; }
    }
}
