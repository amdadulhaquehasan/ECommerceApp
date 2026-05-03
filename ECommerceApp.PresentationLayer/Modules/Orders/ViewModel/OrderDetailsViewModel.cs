namespace ECommerceApp.PresentationLayer.Modules.Orders.ViewModel
{
    public class OrderDetailsViewModel
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public decimal TotalAmount { get; set; }
        public string ShipAddress { get; set; } = string.Empty;
        public List<OrderItemDetailsViewModel> Items { get; set; } = new();
    }
}
