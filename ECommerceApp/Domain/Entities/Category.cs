using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Category name must be between 3 and 50 characters.")]
        [Display(Name = "Category Name")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        [Display(Name = "Category Description")]
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }

        // Relationship: One Category can have many Products
        //public ICollection<Product> products { get; set; } = new List<Product>();
    }
}
