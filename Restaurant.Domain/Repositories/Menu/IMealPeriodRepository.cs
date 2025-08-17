using Restaurant.Domain.Models.Menu;

namespace Restaurant.Domain.Repositories.Menu;

public interface IMealPeriodRepository : IGenericRepository<MealPeriodModel>
{
    Task<bool> IsMealTypeDuplicate(string nameMealPeriod, int id);
    Task<bool> HasTimeOverlap(TimeSpan startTime, TimeSpan endTime, int id);
}