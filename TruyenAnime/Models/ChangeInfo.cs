using System.ComponentModel.DataAnnotations;

namespace TruyenAnime.Models
{
    public class ChangeInfo
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập mật khẩu hiện tại")]
        public string CurrentPassword { get; set; }

        public string? NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Mật khẩu không khớp")]
        public string? ConfirmPassword { get; set; }
    }
}
