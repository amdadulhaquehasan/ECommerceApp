namespace ECommerceApp.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime CreatedDate { get; set; }

        // Relationship: One customer can have many orders
        public ICollection<Order> Orders { get; set; } = new List<Order>();

        // Relationship: One customer can have many shopping carts
        public ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();
    }
}
