namespace ECommerceApp.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string SKU { get; set; }
        public int CategoryId { get; set; } // Foreign key to Category
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        // Relationship: One product belongs to one category
        public Category Category { get; set; }

        // Relationship: One product has one inventory record
        public Inventory Inventory { get; set; }

        // Relationship: One product can have many order items
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

        // Relationship: One product can be in many shopping carts
        public ICollection<ShoppingCart> shoppingCarts { get; set; } = new List<ShoppingCart>();
    }
}
