using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SolidEcommerceDashboard.Interfaces;
using SolidEcommerceDashboard.Models;

namespace SolidEcommerceDashboard.Pages
{
    public class CartModel : PageModel
    {
        private readonly ICartRepository _cartRepository;

        public CartModel(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public List<CartItem> CartItems { get; set; } = new List<CartItem>();

        public decimal GrandTotal { get; set; }

        public async Task OnGetAsync()
        {
            CartItems = await _cartRepository.GetCartItemsAsync();
            GrandTotal = CartItems.Sum(x => x.TotalPrice);
        }

        public async Task<IActionResult> OnPostIncreaseAsync(int id)
        {
            await _cartRepository.IncreaseQuantityAsync(id);
            return RedirectToPage("/Cart");
        }

        public async Task<IActionResult> OnPostDecreaseAsync(int id)
        {
            await _cartRepository.DecreaseQuantityAsync(id);
            return RedirectToPage("/Cart");
        }

        public async Task<IActionResult> OnPostRemoveAsync(int id)
        {
            await _cartRepository.RemoveFromCartAsync(id);
            return RedirectToPage("/Cart");
        }

        public async Task<IActionResult> OnPostClearAsync()
        {
            await _cartRepository.ClearCartAsync();
            return RedirectToPage("/Cart");
        }
    }
}