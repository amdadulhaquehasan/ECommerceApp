namespace ECommerceApp.PresentationLayer.Modules.Orders.ViewModel
{
    public class OrderConfirmViewModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;
    }
}
