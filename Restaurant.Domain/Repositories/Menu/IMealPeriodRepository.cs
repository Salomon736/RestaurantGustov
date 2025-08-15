using Restaurant.Domain.Models.Menu;

namespace Restaurant.Domain.Repositories.Menu;

public interface IMealPeriodRepository : IGenericRepository<MealPeriodModel>
{
    Task<bool> IsMealTypeDuplicate(string mealType, int id);
    Task<bool> HasTimeOverlap(TimeOnly startTime, TimeOnly endTime, int id);
}