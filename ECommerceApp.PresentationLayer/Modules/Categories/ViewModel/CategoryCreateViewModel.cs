using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.PresentationLayer.Module.Categories.ViewModel
{
    public class CategoryCreateViewModel
    {
        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Category name must be between 3 and 50 characters.")]
        [Display(Name = "Category Name")]
        public string Name { get; set; }

        [StringLength(50, ErrorMessage = "Description cannot exceed 500 characters.")]
        [Display(Name = "Category Description")]
        public string? Description { get; set; }

        public string? ErrorMessage { get; set; }
    }
}
