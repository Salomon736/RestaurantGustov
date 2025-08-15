using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Models.Menu;
using Restaurant.Domain.Repositories.Menu;
using Restaurant.Infrastructure.DataBase.EntityFramework.Context;
using Restaurant.Infrastructure.DataBase.EntityFramework.Extensions.Menu;

namespace Restaurant.Infrastructure.DataBase.EntityFramework.Repositories.Menu;

public class DishRepository : IDishRepository
{
    private readonly MenuDBcontext _context;

    public DishRepository(MenuDBcontext context)
    {
        _context = context;
    }

    public async Task<DishModel> InsertAsync(DishModel model)
    {
        if (model.Id == 0)
        {
            var entity = model.ToEntity();
            _context.Dish.Add(entity);
            await _context.SaveChangesAsync();
            return entity.ToModel();
        }
        else
        {
            var entity = await _context.Dish.FindAsync(model.Id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Dish con Id {model.Id} no existe.");
            }
            _context.Entry(entity).CurrentValues.SetValues(model.ToEntity());
            await _context.SaveChangesAsync();
            return entity.ToModel();
        }
    }

    public Task<DishModel> UpdateAsync(DishModel model)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Dish.FindAsync(id);
        if (entity == null)
            return false;

        try
        {
            _context.Dish.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (DbUpdateException ex)
        {
            if (ex.InnerException?.Message.Contains("REFERENCE constraint") == true)
                throw new InvalidOperationException("No se puede eliminar el plato porque está siendo utilizado en menús");
            throw;
        }
    }

    public async Task<DishModel?> GetByIdAsync(int id)
    {
        var entity = await _context.Dish.FindAsync(id);
        return entity?.ToModel();
    }

    public async Task<List<DishModel>> GetAllAsync()
    {
        var entities = await _context.Dish.ToListAsync();
        return entities.ToModelList();
    }

    public async Task<bool> IsExistId(int id)
    {
        return await _context.Dish.AnyAsync(d => d.Id == id);
    }

    public async Task<bool> IsNameDishDuplicate(string name, int id)
    {
        return await _context.Dish.AnyAsync(d => d.Name == name && d.Id != id);
    }
}