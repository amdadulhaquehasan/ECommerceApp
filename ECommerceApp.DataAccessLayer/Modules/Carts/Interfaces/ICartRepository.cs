using ECommerceApp.Domain.Entities;

namespace ECommerceApp.DataAccessLayer.Modules.Carts.Interfaces
{
    public interface ICartRepository
    {
        Cart GetCart();
        void SaveCart(Cart cart);
        void ClearCart();
    }
}
