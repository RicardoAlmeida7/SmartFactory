namespace SmartFactoryApplication.Inventory.Models
{
    public class MaterialDto(int id, string name, string code, decimal unitPrice, int stockQuantity, string unitOfMeasure)
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string Code { get; set; } = code;
        public decimal UnitPrice { get; set; } = unitPrice;
        public int StockQuantity { get; set; } = stockQuantity;
        public string UnitOfMeasure { get; set; } = unitOfMeasure;
    }
}
