using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartFactoryDomain.Entities.Inventory;

namespace SmartFactoryData.EntityConfig.Inventory
{
    public class MaterialEntityConfig : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.ToTable("materials");

            builder.HasKey(m => m.Id);
            builder.Property(m => m.Name).IsRequired().HasMaxLength(100);
            builder.Property(m => m.Code).IsRequired().HasMaxLength(50);
            builder.Property(m => m.UnitPrice).HasColumnType("decimal(18,2)");
            builder.Property(m => m.StockQuantity).IsRequired();
            builder.Property(m => m.UnitOfMeasure).IsRequired().HasMaxLength(20);
            builder.Property(m => m.CreatedAt).IsRequired();
            builder.Property(m => m.UpdatedAt).IsRequired();

            builder.HasMany(m => m.ProductMaterials)
                   .WithOne(pm => pm.Material)
                   .HasForeignKey(pm => pm.MaterialId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
