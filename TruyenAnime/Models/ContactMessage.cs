using System;
using System.ComponentModel.DataAnnotations;

namespace TruyenAnime.Models
{
    public class ContactMessage
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập tên của bạn.")]
        [StringLength(100, ErrorMessage = "Tên không được vượt quá 100 ký tự.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ email.")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ.")]
        [StringLength(100, ErrorMessage = "Email không được vượt quá 100 ký tự.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập tin nhắn của bạn.")]
        [StringLength(1000, ErrorMessage = "Tin nhắn không được vượt quá 1000 ký tự.")]
        public string Message { get; set; }

        public DateTime SentDate { get; set; } = DateTime.Now; 
    }
}