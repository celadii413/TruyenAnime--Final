using System.ComponentModel.DataAnnotations;

namespace TruyenAnime.Models
{
    public class Banner
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Display(Name = "Button Text")]
        public string? ButtonText { get; set; }

        [Display(Name = "Background Image URL")]
        public string? BackgroundImageUrl { get; set; }

        [Display(Name = "Background Color (Hex)")]
        public string BackgroundColor { get; set; }
    }
}
