namespace ECommerceApp.Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int OrderId { get; set; } // Foreign key to Order
        public int Amount { get; set; }
        public string PayMethod { get; set; }
        public DateTime PayDate { get; set; }
        public string TransId { get; set; }

        // Relationship: One Payment belongs to one Order
        public Order Order { get; set; }
    }
}
