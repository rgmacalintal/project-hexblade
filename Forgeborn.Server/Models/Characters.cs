using Newtonsoft.Json.Linq;
using Forgeborn.Server.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forgeborn.Server.Models
{
    public class Characters
    {
        private static readonly string defaultInventory = "{ " +
            "'inventory' : { } }";

        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Class { get; set; } = null!;
        [Required]
        public string Race { get; set; } = null!;
        //[Required]
        //public JObject Stats { get; set; } = null!;
        [Required]
        [Range (0, 30)]
        public int Strength { get; set; } = 0;
        [Required]
        [Range(0, 30)]
        public int Dexterity { get; set; } = 0;
        [Required]
        [Range(0, 30)]
        public int Constitution { get; set; } = 0;
        [Required]
        [Range(0, 30)]
        public int Intelligence { get; set; } = 0;
        [Required]
        [Range(0, 30)]
        public int Wisdom { get; set; } = 0;
        [Required]
        [Range(0, 30)]
        public int Charisma { get; set; } = 0;
        public JObject Inventory { get; set; } = JObject.Parse(emptyInventory);
        public string Background { get; set; } = null!;
        public string Journal { get; set; } = null!;
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public Users? User { get; set; }
        public int RulesetId { get; set; }
        [ForeignKey(nameof(RulesetId))]
        public Rulesets? Ruleset { get; set; }

    }
}
