using Restaurant.Domain.Models.Menu;
using Restaurant.Infrastructure.DataBase.EntityFramework.Entities.Menu;

namespace Restaurant.Infrastructure.DataBase.EntityFramework.Extensions.Menu;

public static class MenuExtension
{
    public static MenuEntity ToEntity(this MenuModel model)
    {
        return new MenuEntity
        {
            Id = model.Id,
            MenuDate = model.MenuDate,
            Quantity = model.Quantity,
            IdDish = model.IdDish,
            IdMealPeriod = model.IdMealPeriod
        };
    }

    public static MenuModel ToModel(this MenuEntity entity)
    {
        var menuModel = new MenuModel(entity.Id, entity.MenuDate, entity.Quantity, entity.IdDish, entity.IdMealPeriod);
        if (entity.Dish != null)
        {
            menuModel.Dish = new DishModel(
                entity.Dish.Id,
                entity.Dish.Name,
                entity.Dish.Description,
                entity.Dish.Image,
                entity.Dish.Price
            );
        }
        if (entity.MealPeriod != null)
        {
            menuModel.MealPeriod = new MealPeriodModel(
                entity.MealPeriod.Id,
                entity.MealPeriod.NameMealPeriod,
                entity.MealPeriod.StartTime.ToString(@"hh\:mm"),
                entity.MealPeriod.EndTime.ToString(@"hh\:mm"),
                entity.MealPeriod.Color
            );
        }
        return menuModel;
    }
}