using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SolidEcommerceDashboard.Interfaces;
using SolidEcommerceDashboard.Models;
using SolidEcommerceDashboard.Services;

namespace SolidEcommerceDashboard.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly ICartRepository _cartRepository;
        private readonly OrderService _orderService;

        public CheckoutModel(ICartRepository cartRepository, OrderService orderService)
        {
            _cartRepository = cartRepository;
            _orderService = orderService;
        }

        public List<CartItem> CartItems { get; set; } = new List<CartItem>();

        public decimal GrandTotal { get; set; }

        [BindProperty]
        public string PaymentMethod { get; set; } = "Card";

        [BindProperty]
        public string CardHolderName { get; set; } = "";

        [BindProperty]
        public string CardNumber { get; set; } = "";

        [BindProperty]
        public string ExpiryDate { get; set; } = "";

        [BindProperty]
        public string CVV { get; set; } = "";

        [BindProperty]
        public string UpiId { get; set; } = "";

        public async Task<IActionResult> OnGetAsync()
        {
            CartItems = await _cartRepository.GetCartItemsAsync();

            if (!CartItems.Any())
            {
                return RedirectToPage("/Cart");
            }

            GrandTotal = CartItems.Sum(x => x.TotalPrice);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            CartItems = await _cartRepository.GetCartItemsAsync();

            if (!CartItems.Any())
            {
                return RedirectToPage("/Cart");
            }

            GrandTotal = CartItems.Sum(x => x.TotalPrice);

            if (PaymentMethod == "Card")
            {
                if (string.IsNullOrWhiteSpace(CardHolderName) ||
                    string.IsNullOrWhiteSpace(CardNumber) ||
                    string.IsNullOrWhiteSpace(ExpiryDate) ||
                    string.IsNullOrWhiteSpace(CVV))
                {
                    ModelState.AddModelError("", "Please fill all card details.");
                    return Page();
                }
            }

            if (PaymentMethod == "UPI")
            {
                if (string.IsNullOrWhiteSpace(UpiId))
                {
                    ModelState.AddModelError("", "Please enter your UPI ID.");
                    return Page();
                }
            }

            var order = new Order
            {
                TotalAmount = GrandTotal,
                OrderDate = DateTime.Now
            };

            await _orderService.CreateOrderAsync(order);
            await _cartRepository.ClearCartAsync();

            return RedirectToPage("/OrderSuccess");
        }
    }
}