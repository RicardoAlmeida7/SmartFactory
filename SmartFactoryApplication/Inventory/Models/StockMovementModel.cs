using SmartFactoryDomain.Enums;

namespace SmartFactoryApplication.Inventory.Models
{
    public class StockMovementModel(int id, int materialId, int quantity, MovementType type, string description, DateTime movementDate)
    {
        public int Id { get; set; } = id;
        public int MaterialId { get; set; } = materialId;
        public int Quantity { get; set; } = quantity;
        public MovementType Type { get; set; } = type;
        public string Description { get; set; } = description;
        public DateTime MovementDate { get; set; } = movementDate;
    }
}
