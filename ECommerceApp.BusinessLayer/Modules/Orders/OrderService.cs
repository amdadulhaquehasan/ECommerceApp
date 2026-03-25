using ECommerceApp.BusinessLayer.Modules.Carts.Interfaces;
using ECommerceApp.BusinessLayer.Modules.Orders.Interfaces;
using ECommerceApp.DataAccessLayer.Modules.Orders.Interfaces;
using ECommerceApp.Domain.Entities;

namespace ECommerceApp.BusinessLayer.Modules.Orders
{
    public class OrderService : IOrderService
    {
        private readonly ICartService _cartService;

        private readonly IOrderRepository _orderRepository;

        public OrderService(ICartService cartService, IOrderRepository orderRepository)
        {
            _cartService = cartService;
            _orderRepository = orderRepository;
        }

        public async Task<Order> PlaceOrderAsync(string firstName, string lastName, string email, string phone, string shipAddress)
        {
            var cart = _cartService.GetCart();
            if (cart == null || cart.Items.Count == 0)
            {
                throw new InvalidOperationException("Cart is empty");
            }

            var customer = new Customer
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Phone = phone ?? "",
                Address = shipAddress,
                CreatedDate = DateTime.UtcNow
            };

            var order = new Order
            {
                CustomerId = null,
                OrderDate = DateTime.UtcNow,
                TotalAmount = cart.GrandTotal,
                Status = "Pending",
                ShipAddress = shipAddress
            };

            foreach (var item in cart.Items)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    SubTotal = item.LineTotal
                });
            }

            order = await _orderRepository.AddAsync(order);
            _cartService.ClearCart();

            return order;
        }
    }
}
