using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Customers.Models
{
    public class Customer
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        public virtual Address Address { get; set; }

        [Required]
        [MaxLength(200)]
        public string EmailAddress { get; set; }

        [Required]
        [MaxLength(255)]
        public string WebsiteURL { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }
    }
}