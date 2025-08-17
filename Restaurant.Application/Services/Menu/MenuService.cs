using System.Net;
using Restaurant.Domain.Models.Menu;
using Restaurant.Domain.Repositories.Menu;
using Restaurant.Domain.Responses;

namespace Restaurant.Application.Services.Menu;

public class MenuService
{
    private readonly IMenuRepository _menuRepository;
    private readonly IDishRepository _dishRepository;
    private readonly IMealPeriodRepository _mealPeriodRepository;

    public MenuService(IMenuRepository menuRepository, IDishRepository dishRepository, IMealPeriodRepository mealPeriodRepository)
    {
        _menuRepository = menuRepository;
        _dishRepository = dishRepository;
        _mealPeriodRepository = mealPeriodRepository;
    }

    public async Task<Result<object>> SaveMenu(MenuModel model)
    {
        if (model.HasErrors())
            return Result<object>.Failure(model.GetAllMessageErrors(), HttpStatusCode.BadRequest);
        if (!await _dishRepository.IsExistId(model.IdDish))
            return Result<object>.Failure("El plato seleccionado no existe", HttpStatusCode.BadRequest);

        if (!await _mealPeriodRepository.IsExistId(model.IdMealPeriod))
            return Result<object>.Failure("El período de comida seleccionado no existe", HttpStatusCode.BadRequest);

        if (await _menuRepository.IsMenuDuplicate(model.MenuDate, model.IdDish, model.IdMealPeriod, model.Id))
            return Result<object>.Failure("Ya existe este plato para el mismo período y fecha", HttpStatusCode.BadRequest);

        try
        {
            await _menuRepository.InsertAsync(model);
            return Result<object>.Success(new { }, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return Result<object>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<List<MenuModel>>> GetAllMenus()
    {
        try
        {
            var menus = await _menuRepository.GetAllAsync();
            return Result<List<MenuModel>>.Success(menus, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return Result<List<MenuModel>>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<MenuModel>> GetMenuById(int id)
    {
        try
        {
            var menu = await _menuRepository.GetByIdAsync(id);
            if (menu == null)
                return Result<MenuModel>.Failure("Menú no encontrado", HttpStatusCode.NotFound);

            return Result<MenuModel>.Success(menu, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return Result<MenuModel>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<object>> DeleteMenu(int id)
    {
        try
        {
            var deleted = await _menuRepository.DeleteAsync(id);
            if (!deleted)
                return Result<object>.Failure("Menú no encontrado", HttpStatusCode.NotFound);

            return Result<object>.Success(new { }, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return Result<object>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<List<MenuModel>>> GetMenuByDate(DateTime date)
    {
        try
        {
            var menus = await _menuRepository.GetMenuByDate(date);
            return Result<List<MenuModel>>.Success(menus, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return Result<List<MenuModel>>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }
}