using Restaurant.Domain.Models.MenuCharge;
using Restaurant.Domain.Dtos.MenuCharge;

namespace Restaurant.Application.Test.Builders
{
    public static class MenuBuilder
    {
        public static MenuModel GetValidMenu()
        {
            return new MenuModel(
                id: 0,
                menuDate: DateTime.Today.AddDays(1),
                idDish: 1,
                idMealPeriod: 1,
                idLounge: 1,
                createdAt: DateTime.UtcNow
            );
        }

        public static MenuModel GetMenuWithInvalidDish()
        {
            return new MenuModel(
                id: 0,
                menuDate: DateTime.Today.AddDays(1),
                idDish: 999, // ID que no existe
                idMealPeriod: 1,
                idLounge: 1,
                createdAt: DateTime.UtcNow
            );
        }

        public static MenuModel GetMenuWithInvalidMealPeriod()
        {
            return new MenuModel(
                id: 0,
                menuDate: DateTime.Today.AddDays(1),
                idDish: 1,
                idMealPeriod: 999, // ID que no existe
                idLounge: 1,
                createdAt: DateTime.UtcNow
            );
        }

        public static MenuModel GetMenuWithInvalidLounge()
        {
            return new MenuModel(
                id: 0,
                menuDate: DateTime.Today.AddDays(1),
                idDish: 1,
                idMealPeriod: 1,
                idLounge: 999, // ID que no existe
                createdAt: DateTime.UtcNow
            );
        }

        public static MenuModel GetMenuWithPastDate()
        {
            return new MenuModel(
                id: 0,
                menuDate: DateTime.Today.AddDays(-1), // Fecha en el pasado
                idDish: 1,
                idMealPeriod: 1,
                idLounge: 1,
                createdAt: DateTime.UtcNow
            );
        }

        public static MenuMatrixDto GetValidMenuMatrix()
        {
            return new MenuMatrixDto
            {
                DishId = 1,
                LoungeId = 1,
                Selections = new List<MenuDatePeriodDto>
                {
                    new MenuDatePeriodDto
                    {
                        Date = DateTime.Today.AddDays(1),
                        MealPeriodId = 1
                    }
                }
            };
        }
    }
}
