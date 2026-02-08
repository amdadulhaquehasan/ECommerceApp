using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(50)]
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; }

        // Relationship: One Category can have many Products
        //public ICollection<Product> products { get; set; } = new List<Product>();
    }
}
