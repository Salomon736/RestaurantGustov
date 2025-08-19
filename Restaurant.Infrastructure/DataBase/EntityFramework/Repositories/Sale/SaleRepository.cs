using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Models.Sale;
using Restaurant.Domain.Repositories.Sale;
using Restaurant.Infrastructure.DataBase.EntityFramework.Context;
using Restaurant.Infrastructure.DataBase.EntityFramework.Extensions.Sale;

namespace Restaurant.Infrastructure.DataBase.EntityFramework.Repositories.Sale;

public class SaleRepository : ISaleRepository
{
    private readonly MenuDBcontext _context;

    public SaleRepository(MenuDBcontext context)
    {
        _context = context;
    }

    public async Task<SaleModel> InsertAsync(SaleModel model)
    {
        if (model.Id == 0)
        {
            var entity = model.ToEntity();
            _context.Sale.Add(entity);
            await _context.SaveChangesAsync();
            return entity.ToModel();
        }
        else
        {
            var entity = await _context.Sale.FindAsync(model.Id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Venta con Id {model.Id} no existe.");
            }
            _context.Entry(entity).CurrentValues.SetValues(model.ToEntity());
            await _context.SaveChangesAsync();
            return entity.ToModel();
        }
    }

    public Task<SaleModel> UpdateAsync(SaleModel model)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Sale.FindAsync(id);
        if (entity == null) return false;

        _context.Sale.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<SaleModel?> GetByIdAsync(int id)
    {
        var entity = await _context.Sale
            .Include(s => s.Menu)
                .ThenInclude(m => m.Dish)
            .Include(s => s.Menu)
                .ThenInclude(m => m.MealPeriod)
            .FirstOrDefaultAsync(x => x.Id == id);
        return entity?.ToModel();
    }

    public async Task<List<SaleModel>> GetAllAsync()
    {
        var entities = await _context.Sale
            .Include(s => s.Menu)
                .ThenInclude(m => m.Dish)
            .Include(s => s.Menu)
                .ThenInclude(m => m.MealPeriod)
            .ToListAsync();
        return entities.ToModelList();
    }

    public async Task<bool> IsExistId(int id)
    {
        return await _context.Sale.AnyAsync(x => x.Id == id);
    }

    public async Task<List<SaleModel>> GetSalesByDate(DateTime date)
    {
        var entities = await _context.Sale
            .Include(s => s.Menu)
                .ThenInclude(m => m.Dish)
            .Include(s => s.Menu)
                .ThenInclude(m => m.MealPeriod)
            .Where(x => x.createdAt.Date == date.Date)
            .ToListAsync();
        return entities.ToModelList();
    }

    public async Task<List<SaleModel>> GetSalesByDateRange(DateTime startDate, DateTime endDate)
    {
        var entities = await _context.Sale
            .Include(s => s.Menu)
                .ThenInclude(m => m.Dish)
            .Include(s => s.Menu)
                .ThenInclude(m => m.MealPeriod)
            .Where(x => x.createdAt.Date >= startDate.Date && x.createdAt.Date <= endDate.Date)
            .ToListAsync();
        return entities.ToModelList();
    }

    public async Task<List<SaleModel>> GetSalesByMenu(int idMenu)
    {
        var entities = await _context.Sale
            .Include(s => s.Menu)
                .ThenInclude(m => m.Dish)
            .Include(s => s.Menu)
                .ThenInclude(m => m.MealPeriod)
            .Where(x => x.IdMenu == idMenu)
            .ToListAsync();
        return entities.ToModelList();
    }

    public async Task<decimal> GetTotalSalesByDate(DateTime date)
    {
        return await _context.Sale
            .Where(x => x.createdAt.Date == date.Date)
            .SumAsync(x => x.TotalPrice);
    }

    public async Task<decimal> GetTotalSalesByDateRange(DateTime startDate, DateTime endDate)
    {
        return await _context.Sale
            .Where(x => x.createdAt.Date >= startDate.Date && x.createdAt.Date <= endDate.Date)
            .SumAsync(x => x.TotalPrice);
    }

    public async Task<bool> ValidateMenuAvailability(int idMenu, int quantityRequested)
    {
        var menu = await _context.Menu.FindAsync(idMenu);
        if (menu == null) return false;

        var totalSold = await _context.Sale
            .Where(x => x.IdMenu == idMenu && x.createdAt.Date == DateTime.Today)
            .SumAsync(x => x.QuantitySold);

        return (totalSold + quantityRequested) <= menu.Quantity;
    }
}