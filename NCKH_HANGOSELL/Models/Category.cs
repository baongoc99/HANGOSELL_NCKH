using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NCKH_HANGOSELL.Models
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int Id { get; set; }

        public string CategoryId { get; set; }

        [Required]
        public string CategoryName { get; set; }

        public string Description { get; set; }
    }

}
