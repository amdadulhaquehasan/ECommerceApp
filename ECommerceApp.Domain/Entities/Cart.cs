namespace ECommerceApp.Domain.Entities
{
    public class Cart
    {
        public List<CartItem> Items { get; set; } = new();
        public decimal GrandTotal => Items.Sum(i => i.LineTotal);
        public int TotalItems => Items.Sum(i => i.Quantity);
    }
}
