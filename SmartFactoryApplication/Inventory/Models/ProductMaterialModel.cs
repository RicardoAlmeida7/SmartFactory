﻿namespace SmartFactoryApplication.Inventory.Models
{
    public class ProductMaterialModel(int productId, int materialId, int quantity)
    {
        public int ProductId { get; set; } = productId;
        public int MaterialId { get; set; } = materialId;
        public int Quantity { get; set; } = quantity;
    }
}
