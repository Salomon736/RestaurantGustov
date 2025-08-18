using System.Net;
using Restaurant.Domain.Models.Menu;
using Restaurant.Domain.Repositories.Menu;
using Restaurant.Domain.Responses;

namespace Restaurant.Application.Services.Menu;

public class DishService
{
    private readonly IDishRepository _dishRepository;

    public DishService(IDishRepository dishRepository)
    {
        _dishRepository = dishRepository;
    }

    public async Task<Result<object>> SaveDish(DishModel model)
    {
        if (model.HasErrors())
            return Result<object>.Failure(model.GetAllMessageErrors(), HttpStatusCode.BadRequest);
        
        if (await _dishRepository.IsNameDishDuplicate(model.Name, model.Id))
            return Result<object>.Failure("Ya existe un plato con ese nombre", HttpStatusCode.BadRequest);

        try
        {
            DishModel savedDish;
            if (model.Id == 0)
            {
                savedDish = await _dishRepository.InsertAsync(model);
                return Result<object>.Success(new { id = savedDish.Id, message = "Plato creado exitosamente" }, HttpStatusCode.Created);
            }
            else
            {
                if (!await _dishRepository.IsExistId(model.Id))
                    return Result<object>.Failure("El plato no existe", HttpStatusCode.NotFound);

                savedDish = await _dishRepository.InsertAsync(model);
                return Result<object>.Success(new { id = savedDish.Id, message = "Plato actualizado exitosamente" }, HttpStatusCode.OK);
            }
        }
        catch (Exception ex)
        {
            return Result<object>.Failure($"Error interno: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<List<DishModel>>> GetAllDishes()
    {
        try
        {
            var dishes = await _dishRepository.GetAllAsync();
            return Result<List<DishModel>>.Success(dishes, HttpStatusCode.OK );
        }
        catch (Exception ex)
        {
            return Result<List<DishModel>>.Failure($"Error al obtener platos: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<DishModel>> GetDishById(int id)
    {
        try
        {
            var dish = await _dishRepository.GetByIdAsync(id);
            if (dish == null)
                return Result<DishModel>.Failure("Plato no encontrado", HttpStatusCode.NotFound);

            return Result<DishModel>.Success(dish, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return Result<DishModel>.Failure($"Error al obtener plato: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<object>> DeleteDish(int id)
    {
        try
        {
            if (!await _dishRepository.IsExistId(id))
                return Result<object>.Failure("El plato no existe", HttpStatusCode.NotFound);

            var deleted = await _dishRepository.DeleteAsync(id);
            if (!deleted)
                return Result<object>.Failure("No se pudo eliminar el plato", HttpStatusCode.InternalServerError);

            return Result<object>.Success(new { message = "Plato eliminado exitosamente" }, HttpStatusCode.OK);
        }
        catch (InvalidOperationException ex)
        {
            return Result<object>.Failure(ex.Message, HttpStatusCode.Conflict);
        }
        catch (Exception ex)
        {
            return Result<object>.Failure($"Error interno: {ex.Message}", HttpStatusCode.InternalServerError);
        }
    }
}