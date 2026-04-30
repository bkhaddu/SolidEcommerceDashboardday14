namespace SolidEcommerceDashboard.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string CustomerName { get; set; } = "";

        public string ProductName { get; set; } = "";

        public decimal Amount { get; set; }

        public string PaymentMethod { get; set; } = "";

        public DateTime OrderDate { get; set; } = DateTime.Now;
    }
}