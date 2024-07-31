using NCKH_HANGOSELL.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NCKH_HANGOSELL.Models
{
    [Table("Product")] // Tên bảng trong cơ sở dữ liệu
    public class Product
    {
        [Key]
        public int Id { get; set; } // Khoá chính của bảng

        [Required]
        [StringLength(100)] // Giới hạn độ dài tối đa của tên sản phẩm
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Range(0, double.MaxValue)] // Đảm bảo giá không âm
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        // Thiết lập quan hệ nhiều-đến-một với model Category
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}

