using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forgeborn.Server.Models
{
    public class Lobby
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int PlayerId { get; set; }
        [ForeignKey(nameof(PlayerId))]
        public Player Host { get; set; } = null!;
        
    }
}
