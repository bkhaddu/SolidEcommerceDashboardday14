using Microsoft.EntityFrameworkCore;
using SolidEcommerceDashboard.Data;
using SolidEcommerceDashboard.Interfaces;
using SolidEcommerceDashboard.Models;

namespace SolidEcommerceDashboard.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CartItem>> GetCartItemsAsync()
        {
            return await _context.CartItems.ToListAsync();
        }

        public async Task AddToCartAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null)
                return;

            var existingItem = await _context.CartItems
                .FirstOrDefaultAsync(x => x.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity += 1;
            }
            else
            {
                _context.CartItems.Add(new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = 1,
                    ImagePath = product.ImagePath
                });
            }

            await _context.SaveChangesAsync();
        }

        public async Task IncreaseQuantityAsync(int cartItemId)
        {
            var item = await _context.CartItems.FindAsync(cartItemId);

            if (item != null)
            {
                item.Quantity += 1;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DecreaseQuantityAsync(int cartItemId)
        {
            var item = await _context.CartItems.FindAsync(cartItemId);

            if (item != null)
            {
                if (item.Quantity > 1)
                {
                    item.Quantity -= 1;
                }
                else
                {
                    _context.CartItems.Remove(item);
                }

                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveFromCartAsync(int cartItemId)
        {
            var item = await _context.CartItems.FindAsync(cartItemId);

            if (item != null)
            {
                _context.CartItems.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ClearCartAsync()
        {
            var items = await _context.CartItems.ToListAsync();

            _context.CartItems.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}