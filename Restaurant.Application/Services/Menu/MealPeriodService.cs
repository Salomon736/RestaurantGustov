using System.Net;
using Restaurant.Domain.Models.Menu;
using Restaurant.Domain.Repositories.Menu;
using Restaurant.Domain.Responses;

namespace Restaurant.Application.Services.Menu;

public class MealPeriodService
{
    private readonly IMealPeriodRepository _mealPeriodRepository;

    public MealPeriodService(IMealPeriodRepository mealPeriodRepository)
    {
        _mealPeriodRepository = mealPeriodRepository;
    }

    public async Task<Result<object>> SaveMealPeriod(MealPeriodModel model)
    {
        if (model.HasErrors())
            return Result<object>.Failure(model.GetAllMessageErrors(), HttpStatusCode.BadRequest);

        if (await _mealPeriodRepository.IsMealTypeDuplicate(model.NameMealPeriod, model.Id))
            return Result<object>.Failure("Ya existe un período de comida con ese tipo", HttpStatusCode.BadRequest);
        if (TimeSpan.TryParse(model.StartTime, out var startTimeSpan) && 
            TimeSpan.TryParse(model.EndTime, out var endTimeSpan))

        if (await _mealPeriodRepository.HasTimeOverlap(startTimeSpan, endTimeSpan, model.Id))
            return Result<object>.Failure("Los horarios se superponen con otro período existente", HttpStatusCode.BadRequest);

        try
        {
            await _mealPeriodRepository.InsertAsync(model);
            return Result<object>.Success(new { }, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return Result<object>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<List<MealPeriodModel>>> GetAllMealPeriods()
    {
        try
        {
            var mealPeriods = await _mealPeriodRepository.GetAllAsync();
            return Result<List<MealPeriodModel>>.Success(mealPeriods, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return Result<List<MealPeriodModel>>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<MealPeriodModel>> GetMealPeriodById(int id)
    {
        try
        {
            var mealPeriod = await _mealPeriodRepository.GetByIdAsync(id);
            if (mealPeriod == null)
                return Result<MealPeriodModel>.Failure("Período de comida no encontrado", HttpStatusCode.NotFound);

            return Result<MealPeriodModel>.Success(mealPeriod, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return Result<MealPeriodModel>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }

    public async Task<Result<object>> DeleteMealPeriod(int id)
    {
        try
        {
            if (!await _mealPeriodRepository.IsExistId(id))
                return Result<object>.Failure("Período de comida no encontrado", HttpStatusCode.NotFound);

            await _mealPeriodRepository.DeleteAsync(id);
            return Result<object>.Success(new { }, HttpStatusCode.OK);
        }
        catch (Exception ex)
        {
            return Result<object>.Failure(ex.Message, HttpStatusCode.InternalServerError);
        }
    }
}