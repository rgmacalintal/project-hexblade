using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Forgeborn.Server.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        [PasswordPropertyText]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public ICollection<Characters> Characters { get; set; } = new List<Characters>();

    }
}
