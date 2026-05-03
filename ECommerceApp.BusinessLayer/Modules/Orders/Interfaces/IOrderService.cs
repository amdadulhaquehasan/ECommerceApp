using ECommerceApp.Domain.Entities;

namespace ECommerceApp.BusinessLayer.Modules.Orders.Interfaces
{
    public interface IOrderService
    {
        Task<Order> PlaceOrderAsync(string userId, string shipAddress);
        Task<Order?> GetByIdAsync(int orderId);
        Task<List<Order>> GetOrdersByUserIdAsync(string userId);
        Task<List<Order>> GetAllOrdersAsync();
        Task MarkDeliveredAsync(int orderId);
    }
}
