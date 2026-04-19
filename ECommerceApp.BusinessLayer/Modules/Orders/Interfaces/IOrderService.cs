using ECommerceApp.Domain.Entities;

namespace ECommerceApp.BusinessLayer.Modules.Orders.Interfaces
{
    public interface IOrderService
    {
        Task<Order> PlaceOrderAsync(
            string userId,
            string shipAddress);
    }
}
