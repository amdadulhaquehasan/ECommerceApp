using ECommerceApp.PresentationLayer.Modules.Orders.ViewModel;

namespace ECommerceApp.PresentationLayer.Modules.Orders.Interfaces
{
    public interface ICheckoutViewModelProvider
    {
        Task<CheckoutViewModel?> GetCheckoutViewModel(string userId);
        Task<OrderConfirmViewModel> PlaceOrderAsync(CheckoutViewModel model, string userId);
    }
}
