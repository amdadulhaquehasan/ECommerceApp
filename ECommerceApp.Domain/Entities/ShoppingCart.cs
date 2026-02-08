namespace ECommerceApp.Domain.Entities
{
    public class ShoppingCart
    {
        public int Id { get; set; }
        public int CustomerId { get; set; } // Foreign key to Customer
        public int ProductId { get; set; } // Foreign key to Product
        public int Quantity { get; set; }
        public DateTime AddedDate { get; set; }

        // Relationship: One ShoppingCart has one Customer
        public Customer Customer { get; set; }

        // Relationship: One ShoppingCart has one Product
        public Product Product { get; set; }
    }
}
