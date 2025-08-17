using Restaurant.Domain.Models.Menu;

namespace Restaurant.Domain.Repositories.Menu;

public interface IMenuRepository : IGenericRepository<MenuModel>
{
    Task<bool> IsMenuDuplicate(DateTime menuDate, int idDish, int idMealPeriod, int id);
    Task<List<MenuModel>> GetMenuByDate(DateTime date);
    Task<List<MenuModel>> GetMenuByDateAndMealPeriod(DateTime date, int idMealPeriod);
}