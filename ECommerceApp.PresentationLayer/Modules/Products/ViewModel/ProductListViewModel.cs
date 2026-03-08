using System.ComponentModel.DataAnnotations;

namespace ECommerceApp.PresentationLayer.Modules.Products.ViewModel
{
    public class ProductListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string SKU { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
