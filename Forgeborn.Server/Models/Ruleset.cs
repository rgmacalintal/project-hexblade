using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forgeborn.Server.Models
{
    public class Ruleset
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedOn { get; set; } = DateTime.Now;


        public int CharacterId { get; set; }
        [ForeignKey(nameof(CharacterId))]
        public Character? CreatedBy { get; set; }
    }
}
