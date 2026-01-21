namespace ECommerceApp.Domain.Entities
{
    public class Inventory
    {
        public int Id { get; set; }
        public int ProductId { get; set; } //Foreign Key to Product
        public int StockQty { get; set; }
        public int RecordLvl { get; set; }
        public DateTime lastUpdate { get; set; }

        // Relationship: Inventory belongs to one Product
        public Product Product { get; set; }
    }
}
