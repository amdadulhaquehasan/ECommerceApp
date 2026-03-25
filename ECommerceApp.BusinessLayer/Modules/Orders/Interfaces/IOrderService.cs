using ECommerceApp.Domain.Entities;

namespace ECommerceApp.BusinessLayer.Modules.Orders.Interfaces
{
    public interface IOrderService
    {
        Task<Order> PlaceOrderAsync(
            string firstName,
            string lastName,
            string email,
            string phone,
            string shipAddress);
    }
}
