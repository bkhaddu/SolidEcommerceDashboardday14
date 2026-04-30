using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SolidEcommerceDashboard.Interfaces;
using SolidEcommerceDashboard.Models;

namespace SolidEcommerceDashboard.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly IProductRepository _productRepository;

        public ProductsModel(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<Product> Products { get; set; } = new();

        [BindProperty]
        public Product Product { get; set; } = new();

        public async Task OnGetAsync()
        {
            Products = await _productRepository.GetAllProductsAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Products = await _productRepository.GetAllProductsAsync();
                return Page();
            }

            await _productRepository.AddProductAsync(Product);

            return RedirectToPage("/Products");
        }
    }
}
