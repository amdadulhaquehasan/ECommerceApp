using ECommerceApp.PresentationLayer.Modules.Carts.ViewModel;

namespace ECommerceApp.PresentationLayer.Modules.Carts.Interfaces
{
    public interface ICartViewModelProvider
    {
        CartViewModel GetCartViewModel();
        void AddItem(int productId, string productName, decimal unitPrice, int quantity = 1);
        void UpdateQuantity(int productId, int newQuantity);
        void RemoveItem(int productId);
    }
}
