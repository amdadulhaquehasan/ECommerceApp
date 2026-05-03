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

        public async Task<Order> PlaceOrderAsync(string userId, string shipAddress)
        {
            var cart = _cartService.GetCart();
            if (cart == null || cart.Items.Count == 0)
            {
                throw new InvalidOperationException("Cart is empty");
            }

            var order = new Order
            {
                ApplicationUserId = userId,
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

        public async Task<Order?> GetByIdAsync(int orderId)
        {
            return await _orderRepository.GetByIdAsync(orderId);
        }

        public async Task<List<Order>> GetOrdersByUserIdAsync(string userId)
        {
            return await _orderRepository.GetOrdersByUserIdAsync(userId);
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllOrdersAsync();
        }

        public async Task MarkDeliveredAsync(int orderId)
        {
            var order = await _orderRepository.GetByIdAsync(orderId);
            if (order == null)
            {
                throw new InvalidOperationException("Order not found");
            }

            order.Status = "Delivered";

            await _orderRepository.UpdateAsync(order); 
        }
    }
}
