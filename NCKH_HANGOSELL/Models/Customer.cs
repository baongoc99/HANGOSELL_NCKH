using System.ComponentModel.DataAnnotations;

namespace NCKH_HANGOSELL.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Customer Type")]
        public string CustomerType { get; set; }
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }
        public string Email { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? RecordCreatedOn { get; set; }  // Ngày và giờ tạo bản ghi người dùng.
    }
}
