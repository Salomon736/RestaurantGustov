using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Models.Menu;
using Restaurant.Domain.Repositories.Menu;
using Restaurant.Infrastructure.DataBase.EntityFramework.Context;
using Restaurant.Infrastructure.DataBase.EntityFramework.Extensions.Menu;

namespace Restaurant.Infrastructure.DataBase.EntityFramework.Repositories.Menu;

public class MealPeriodRepository : IMealPeriodRepository
{
    private readonly MenuDBcontext _context;

    public MealPeriodRepository(MenuDBcontext context)
    {
        _context = context;
    }

    public async Task<MealPeriodModel> InsertAsync(MealPeriodModel model)
    {
        if (model.Id == 0)
        {
            var entity = model.ToEntity();
            _context.MealPeriod.Add(entity);
            await _context.SaveChangesAsync();
            return entity.ToModel();
        }
        else
        {
            var entity = await _context.MealPeriod.FindAsync(model.Id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"MealPeriod con Id {model.Id} no existe.");
            }
            _context.Entry(entity).CurrentValues.SetValues(model.ToEntity());
            await _context.SaveChangesAsync();
            return entity.ToModel();
        }
    }
    public Task<MealPeriodModel> UpdateAsync(MealPeriodModel model)
    {
        throw new NotImplementedException();
    }
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.MealPeriod.FindAsync(id);
        if (entity == null) return false;

        _context.MealPeriod.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<MealPeriodModel?> GetByIdAsync(int id)
    {
        var entity = await _context.MealPeriod.FindAsync(id);
        return entity?.ToModel();
    }

    public async Task<List<MealPeriodModel>> GetAllAsync()
    {
        var entities = await _context.MealPeriod.ToListAsync();
        return entities.Select(e => e.ToModel()).ToList();
    }

    public async Task<bool> IsExistId(int id)
    {
        return await _context.MealPeriod.AnyAsync(x => x.Id == id);
    }

    public async Task<bool> IsMealTypeDuplicate(string mealType, int id)
    {
        return await _context.MealPeriod.AnyAsync(x => x.NameMealPeriod == mealType && x.Id != id);
    }

    public async Task<bool> HasTimeOverlap(TimeSpan startTime, TimeSpan endTime, int id)
    {
        return await _context.MealPeriod.AnyAsync(x => 
            x.Id != id && 
            ((startTime >= x.StartTime && startTime < x.EndTime) ||
             (endTime > x.StartTime && endTime <= x.EndTime) ||
             (startTime <= x.StartTime && endTime >= x.EndTime)));
    }
}