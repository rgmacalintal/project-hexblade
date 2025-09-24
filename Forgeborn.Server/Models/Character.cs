using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forgeborn.Server.Models
{
    public class Character
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Class { get; set; } = null!;
        [Required]
        public string Race { get; set; } = null!;
        [Required]
        public JsonContent Stats { get; set; } = null!;
        public JsonContent[] Inventory { get; set; } = [];
        public string Background { get; set; } = null!;
        public string Journal { get; set; } = null!;
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
        public int RulesetId { get; set; }
        [ForeignKey(nameof(RulesetId))]
        public Ruleset? Ruleset { get; set; }

    }
}
