using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using Microsoft.EntityFrameworkCore;

namespace EcommApi.api.Models
{
    [Index(nameof(Email), IsUnique = true)]
    [Index(nameof(Username), IsUnique = true)]
    [Index(nameof(Phone), IsUnique = true)]

    public class User
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        
        public string Email { get; set; }
        [Required]
        
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }    
        public int Age {  get; set; }
        public string Gender { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Role { get; set; }    
        public DateTime CreatedAt { get; set; }
    }
}
