using ECommerceApp.PresentationLayer.Modules.Orders.ViewModel;

namespace ECommerceApp.PresentationLayer.Modules.Orders.Interfaces
{
    public interface ICheckoutViewModelProvider
    {
        CheckoutViewModel? GetCheckoutViewModel();
        Task<OrderConfirmViewModel> PlaceOrderAsync(CheckoutViewModel model);
    }
}
