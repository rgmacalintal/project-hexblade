using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forgeborn.Server.Models
{
    public class CharacterRuleset
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime AssignedDate { get; set; } = DateTime.Now;

        public int CharacterId { get; set; }
        [ForeignKey(nameof(CharacterId))]
        public Character? Character { get; set; }
        public int RulesetId { get; set; }
        [ForeignKey(nameof(RulesetId))]
        public Ruleset? Ruleset { get; set; }
    }
}
