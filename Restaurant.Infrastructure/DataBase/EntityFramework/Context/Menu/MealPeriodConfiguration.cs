using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restaurant.Infrastructure.DataBase.EntityFramework.Entities.Menu;

namespace Restaurant.Infrastructure.DataBase.EntityFramework.Context.Menu;

public class MealPeriodConfiguration : IEntityTypeConfiguration<MealPeriodEntity>
{
    public void Configure(EntityTypeBuilder<MealPeriodEntity> builder)
    {
        builder.ToTable("mealPeriod","REST");
    }
}