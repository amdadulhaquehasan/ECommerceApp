using AutoMapper;
using ECommerceApp.Domain.Entities;
using ECommerceApp.PresentationLayer.Modules.Orders.ViewModel;

namespace ECommerceApp.PresentationLayer.Modules.Orders
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<Order, OrderConfirmViewModel>()
                .ForMember(
                dest => dest.OrderId,
                opt => opt.MapFrom(src => src.Id));

            CreateMap<Order, OrderDetailsViewModel>();

            CreateMap<OrderItem, OrderItemDetailsViewModel>();
        }
    }
}
