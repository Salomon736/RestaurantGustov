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
    private readonly IMealPeriodRepository _mealPeriodRepository;

    public SaleService(ISaleRepository saleRepository, IMenuRepository menuRepository, IMealPeriodRepository mealPeriodRepository)
    {
        _saleRepository = saleRepository;
        _menuRepository = menuRepository;
        _mealPeriodRepository = mealPeriodRepository;
    }

    public async Task<Result<object>> SaveSale(SaleModel model)
    {
        if (model.HasErrors())
            return Result<object>.Failure(model.GetAllMessageErrors(), HttpStatusCode.BadRequest);

        if (!await _menuRepository.IsExistId(model.IdMenu))
            return Result<object>.Failure("El menú seleccionado no existe", HttpStatusCode.BadRequest);

        var menu = await _menuRepository.GetByIdAsync(model.IdMenu);
        if (menu == null)
            return Result<object>.Failure("El menú seleccionado no existe", HttpStatusCode.BadRequest);

        var mealPeriod = await _mealPeriodRepository.GetByIdAsync(menu.IdMealPeriod);
        if (mealPeriod != null)
        {
            var menuDate = menu.MenuDate.Date;
            var currentDate = DateTime.Now.Date;
            
            if (menuDate == currentDate) // Solo validar hora si es para hoy
            {
                if (TimeSpan.TryParse(mealPeriod.EndTime, out var endTimeSpan))
                {
                    var currentTime = DateTime.Now.TimeOfDay;
                    if (currentTime > endTimeSpan)
                    {
                        return Result<object>.Failure(
                            $"No se puede realizar la venta. El horario de {mealPeriod.NameMealPeriod} ya terminó a las {endTimeSpan}",
                            HttpStatusCode.BadRequest
                        );
                    }
                }
                else
                {
                    return Result<object>.Failure(
                        $"El formato de hora de fin del periodo {mealPeriod.NameMealPeriod} es inválido.",
                        HttpStatusCode.InternalServerError
                    );
                }
            }
            else if (menuDate < currentDate) // Validar que no sea una fecha pasada
            {
                return Result<object>.Failure(
                    $"No se puede realizar la venta. La fecha del menú ({menuDate:yyyy-MM-dd}) ya pasó.",
                    HttpStatusCode.BadRequest
                );
            }
        }
        if (menu.Quantity < model.QuantitySold)
            return Result<object>.Failure($"No hay suficiente cantidad disponible. Stock actual: {menu.Quantity}, solicitado: {model.QuantitySold}", HttpStatusCode.BadRequest);

        if (menu.Dish != null)
        {
            var expectedTotal = menu.Dish.Price * model.QuantitySold;
            if (Math.Abs(model.TotalPrice - expectedTotal) > 0.01m)
                return Result<object>.Failure("El precio total no coincide con el cálculo esperado", HttpStatusCode.BadRequest);
        }

        try
        {
            await _saleRepository.InsertAsync(model);
            
            menu.Quantity -= model.QuantitySold;
            await _menuRepository.InsertAsync(menu);

            return Result<object>.Success(new { message = "Venta registrada exitosamente", stockRestante = menu.Quantity }, HttpStatusCode.OK);
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