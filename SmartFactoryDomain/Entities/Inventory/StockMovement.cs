using SmartFactoryDomain.Enums;

namespace SmartFactoryDomain.Entities.Inventory
{
    public class StockMovement(int materialId, int quantity, MovementType type, string description)
    {
        public int Id { get; set; }
        public int MaterialId { get; set; } = materialId;
        public Material? Material { get; set; }
        public DateTime MovementDate { get; set; } = DateTime.UtcNow;
        public int Quantity { get; set; } = quantity;
        public MovementType Type { get; set; } = type;
        public string Description { get; set; } = description;
    }
}
