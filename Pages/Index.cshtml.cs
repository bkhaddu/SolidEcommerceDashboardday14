using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SolidEcommerceDashboard.Interfaces;
using SolidEcommerceDashboard.Models;

namespace SolidEcommerceDashboard.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        private readonly ICartRepository _cartRepository;

        public IndexModel(
            IProductRepository productRepository,
            ICartRepository cartRepository)
        {
            _productRepository = productRepository;
            _cartRepository = cartRepository;
        }

        public List<Product> Products { get; set; } = new List<Product>();

        public async Task OnGetAsync()
        {
            Products = await _productRepository.GetAllProductsAsync();
        }

        public async Task<IActionResult> OnPostAddToCartAsync(int productId)
        {
            await _cartRepository.AddToCartAsync(productId);
            return RedirectToPage("/Cart");
        }
    }
}