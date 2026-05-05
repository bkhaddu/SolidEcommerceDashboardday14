using System.ComponentModel.DataAnnotations;

namespace SolidEcommerceDashboard.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public string CustomerName { get; set; } = "";

        [Required]
        public string ProductName { get; set; } = "";

        public decimal Amount { get; set; }

        public decimal TotalAmount { get; set; }

        public string PaymentMethod { get; set; } = "";

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public string Status { get; set; } = "Pending";
    }
}