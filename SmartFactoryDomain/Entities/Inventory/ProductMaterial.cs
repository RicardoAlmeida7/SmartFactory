namespace SmartFactoryDomain.Entities.Inventory
{
    public class ProductMaterial(int productId, int materialId, int quantity)
    {
        public int Id { get; set; }
        public int ProductId { get; set; } = productId;
        public virtual Product? Product { get; set; } // Relationship with Product entity
        public int MaterialId { get; set; } = materialId;
        public virtual Material? Material { get; set; } // Relationship with Material entity
        public int Quantity { get; set; } = quantity;
    }
}
