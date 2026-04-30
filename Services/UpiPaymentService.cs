using SolidEcommerceDashboard.Interfaces;

namespace SolidEcommerceDashboard.Services
{
    public class UpiPaymentService : IPaymentService
    {
        public string Pay(decimal amount)
        {
            return $"Payment of ₹{amount} completed using UPI.";
        }
    }
}