using Microsoft.EntityFrameworkCore;
using SmartFactoryData.EntityConfig.Inventory;
using SmartFactoryDomain.Entities.Inventory;

namespace SmartFactoryData.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Entity type configuration
            modelBuilder.ApplyConfiguration(new ProductEntityConfig());
            modelBuilder.ApplyConfiguration(new MaterialEntityConfig());
            modelBuilder.ApplyConfiguration(new ProductMaterialEntityConfig());
            modelBuilder.ApplyConfiguration(new ProductStockMovementEntityConfig());
            modelBuilder.ApplyConfiguration(new MaterialStockMovementEntityConfig());
        }

        // Inventory
        public DbSet<Product> Products { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<ProductMaterial> ProductMaterials { get; set; }
        public DbSet<ProductStockMovement> ProductStockMovements { get; set; }
        public DbSet<MaterialStockMovement> MaterialStockMovements { get; set; }
    }
}
