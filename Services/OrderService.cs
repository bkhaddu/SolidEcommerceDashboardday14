using SolidEcommerceDashboard.Interfaces;
using SolidEcommerceDashboard.Models;

namespace SolidEcommerceDashboard.Services
{
    public class OrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IPaymentService _paymentService;
        private readonly INotificationService _notificationService;

        public OrderService(
            IOrderRepository orderRepository,
            IPaymentService paymentService,
            INotificationService notificationService)
        {
            _orderRepository = orderRepository;
            _paymentService = paymentService;
            _notificationService = notificationService;
        }

        public async Task<string> CreateOrderAsync(Order order)
        {
            // Strategy Pattern (Payment)
            string paymentResult = _paymentService.Pay(order.Amount);

            // Save order
            await _orderRepository.AddOrderAsync(order);

            // Notification
            string notificationResult =
                _notificationService.SendNotification($"Order placed for {order.CustomerName}");

            return paymentResult + " | " + notificationResult;
        }
    }
}