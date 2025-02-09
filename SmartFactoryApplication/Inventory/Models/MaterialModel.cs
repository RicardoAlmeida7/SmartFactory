namespace SmartFactoryApplication.Inventory.Models
{
    public class MaterialModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? StockQuantity { get; set; }
        public string? UnitOfMeasure { get; set; }

        public MaterialModel() { }

        public MaterialModel(int? id, string? name, string? code, decimal? unitPrice, int? stockQuantity, string? unitOfMeasure)
        {
            Id = id;
            Name = name;
            Code = code;
            UnitPrice = unitPrice;
            StockQuantity = stockQuantity;
            UnitOfMeasure = unitOfMeasure;
        }
    }
}
