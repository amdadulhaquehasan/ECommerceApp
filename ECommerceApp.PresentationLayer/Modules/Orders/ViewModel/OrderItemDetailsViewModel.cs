namespace ECommerceApp.PresentationLayer.Modules.Orders.ViewModel
{
    public class OrderItemDetailsViewModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal SubTotal { get; set; }
    }
}
