using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace THLTW_BT3.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Họ tên không được để trống")]
        [StringLength(100, ErrorMessage = "Họ tên không được vượt quá 100 ký tự")]
        [Display(Name = "Họ tên")]
        public required string FullName { get; set; }

        [Display(Name = "Địa chỉ")]
        public string? Address { get; set; }

        [Display(Name = "Tuổi")]
        [Range(0, 150, ErrorMessage = "Tuổi không hợp lệ")]
        public int? Age { get; set; }

        [Display(Name = "Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Giới tính")]
        public string? Gender { get; set; }

        [Display(Name = "Ảnh đại diện")]
        public string? AvatarUrl { get; set; }

        [Display(Name = "Ngày tham gia")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Display(Name = "Lần đăng nhập cuối")]
        public DateTime? LastLoginAt { get; set; }

        [Display(Name = "Trạng thái")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "Danh sách đơn hàng")]
        public List<Order>? Orders { get; set; }

        [Display(Name = "Ghi chú")]
        public string? Notes { get; set; }
    }
}
