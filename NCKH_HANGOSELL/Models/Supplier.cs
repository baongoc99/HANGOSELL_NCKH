using System;
using System.ComponentModel.DataAnnotations;

namespace NCKH_HANGOSELL.Models
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }  // Thuộc tính Id là khoá chính của thực thể Supplier.

        [Required]
        [Display(Name = "Supply ID")]
        public string SupplyId { get; set; }  // Mã định danh nhà cung cấp.

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }  // Tên của nhà cung cấp.

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }  // Địa chỉ email của nhà cung cấp.

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }  // Số điện thoại của nhà cung cấp.

        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }  // Địa chỉ của nhà cung cấp.

        [DataType(DataType.DateTime)]
        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; }  // Ngày và giờ tạo bản ghi nhà cung cấp.

    }
}
