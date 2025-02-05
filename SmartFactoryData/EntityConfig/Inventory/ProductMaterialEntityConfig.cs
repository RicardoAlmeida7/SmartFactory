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

            builder.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.HasOne(pm => pm.Product)
                .WithMany(p => p.ProductMaterials)
                .HasForeignKey(pm => pm.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pm => pm.Material)
                .WithMany(m => m.ProductMaterials)
                .HasForeignKey(pm => pm.MaterialId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(pm => pm.Quantity).
                IsRequired();
        }
    }
}
