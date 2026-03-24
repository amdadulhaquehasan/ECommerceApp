namespace ECommerceApp.PresentationLayer.Modules.Carts.ViewModel
{
    public class CartViewModel
    {
        public IReadOnlyList<CartItemViewModel> Items { get; set; } = Array.Empty<CartItemViewModel>();
        public decimal GrandTotal { get; set; }
        public int TotalItems { get; set; }
    }
}
