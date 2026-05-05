using Microsoft.EntityFrameworkCore;
using SolidEcommerceDashboard.Models;

namespace SolidEcommerceDashboard.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<AppUser> AppUsers { get; set; }
    }
}