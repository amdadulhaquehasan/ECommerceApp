using ECommerceApp.Domain.Entities;

namespace ECommerceApp.BusinessLayer.Modules.Carts.Interfaces
{
    public interface ICartService
    {
        Cart GetCart();
        void AddItem(int productId, string productName, decimal unitPrice, int quantity = 1);
        void UpdateQuantity(int productId, int quantity);
        void RemoveItem(int productId);
    }
}
