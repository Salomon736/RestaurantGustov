using System.Net;
using Restaurant.Domain.Models.Sale;
using Restaurant.Domain.Repositories.Menu;
using Restaurant.Domain.Repositories.Sale;
using Restaurant.Domain.Responses;

namespace Restaurant.Application.Services.Sale;

public class SaleService
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMenuRepository _menuRepository;

    public SaleService(ISaleRepository saleRepository, IMenuRepository menuRepository)
    {
        _saleRepository = saleRepository;
        _menuRepository = menuRepository;
    }

    public async Task<Result<object>> SaveSale(SaleModel model)
    {
        if (model.HasErrors())
            return Result<object>.Failure(model.GetAllMessageErrors(), HttpStatusCode.BadRequest);

        if (!await _menuRepository.IsExistId(model.IdMenu))
            return Result<object>.Failure("El menú seleccionado no existe", HttpStatusCode.BadRequest);

        if (!await _saleRepository.ValidateMenuAvailability(model.IdMenu, model.QuantitySold))
            return Result<object>.Failure("No hay suficiente cantidad disponible en el menú", HttpStatusCode.BadRequest);

        var menu = await _menuRepository.GetByIdAsync(model.IdMenu);
        if (menu != null && menu.Dish != null)
        {
            var expectedTotal = menu.Dish.Price * model.QuantitySold;
            if (Math.Abs(model.TotalPrice - expectedTotal) > 0.01m)
                return Result<object>.Failure("El precio total no coincide con el cálculo esperado", HttpStatusCode.BadRequest);
        }

        try
        {
            await _saleRepository.InsertAsync(model);
            return Result<object>.Success(new { }, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return Result<object>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<List<SaleModel>>> GetAllSales()
    {
        try
        {
            var sales = await _saleRepository.GetAllAsync();
            return Result<List<SaleModel>>.Success(sales, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return Result<List<SaleModel>>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<SaleModel>> GetSaleById(int id)
    {
        try
        {
            var sale = await _saleRepository.GetByIdAsync(id);
            if (sale == null)
                return Result<SaleModel>.Failure("Venta no encontrada", HttpStatusCode.NotFound);

            return Result<SaleModel>.Success(sale, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return Result<SaleModel>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<object>> DeleteSale(int id)
    {
        try
        {
            var deleted = await _saleRepository.DeleteAsync(id);
            if (!deleted)
                return Result<object>.Failure("Venta no encontrada", HttpStatusCode.NotFound);

            return Result<object>.Success(new { }, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return Result<object>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<List<SaleModel>>> GetSalesByDate(DateTime date)
    {
        try
        {
            var sales = await _saleRepository.GetSalesByDate(date);
            return Result<List<SaleModel>>.Success(sales, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return Result<List<SaleModel>>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<List<SaleModel>>> GetSalesByDateRange(DateTime startDate, DateTime endDate)
    {
        try
        {
            if (startDate > endDate)
                return Result<List<SaleModel>>.Failure("La fecha de inicio no puede ser mayor a la fecha de fin", HttpStatusCode.BadRequest);

            var sales = await _saleRepository.GetSalesByDateRange(startDate, endDate);
            return Result<List<SaleModel>>.Success(sales, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return Result<List<SaleModel>>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<List<SaleModel>>> GetSalesByMenu(int idMenu)
    {
        try
        {
            if (!await _menuRepository.IsExistId(idMenu))
                return Result<List<SaleModel>>.Failure("El menú especificado no existe", HttpStatusCode.BadRequest);

            var sales = await _saleRepository.GetSalesByMenu(idMenu);
            return Result<List<SaleModel>>.Success(sales, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return Result<List<SaleModel>>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<decimal>> GetTotalSalesByDate(DateTime date)
    {
        try
        {
            var total = await _saleRepository.GetTotalSalesByDate(date);
            return Result<decimal>.Success(total, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return Result<decimal>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<decimal>> GetTotalSalesByDateRange(DateTime startDate, DateTime endDate)
    {
        try
        {
            if (startDate > endDate)
                return Result<decimal>.Failure("La fecha de inicio no puede ser mayor a la fecha de fin", HttpStatusCode.BadRequest);

            var total = await _saleRepository.GetTotalSalesByDateRange(startDate, endDate);
            return Result<decimal>.Success(total, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return Result<decimal>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }
}