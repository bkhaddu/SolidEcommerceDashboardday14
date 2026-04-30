using System.ComponentModel.DataAnnotations;

namespace SolidEcommerceDashboard.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = "";

        [Required]
        public string Category { get; set; } = "";

        [Required]
        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}