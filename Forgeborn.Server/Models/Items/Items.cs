using System.ComponentModel.DataAnnotations;

namespace Forgeborn.Server.Models.Items
{
    public class Items
    {

        public enum RarityVal
        {
            None,
            Common,
            Uncommon,
            Rare,
            VeryRare,
            Legendary
        }
        public enum SourceVal
        {
            DND,
            Homebrew
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Cost { get; set; } = string.Empty;
        public string Weight { get; set; } = string.Empty;
        public SourceVal Source { get; set; } = SourceVal.Homebrew;
        public RarityVal Rarity { get; set; } = RarityVal.None;
        public bool WondrousItem { get; set; } = false;
        public bool Attunement { get; set; } = false;
        public string Requirements { get; set; } = string.Empty;

    }
}
