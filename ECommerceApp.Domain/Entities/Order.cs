namespace ECommerceApp.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; } // Foreign key to Customer
        public DateTime OrderDate { get; set; }
        public int TotalAmount { get; set; }
        public string Status { get; set; }
        public string ShipAddress { get; set; }

        // Relationship: One Order belongs to one Customer
        public Customer Customer { get; set; }

        // Relationship: One Order has one Payment
        public Payment Payment { get; set; }

        // Relationship: One Order has many OrderItems
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
