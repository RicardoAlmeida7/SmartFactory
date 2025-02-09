namespace SmartFactoryDomain.Entities.Inventory
{
    public class Material
    {
        public Material() { }

        public Material(string name, string code, decimal unitPrice, int stockQuantity, string unitOfMeasure)
        {
            Name = name;
            Code = code;
            UnitPrice = unitPrice;
            StockQuantity = stockQuantity;
            UnitOfMeasure = unitOfMeasure;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public decimal UnitPrice { get; set; }
        public int StockQuantity { get; set; }
        public string UnitOfMeasure { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public virtual List<ProductMaterial> ProductMaterials { get; set; } = [];
    }
}
