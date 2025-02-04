using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SmartFactoryDomain.Entities.Inventory;

namespace SmartFactoryData.EntityConfig.Inventory
{
    public class ProductStockMovementEntityConfig : IEntityTypeConfiguration<ProductStockMovement>
    {
        public void Configure(EntityTypeBuilder<ProductStockMovement> builder)
        {
            builder.ToTable("product_stock_movements");

            builder.HasKey(psm => psm.Id);

            builder.Property(psm => psm.MovementDate)
                   .IsRequired()
                   .HasColumnType("datetime");

            builder.Property(psm => psm.Quantity)
                   .IsRequired()
                   .HasColumnType("int");

            builder.Property(psm => psm.Type)
                   .IsRequired()
                   .HasConversion<int>();

            builder.Property(psm => psm.Description)
                   .HasMaxLength(255)
                   .IsRequired(false);

            builder.HasOne(psm => psm.Product)
                   .WithMany()
                   .HasForeignKey(psm => psm.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
