using AutoMapper;
using ECommerceApp.Domain.Entities;
using ECommerceApp.PresentationLayer.Modules.Carts.ViewModel;

namespace ECommerceApp.PresentationLayer.Modules.Carts
{
    public class CartMappingProfile : Profile
    {
        public CartMappingProfile() 
        {
            CreateMap<CartItem, CartItemViewModel>()
                .ForMember(dest => dest.LineTotal, opt => opt.MapFrom(src => src.UnitPrice * src.Quantity));
            CreateMap<Cart, CartViewModel>();
        }
    }
}
