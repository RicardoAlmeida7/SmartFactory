using SmartFactoryDomain.Enums;

namespace SmartFactoryDomain.Entities.Inventory
{
    public abstract class StockMovementBase(int quantity, MovementType type, string description)
    {
        public int Id { get; set; }
        public DateTime MovementDate { get; set; } = DateTime.UtcNow;
        public int Quantity { get; set; } = quantity;
        public MovementType Type { get; set; } = type;
        public string Description { get; set; } = description;
    }
}
