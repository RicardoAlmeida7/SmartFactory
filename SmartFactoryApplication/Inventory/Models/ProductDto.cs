namespace SmartFactoryApplication.Inventory.Models
{
    public class ProductDto(int id, string name, string code, string category, decimal price, int stockQuantity)
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string Code { get; set; } = code;
        public string Category { get; set; } = category;
        public decimal Price { get; set; } = price;
        public int StockQuantity { get; set; } = stockQuantity;
    }
}
