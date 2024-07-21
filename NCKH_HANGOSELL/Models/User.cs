using System.ComponentModel.DataAnnotations;

namespace NCKH_HANGOSELL.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }  // Thuộc tính Id là khoá chính của thực thể User.

        public string UserId { get; set; }  // Mã định danh người dùng.

        [Display(Name = "Full Name")]
        public string Name { get; set; }  // Tên đầy đủ của người dùng.

        [EmailAddress]
        public string Email { get; set; }  // Địa chỉ email của người dùng, với xác thực định dạng email.

        [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }  // Ngày sinh của người dùng, định dạng ngày.

        [DataType(DataType.Date)]
        [Display(Name = "Join Date")]
        public DateTime? JoinDate { get; set; }  // Ngày gia nhập của người dùng, định dạng ngày.

        [Phone]
        public string PhoneNumber { get; set; }  // Số điện thoại của người dùng, với xác thực định dạng số điện thoại.

        public string Status { get; set; }  // Trạng thái của người dùng (có thể là "active", "inactive", v.v.).

        [Display(Name = "Role Name")]
        public string RoleName { get; set; }  // Tên vai trò của người dùng trong hệ thống (ví dụ: "admin", "user").

        public string? Avatar { get; set; }  // Đường dẫn tới ảnh đại diện của người dùng.
        public string Position { get; set; }  // Vị trí công việc của người dùng.

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }  // Mật khẩu của người dùng, được yêu cầu và bảo mật.

        public string? RememberToken { get; set; }  // Token dùng để ghi nhớ đăng nhập của người dùng.

        [DataType(DataType.DateTime)]
        public DateTime? RecordCreatedOn { get; set; }  // Ngày và giờ tạo bản ghi người dùng.

    }
}
