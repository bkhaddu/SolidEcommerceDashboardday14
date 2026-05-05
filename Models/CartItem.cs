using System.ComponentModel.DataAnnotations;

namespace SolidEcommerceDashboard.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        [Required]
        public string ProductName { get; set; } = "";

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public string? ImagePath { get; set; }

        public decimal TotalPrice => Price * Quantity;
    }
}