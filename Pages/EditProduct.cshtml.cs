using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SolidEcommerceDashboard.Interfaces;
using SolidEcommerceDashboard.Models;

namespace SolidEcommerceDashboard.Pages
{
    public class EditProductModel : PageModel
    {
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _environment;

        public EditProductModel(
            IProductRepository productRepository,
            IWebHostEnvironment environment)
        {
            _productRepository = productRepository;
            _environment = environment;
        }

        [BindProperty]
        public Product Product { get; set; } = new Product();

        [BindProperty]
        public IFormFile? ProductImage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var product = await _productRepository.GetProductByIdAsync(id);

            if (product == null)
            {
                return RedirectToPage("/Products");
            }

            Product = product;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(Product.Category))
            {
                Product.Category = "General";
            }

            if (string.IsNullOrWhiteSpace(Product.Description))
            {
                Product.Description = "No description";
            }

            if (ProductImage != null)
            {
                string folderPath = Path.Combine(
                    _environment.WebRootPath,
                    "images",
                    "products"
                );

                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                string fileName = Guid.NewGuid().ToString()
                                  + Path.GetExtension(ProductImage.FileName);

                string fullPath = Path.Combine(folderPath, fileName);

                using FileStream stream = new FileStream(fullPath, FileMode.Create);
                await ProductImage.CopyToAsync(stream);

                Product.ImagePath = "/images/products/" + fileName;
            }

            await _productRepository.UpdateProductAsync(Product);

            return RedirectToPage("/Products");
        }
    }
}