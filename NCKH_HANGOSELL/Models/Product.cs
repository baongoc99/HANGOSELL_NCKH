using NCKH_HANGOSELL.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NCKH_HANGOSELL.Models
{
    [Table("Product")] // Tên bảng trong cơ sở dữ liệu
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")] // Chỉ định kiểu dữ liệu cho cột Price
        public decimal Price { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        [StringLength(255)]
        public string Image { get; set; }

        [Required]
        [StringLength(50)]
        public string Status { get; set; }
    }
}

