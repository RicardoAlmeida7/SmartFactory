using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SmartFactoryDomain.Entities.Inventory;

namespace SmartFactoryData.EntityConfig.Inventory
{
    public class MaterialStockMovementEntityConfig : IEntityTypeConfiguration<MaterialStockMovement>
    {
        public void Configure(EntityTypeBuilder<MaterialStockMovement> builder)
        {
            builder.ToTable("material_stock_movements");

            builder.HasKey(msm => msm.Id);

            builder.Property(msm => msm.MovementDate)
                   .IsRequired()
                   .HasColumnType("datetime");

            builder.Property(msm => msm.Quantity)
                   .IsRequired()
                   .HasColumnType("int");

            builder.Property(msm => msm.Type)
                   .IsRequired()
                   .HasConversion<int>();

            builder.Property(msm => msm.Description)
                   .HasMaxLength(255)
                   .IsRequired(false);

            builder.HasOne(msm => msm.Material)
                   .WithMany()
                   .HasForeignKey(msm => msm.MaterialId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
