using Restaurant.Domain.Models.Menu;

namespace Restaurant.Domain.Repositories.Menu;

public interface IDishRepository : IGenericRepository<DishModel>
{
    Task<bool> IsNameDishDuplicate(string name, int id);
}