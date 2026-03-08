using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.PresentationLayer.Modules.Categories.ViewModel
{
    public class CategoryCreateViewModel
    {
        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Category name must be between 3 and 50 characters.")]
        [Display(Name = "Category Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Category description is required")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        [Display(Name = "Category Description")]
        public string? Description { get; set; }
    }
}
