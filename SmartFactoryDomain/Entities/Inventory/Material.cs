namespace SmartFactoryDomain.Entities.Inventory
{
    public class Material(string name, string code, decimal unitPrice, int stockQuantity, string unitOfMeasure)
    {
        public int Id { get; set; }
        public string Name { get; set; } = name;
        public string Code { get; set; } = code;
        public decimal UnitPrice { get; set; } = unitPrice;
        public int StockQuantity { get; set; } = stockQuantity;
        public string UnitOfMeasure { get; set; } = unitOfMeasure;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public void UpdateStock(int quantity)
        {
            if (quantity < 0)
                throw new InvalidOperationException("Stock quantity cannot be negative.");

            StockQuantity = quantity;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
