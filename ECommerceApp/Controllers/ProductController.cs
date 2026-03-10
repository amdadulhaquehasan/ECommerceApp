using ECommerceApp.BusinessLayer.Exceptions;
using ECommerceApp.PresentationLayer.Modules.Categories.Interface;
using ECommerceApp.PresentationLayer.Modules.Products.Interfaces;
using ECommerceApp.PresentationLayer.Modules.Products.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerceApp.Controllers
{
    public class ProductController : Controller
    {
        #region Dependencies
        private readonly IProductViewModelProvider _productViewModelProvider;
        private readonly ICategoryViewModelProvider _categoryViewModelProvider;
        private readonly IWebHostEnvironment _env;

        public ProductController(IProductViewModelProvider productViewModelProvider, ICategoryViewModelProvider categoryViewModelProvider, IWebHostEnvironment env)
        {
            _productViewModelProvider = productViewModelProvider;
            _categoryViewModelProvider = categoryViewModelProvider;
            _env = env;
        }
        #endregion

        #region View All Products
        public async Task<IActionResult> Index()
        {
            var products = await _productViewModelProvider.GetAllAsync();
            return View(products);
        }
        #endregion

        #region Create
        public async Task<IActionResult> Create()
        {
            var categories = await _categoryViewModelProvider.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateViewModel product, IFormFile? imageFile)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoryViewModelProvider.GetAllAsync();
                ViewBag.Categories = new SelectList(categories, "Id", "Name");
                return View(product);
            }

            product.ImagePath = await SaveProductImageAsync(imageFile);

            try
            {
                await _productViewModelProvider.AddAsync(product);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidUserInputException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                var categories = await _categoryViewModelProvider.GetAllAsync();
                ViewBag.Categories = new SelectList(categories, "Id", "Name");
                return View(product);
            }
        }
        #endregion

        #region Helper Method for Image Saving
        private async Task<string?> SaveProductImageAsync(IFormFile? imageFile, string? oldPath = null)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return oldPath;
            }
            
            var allowed = new[] { ".jpg", ".jpeg", ".png" };
            var ext = Path.GetExtension(imageFile.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(ext) || !allowed.Contains(ext))
            {
                return oldPath;
            }

            var dir = Path.Combine(_env.WebRootPath, "Images", "Products");
            Directory.CreateDirectory(dir);
            var fileName = $"{Guid.NewGuid():N}{ext}";
            var fullPath = Path.Combine(dir, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            if (!string.IsNullOrEmpty(oldPath))
            {
                var oldFullPath = Path.Combine(_env.WebRootPath, oldPath.TrimStart('/'));
                if (System.IO.File.Exists(oldFullPath))
                {
                    System.IO.File.Delete(oldFullPath);
                }
            }

            return "/Images/Products/" + fileName;
        }
        #endregion

        #region Edit
        public async Task<IActionResult> Edit(int productId)
        {
            var product = await _productViewModelProvider.GetByIdAsync(productId);
            if (product == null)
            {
                return NotFound();
            }
            var categories = await _categoryViewModelProvider.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductEditViewModel product, IFormFile? imageFile)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                var categories = await _categoryViewModelProvider.GetAllAsync();
                ViewBag.Categories = new SelectList(categories, "Id", "Name");
                return View(product);
            }

            product.ImagePath = await SaveProductImageAsync(imageFile, product.ImagePath);

            try
            {
                await _productViewModelProvider.UpdateAsync(product);
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidUserInputException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                var categories = await _categoryViewModelProvider.GetAllAsync();
                ViewBag.Categories = new SelectList(categories, "Id", "Name");
                return View(product);
            }
        }
        #endregion

        #region Details
        public async Task<IActionResult> Details(int id)
        {
            var product = await _productViewModelProvider.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productViewModelProvider.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            bool isDeleted = await _productViewModelProvider.DeleteAsync(id);
            if (!isDeleted)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
