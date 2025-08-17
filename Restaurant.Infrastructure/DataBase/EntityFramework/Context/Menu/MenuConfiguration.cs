using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Infrastructure.DataBase.EntityFramework.Entities.Menu;

namespace Restaurant.Infrastructure.DataBase.EntityFramework.Context.Menu;

public class MenuConfiguration : IEntityTypeConfiguration<MenuEntity>
{
    public void Configure(EntityTypeBuilder<MenuEntity> builder)
    {
        builder.ToTable("menu", "REST");
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id)
            .HasColumnName("id");

        builder.Property(m => m.MenuDate)
            .HasColumnName("menuDate")
            .IsRequired();

        builder.Property(m => m.IdDish)
            .HasColumnName("idDish")
            .IsRequired();

        builder.Property(m => m.IdMealPeriod)
            .HasColumnName("idMealPeriod")
            .IsRequired();
        builder.HasOne(m => m.Dish)
            .WithMany() 
            .HasForeignKey(m => m.IdDish)
            .OnDelete(DeleteBehavior.Restrict); 

        builder.HasOne(m => m.MealPeriod)
            .WithMany()
            .HasForeignKey(m => m.IdMealPeriod)
            .OnDelete(DeleteBehavior.Restrict);
    }
}