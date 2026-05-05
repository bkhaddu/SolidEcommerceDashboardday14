using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SolidEcommerceDashboard.Data;
using SolidEcommerceDashboard.Models;

namespace SolidEcommerceDashboard.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly AppDbContext _context;

        public RegisterModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AppUser NewUser { get; set; } = new AppUser();

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrWhiteSpace(NewUser.FullName) ||
                string.IsNullOrWhiteSpace(NewUser.Email) ||
                string.IsNullOrWhiteSpace(NewUser.Password))
            {
                return Page();
            }

            NewUser.Role = "Customer";

            _context.AppUsers.Add(NewUser);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Account/Login");
        }
    }
}