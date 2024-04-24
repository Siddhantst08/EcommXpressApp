using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace EcommApi.api.Models
{
    
    public class Cart
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ProductName {  get; set; }
        [Required]
        public int UserId {  get; set; }
        [Required]
        public int ProductId {  get; set; }
        
        public int Quantity {  get; set; }
        [Required]
        public int Price {  get; set; }
        [Required]
        public string Image {  get; set; }
        [Required]
        public string Description { get; set; }
    }
}
