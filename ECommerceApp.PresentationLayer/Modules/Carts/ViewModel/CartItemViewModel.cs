namespace ECommerceApp.PresentationLayer.Modules.Carts.ViewModel
{
    public class CartItemViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal {  get; set; }
    }
}
