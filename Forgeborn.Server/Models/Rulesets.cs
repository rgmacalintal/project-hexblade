using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Hexblade.Server.Models
{
    public class Rulesets
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime CreatedOn { get; set; } = DateTime.Now;


        public int CharacterId { get; set; }
        [ForeignKey(nameof(CharacterId))]
        public Characters? CreatedBy { get; set; }
    }
}
