using System.ComponentModel.DataAnnotations;
namespace ProductC.Models
{
    public class Product
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        public string ImageUrl { get; set; }
    }
}