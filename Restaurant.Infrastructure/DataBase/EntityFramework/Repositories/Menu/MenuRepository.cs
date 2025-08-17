using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Models.Menu;
using Restaurant.Domain.Repositories.Menu;
using Restaurant.Infrastructure.DataBase.EntityFramework.Context;
using Restaurant.Infrastructure.DataBase.EntityFramework.Extensions.Menu;

namespace Restaurant.Infrastructure.DataBase.EntityFramework.Repositories.Menu;

public class MenuRepository : IMenuRepository
{
    private readonly MenuDBcontext _context;

    public MenuRepository(MenuDBcontext context)
    {
        _context = context;
    }

    public async Task<MenuModel> InsertAsync(MenuModel model)
    {
        if (model.Id == 0)
        {
            var entity = model.ToEntity();
            _context.Menu.Add(entity);
            await _context.SaveChangesAsync();
            return entity.ToModel();
        }
        else
        {
            var entity = await _context.Menu.FindAsync(model.Id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Menu con Id {model.Id} no existe.");
            }
            _context.Entry(entity).CurrentValues.SetValues(model.ToEntity());
            await _context.SaveChangesAsync();
            return entity.ToModel();
        }
    }

    public Task<MenuModel> UpdateAsync(MenuModel model)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Menu.FindAsync(id);
        if (entity == null) return false;

        _context.Menu.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<MenuModel?> GetByIdAsync(int id)
    {
        var entity = await _context.Menu
            .Include(m => m.Dish)
            .Include(m => m.MealPeriod)
            .FirstOrDefaultAsync(x => x.Id == id);
        return entity?.ToModel();
    }

    public async Task<List<MenuModel>> GetAllAsync()
    {
        var entities = await _context.Menu
            .Include(m => m.Dish)
            .Include(m => m.MealPeriod)
            .ToListAsync();
        return entities.Select(entity => entity.ToModel()).ToList();
    }

    public async Task<bool> IsExistId(int id)
    {
        return await _context.Menu.AnyAsync(x => x.Id == id);
    }

    public async Task<bool> IsMenuDuplicate(DateTime menuDate, int idDish, int idMealPeriod, int id)
    {
        return await _context.Menu.AnyAsync(x => 
            x.Id != id && 
            x.MenuDate.Date == menuDate.Date && 
            x.IdDish == idDish && 
            x.IdMealPeriod == idMealPeriod);
    }

    public async Task<List<MenuModel>> GetMenuByDate(DateTime date)
    {
        var entities = await _context.Menu
            .Include(m => m.Dish)
            .Include(m => m.MealPeriod)
            .Where(x => x.MenuDate.Date == date.Date)
            .ToListAsync();
        return entities.Select(e => e.ToModel()).ToList();
    }

    public async Task<List<MenuModel>> GetMenuByDateAndMealPeriod(DateTime date, int idMealPeriod)
    {
        var entities = await _context.Menu
            .Include(m => m.Dish)
            .Include(m => m.MealPeriod)
            .Where(x => x.MenuDate.Date == date.Date && x.IdMealPeriod == idMealPeriod)
            .ToListAsync();
        return entities.Select(e => e.ToModel()).ToList();
    }
}