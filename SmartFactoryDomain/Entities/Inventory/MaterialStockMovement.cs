using SmartFactoryDomain.Enums;

namespace SmartFactoryDomain.Entities.Inventory
{
    public class MaterialStockMovement(int materialId, int quantity, MovementType type, string description = "") : StockMovementBase(quantity, type, description)
    {
        public int MaterialId { get; set; } = materialId;
        public Material? Material { get; set; }
    }
}
