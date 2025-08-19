using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Infrastructure.DataBase.EntityFramework.Entities.Sale;

namespace Restaurant.Infrastructure.DataBase.EntityFramework.Context.Sale;

public class SaleConfiguration : IEntityTypeConfiguration<SaleEntity>
{
    public void Configure(EntityTypeBuilder<SaleEntity> builder)
    {
        builder.ToTable("sale", "REST");
        builder.HasKey(s => s.Id);
        
        builder.Property(s => s.Id)
            .HasColumnName("id");
        
        builder.Property(s => s.IdMenu)
            .HasColumnName("idMenu")
            .IsRequired();

        builder.Property(s => s.QuantitySold)
            .HasColumnName("quantitySold")
            .IsRequired();

        builder.Property(s => s.TotalPrice)
            .HasColumnName("totalPrice")
            .HasColumnType("decimal(10,2)")
            .IsRequired();

        builder.HasOne(s => s.Menu)
            .WithMany()
            .HasForeignKey(s => s.IdMenu)
            .OnDelete(DeleteBehavior.Restrict);
    }
}