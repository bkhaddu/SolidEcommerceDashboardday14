using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SolidEcommerceDashboard.Interfaces;
using SolidEcommerceDashboard.Models;
using SolidEcommerceDashboard.Services;

namespace SolidEcommerceDashboard.Pages
{
    public class OrdersModel : PageModel
    {
        private readonly OrderService _orderService;
        private readonly IOrderRepository _orderRepository;

        public OrdersModel(OrderService orderService, IOrderRepository orderRepository)
        {
            _orderService = orderService;
            _orderRepository = orderRepository;
        }

        [BindProperty]
        public Order Order { get; set; } = new();

        public List<Order> Orders { get; set; } = new();

        public string Message { get; set; } = "";

        public async Task OnGetAsync()
        {
            Orders = await _orderRepository.GetAllOrdersAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Order.PaymentMethod = "UPI";

            Message = await _orderService.CreateOrderAsync(Order);

            Orders = await _orderRepository.GetAllOrdersAsync();

            return Page();
        }
    }
}