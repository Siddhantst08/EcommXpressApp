using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace EcommApi.api.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Product
    {
        public int Id { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price {  get; set; }
        public string Description {  get; set; }
        [Required]
        public int Quantity {  get; set; }
        [Required]
        public string ImagePath { get; set; }

    }
}
