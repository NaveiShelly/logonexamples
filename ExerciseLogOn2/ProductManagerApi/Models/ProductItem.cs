using System.ComponentModel.DataAnnotations;

namespace ProductManagerApi.Models
{
    public class ProductItem
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public string? Category { get; set; }

        public bool InStock { get; set; }
    }
}