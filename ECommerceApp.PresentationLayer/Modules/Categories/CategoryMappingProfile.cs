using AutoMapper;
using ECommerceApp.Domain.Entities;
using ECommerceApp.PresentationLayer.Modules.Categories.ViewModel;
using System.Runtime.InteropServices;

namespace ECommerceApp.PresentationLayer.Modules.Categories
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            //ViewModel to Entity
            CreateMap<CategoryCreateViewModel, Category>();
            CreateMap<CategoryEditViewModel, Category>();

            //Entity to ViewModel
            CreateMap<Category, CategoryEditViewModel>();
            CreateMap<Category, CategoryListViewModel>();
        }
    }
}
