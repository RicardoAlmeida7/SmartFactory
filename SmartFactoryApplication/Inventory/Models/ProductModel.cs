namespace SmartFactoryApplication.Inventory.Models
{
    public class ProductModel
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Category { get; set; }
        public decimal? Price { get; set; }
        public int? StockQuantity { get; set; }
        public virtual List<ProductMaterialModel>? ProductMaterials { get; set; }

        public ProductModel() { }

        public ProductModel(int id, string name, string code, string category, decimal price, int stockQuantity)
        {
            Id = id;
            Name = name;
            Code = code;
            Category = category;
            Price = price;
            StockQuantity = stockQuantity;
        }
    }
}
