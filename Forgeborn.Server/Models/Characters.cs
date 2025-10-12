using Newtonsoft.Json.Linq;
using Project_Hexblade.Server.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Hexblade.Server.Models
{
    public class Characters
    {
        private static string emptyInventory = "{" +
            "'contents' : { }" +
        "}";
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Class { get; set; } = null!;
        [Required]
        public string Race { get; set; } = null!;
        [Required]
        public JObject Stats { get; set; } = null!;
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
