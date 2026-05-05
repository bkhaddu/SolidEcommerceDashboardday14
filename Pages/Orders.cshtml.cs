using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SolidEcommerceDashboard.Models;
using SolidEcommerceDashboard.Services;
using SolidEcommerceDashboard.Interfaces;

namespace SolidEcommerceDashboard.Pages
{
    public class OrdersModel : PageModel
    {
        private readonly OrderService _orderService;
        private readonly ICartRepository _cartRepository;

        public OrdersModel(
            OrderService orderService,
            ICartRepository cartRepository)
        {
            _orderService = orderService;
            _cartRepository = cartRepository;
        }

        public List<Order> Orders { get; set; } = new List<Order>();

        public decimal GrandTotal { get; set; }

        [BindProperty]
        public Order Order { get; set; } = new Order();

        public async Task OnGetAsync()
        {
            Orders = await _orderService.GetAllOrdersAsync();

            var cartItems = await _cartRepository.GetCartItemsAsync();
            GrandTotal = cartItems.Sum(x => x.TotalPrice);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var cartItems = await _cartRepository.GetCartItemsAsync();

            if (!cartItems.Any())
                return RedirectToPage("/Cart");

            Order.TotalAmount = cartItems.Sum(x => x.TotalPrice);
            Order.OrderDate = DateTime.Now;

            // ✅ FIXED LINE (NO STRING)
            await _orderService.CreateOrderAsync(Order);

            // Clear cart after order
            await _cartRepository.ClearCartAsync();

            return RedirectToPage();
        }
    }
}