using System.ComponentModel.DataAnnotations;

namespace TruyenAnime.Models
{
    public class User
    {
        [Required]
        public string Role { get; set; } = "User"; // Mặc định là User

        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        public ICollection<Review> Reviews { get; set; }
    }
}
