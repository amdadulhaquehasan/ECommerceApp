using ECommerceApp.Domain.Entities;

namespace ECommerceApp.DataAccessLayer.Modules.Orders.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> AddAsync(Order order);
        Task<Order?> GetByIdAsync(int orderId);
    }
}
