using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SmartFactoryDomain.Entities.Inventory;

namespace SmartFactoryData.EntityConfig.Inventory
{
    public class ProductMaterialEntityConfig : IEntityTypeConfiguration<ProductMaterial>
    {
        public void Configure(EntityTypeBuilder<ProductMaterial> builder)
        {
            builder.ToTable("product_materials");

            builder.HasKey(pm => pm.Id);

            builder.HasOne(pm => pm.Product)
                   .WithMany(p => p.ProductMaterials)
                   .HasForeignKey(pm => pm.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pm => pm.Material)
                   .WithMany(m => m.ProductMaterials)
                   .HasForeignKey(pm => pm.MaterialId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.Property(pm => pm.Quantity).IsRequired();
        }
    }
}
