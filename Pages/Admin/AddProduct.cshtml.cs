using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SolidEcommerceDashboard.Data;
using SolidEcommerceDashboard.Models;

namespace SolidEcommerceDashboard.Pages.Admin
{
    public class AddProductModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public AddProductModel(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Product Product { get; set; } = new Product();

        [BindProperty]
        public IFormFile? ProductImage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
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

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ProductImage.FileName);

                string fullPath = Path.Combine(folderPath, fileName);

                using FileStream stream = new FileStream(fullPath, FileMode.Create);
                await ProductImage.CopyToAsync(stream);

                Product.ImagePath = "/images/products/" + fileName;
            }

            _context.Products.Add(Product);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Admin/Dashboard");
        }
    }
}