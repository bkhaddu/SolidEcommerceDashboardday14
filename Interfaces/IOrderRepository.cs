using SolidEcommerceDashboard.Models;

namespace SolidEcommerceDashboard.Interfaces
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(Order order);
        Task<List<Order>> GetAllOrdersAsync();
    }
}