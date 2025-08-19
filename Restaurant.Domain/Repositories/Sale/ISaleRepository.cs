using Restaurant.Domain.Models.Sale;

namespace Restaurant.Domain.Repositories.Sale;

public interface ISaleRepository : IGenericRepository<SaleModel>
{
    Task<List<SaleModel>> GetSalesByDate(DateTime date);
    Task<List<SaleModel>> GetSalesByDateRange(DateTime startDate, DateTime endDate);
    Task<List<SaleModel>> GetSalesByMenu(int idMenu);
    Task<decimal> GetTotalSalesByDate(DateTime date);
    Task<decimal> GetTotalSalesByDateRange(DateTime startDate, DateTime endDate);
    Task<bool> ValidateMenuAvailability(int idMenu, int quantityRequested);
}