using System.ComponentModel.DataAnnotations;

namespace EcommApi.api.Models
{
    public class Auth
    {
        [Key] 
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
