using AutoMapper;
using ECommerceApp.BusinessLayer.Modules.Orders.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Orders.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Orders.ViewModel;

namespace ECommerceApp.PresentationLayer.Modules.Orders
{
    public class OrderViewModelProvider : IOrderViewModelProvider
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderViewModelProvider(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        public async Task<List<OrderConfirmViewModel>> GetCustomerOrdersAsync(string userId)
        {
            var orders = await _orderService.GetOrdersByUserIdAsync(userId);

            return _mapper.Map<List<OrderConfirmViewModel>>(orders);
        }

        public async Task<OrderDetailsViewModel?> GetOrderDetailsAsync(int orderId, string userId, bool isAdmin)
        {
            var order = await _orderService.GetByIdAsync(orderId);
            if (order == null) 
            {
                return null;
            }

            if (!isAdmin && order.ApplicationUserId != userId)
            {
                return null;
            }
            return _mapper.Map<OrderDetailsViewModel>(order);
        }

        public async Task MarkDeliveredAsync(int orderId)
        {
            await _orderService.MarkDeliveredAsync(orderId);
        }
    }
}
