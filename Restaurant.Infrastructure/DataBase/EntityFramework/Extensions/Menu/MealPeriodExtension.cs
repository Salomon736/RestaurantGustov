using Restaurant.Domain.Models.Menu;
using Restaurant.Infrastructure.DataBase.EntityFramework.Entities.Menu;

namespace Restaurant.Infrastructure.DataBase.EntityFramework.Extensions.Menu;

public static class MealPeriodExtension
{
    public static MealPeriodEntity ToEntity(this MealPeriodModel model)
    {
        return new MealPeriodEntity
        {
            Id = model.Id,
            NameMealPeriod = model.NameMealPeriod,
            StartTime = TimeSpan.Parse(model.StartTime),
            EndTime = TimeSpan.Parse(model.EndTime),
            Color = model.Color
        };
    }

    public static MealPeriodModel ToModel(this MealPeriodEntity entity)
    {
        return new MealPeriodModel(
            entity.Id,
            entity.NameMealPeriod,
            entity.StartTime.ToString(@"hh\:mm"),
            entity.EndTime.ToString(@"hh\:mm"),
            entity.Color
        );
    }
}