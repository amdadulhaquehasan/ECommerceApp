using AutoMapper;
using ECommerceApp.Domain.Entities;
using ECommerceApp.PresentationLayer.Modules.Products.ViewModel;

namespace ECommerceApp.PresentationLayer.Modules.Products
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile() 
        {
            //ViewModel to Entity
            CreateMap<ProductCreateViewModel, Product>();
            CreateMap<ProductEditViewModel, Product>();

            //Entity to ViewModel
            CreateMap<Product, ProductEditViewModel>()
                .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.Category != null ? s.Category.Name : string.Empty));
            CreateMap<Product, ProductListViewModel>()
                .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.Category != null ? s.Category.Name : string.Empty));
        }
    }
}
