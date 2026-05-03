using ECommerceApp.PresentationLayer.Modules.Orders.ViewModel;

namespace ECommerceApp.PresentationLayer.Modules.Orders.Interfaces
{
    public interface IOrderViewModelProvider
    {
        Task<List<OrderConfirmViewModel>> GetCustomerOrdersAsync(string userId);
        Task<OrderDetailsViewModel?> GetOrderDetailsAsync(int orderId, string userId, bool isAdmin);
        Task MarkDeliveredAsync(int orderId);
    }
}
