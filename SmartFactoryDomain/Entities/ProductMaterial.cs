namespace SmartFactoryDomain.Entities
{
    public class ProductMaterial(int productId, int materialId, int quantity)
    {
        public int Id { get; set; }
        public int ProductId { get; set; } = productId;
        public Product? Product { get; set; } // Relationship with Product entity
        public int MaterialId { get; set; } = materialId;
        public Material? Material { get; set; } // Relationship with Material entity
        public int Quantity { get; set; } = quantity;
    }
}
