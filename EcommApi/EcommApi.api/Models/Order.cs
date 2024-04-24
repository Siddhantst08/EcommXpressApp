using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace EcommApi.api.Models
{
    
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public int UserId {  get; set; }
        [Required]
        public string username { get; set; }
      
       
        public string Status {  get; set; }

        [Required]
        public int Price { get; set; }


    }
}
