using SolidEcommerceDashboard.Models;

namespace SolidEcommerceDashboard.Interfaces
{
    public interface ICartRepository
    {
        Task<List<CartItem>> GetCartItemsAsync();
        Task AddToCartAsync(int productId);
        Task IncreaseQuantityAsync(int cartItemId);
        Task DecreaseQuantityAsync(int cartItemId);
        Task RemoveFromCartAsync(int cartItemId);
        Task ClearCartAsync();
    }
}