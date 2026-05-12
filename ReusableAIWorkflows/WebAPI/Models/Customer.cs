using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string EmailId { get; set; } = string.Empty;

        [Required]
        [Phone]
        [StringLength(20)]
        public string MobileNumber { get; set; } = string.Empty;
    }
}
