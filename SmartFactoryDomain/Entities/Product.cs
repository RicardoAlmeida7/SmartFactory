namespace SmartFactoryDomain.Entities
{
    public class Product(string name, string code, string category, decimal price, int stockQuantity)
    {
        public int Id { get; set; }
        public string Name { get; set; } = name;
        public string Code { get; set; } = code;
        public string Category { get; set; } = category;
        public decimal Price { get; set; } = price;
        public int StockQuantity { get; set; } = stockQuantity;
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
