using AutoMapper;
using ECommerceApp.BusinessLayer.Modules.Products.Interfaces;
using ECommerceApp.Domain.Entities;
using ECommerceApp.PresentationLayer.Modules.Products.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Products.ViewModel;

namespace ECommerceApp.PresentationLayer.Modules.Products
{
    public class ProductViewModelProvider : IProductViewModelProvider
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductViewModelProvider(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        public async Task<ProductEditViewModel?> GetByIdAsync(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return null;

            return _mapper.Map<ProductEditViewModel>(product);
        }

        public async Task<IReadOnlyList<ProductListViewModel>> GetAllAsync()
        {
            var product = await _productService.GetAllAsync();
            return _mapper.Map<IReadOnlyList<ProductListViewModel>>(product);
        }

        public async Task<Product> AddAsync(ProductCreateViewModel productVm)
        {
            var product = _mapper.Map<Product>(productVm);
            return await _productService.AddAsync(product);
        }

        public async Task UpdateAsync(ProductEditViewModel productVm)
        {
            var product = _mapper.Map<Product>(productVm);
            await _productService.UpdateAsync(product);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return false;

            return await _productService.DeleteAsync(product);
        }
    }
}
