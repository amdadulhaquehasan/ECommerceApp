using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        [StringLength(50)]
        public string SKU { get; set; }

        [StringLength(500)]
        public string? ImagePath { get; set; }
        public int CategoryId { get; set; } // Foreign key to Category
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        // Relationship: One product belongs to one category
        public Category Category { get; set; }

        // Relationship: One product has one inventory record
        // public Inventory? Inventory { get; set; }

        // Relationship: One product can have many order items
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        // Relationship: One product can be in many shopping carts
        // public ICollection<ShoppingCart> shoppingCarts { get; set; } = new List<ShoppingCart>();
    }
}
