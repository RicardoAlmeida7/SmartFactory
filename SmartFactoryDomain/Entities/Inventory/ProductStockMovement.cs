using SmartFactoryDomain.Enums;

namespace SmartFactoryDomain.Entities.Inventory
{
    public class ProductStockMovement(int productId, int quantity, MovementType type, string description = "") 
        : StockMovementBase(quantity, type, description)
    {
        public int ProductId { get; set; } = productId;
        public Product? Product { get; set; }
    }
}
