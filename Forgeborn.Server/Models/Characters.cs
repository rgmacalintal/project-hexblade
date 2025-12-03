using Forgeborn.Server.Models;
using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;
using System.Text.Json;

namespace Forgeborn.Server.Models
{
    public class Characters
    {
        private static readonly string defaultInventory = "{ " +
            "'inventory' : { } }";

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Class { get; set; } = null!;
        [Required]
        public string Race { get; set; } = null!;
        [Required]
        public int Level { get; set; } = 1;
        public int MaxHP { get; set; }
        public int CurrentHP { get; set; }
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
        public JObject Inventory { get; set; } = JObject.Parse(defaultInventory);
        public string Background { get; set; } = null!;
        public string Journal { get; set; } = null!;
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        [Required]
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public Users? User { get; set; }
        public int PlayerId { get; set; }
        [ForeignKey(nameof(PlayerId))]
        public Players? Player { get; set; }
        public ICollection<Rulesets> Rulesets { get; set; } = new List<Rulesets>();
        public ICollection<Players> Players { get; set; } = new List<Players>();
        public Lobbys? Lobby => Player?.Lobby;

        public void InitializeHP()
        {
            MaxHP = 10 + (Level * 5);
            CurrentHP = MaxHP;
        }

    }

}
