using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forgeborn.Server.Models
{
    public class Lobbies
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int PlayerId { get; set; }
        [ForeignKey(nameof(PlayerId))]
        public Players Host { get; set; } = null!;
        
    }
}
