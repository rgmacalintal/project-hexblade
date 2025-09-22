using System.ComponentModel.DataAnnotations;

namespace Project_Hexblade.Server.Models
{
    public class Items
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public int Page {  get; set; } = 0;
        [Required]
        public string Rarity { get; set; } = null!;
        [Required]
        public string Type { get; set; } = null!;
        public bool Attunement { get; set; } = false;
        public string Damage { get; set; } = null!;
        public string Properties { get; set; } = null!;
        public string Mastery { get; set; } = null!;
        public string Cost { get; set; } = null!;
        public string Text { get; set; } = null!;

    }
}
