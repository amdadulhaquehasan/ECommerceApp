namespace ECommerceApp.Domain.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int OrderId { get; set; } // Foreign key to Order
        public int ProductId { get; set; } // Foreign key to Product
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get; set; }

        // Relationship: OrderItem belongs to one Order
        public Order Order { get; set; }

        // Relationship: OrderItem refers to Product
        public Product Product { get; set; }
    }
}
