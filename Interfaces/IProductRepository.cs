using SolidEcommerceDashboard.Models;

namespace SolidEcommerceDashboard.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync();
        Task AddProductAsync(Product product);
    }
}