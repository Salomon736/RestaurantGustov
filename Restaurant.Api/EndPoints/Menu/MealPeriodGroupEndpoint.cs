using Restaurant.Api.EndPoints.Common;
using Restaurant.Application.Services.Menu;
using Restaurant.Domain.Models.Menu;

namespace Restaurant.Api.EndPoints.Menu;

public static class MealPeriodGroupEndpoint
{
    public static void MapMealPeriodEndpoints(this WebApplication webApp)
    {
        webApp.MapGroup("/mealperiod").WithTags("MealPeriod").MapGroupMealPeriod();
    }

    private static void MapGroupMealPeriod(this RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapPost(
            "/",
            (MealPeriodService service, MealPeriodModel model) =>
                service.SaveMealPeriod(model).ToApiResult()
        );

        groupBuilder.MapPut(
            "/{id:int}",
            (int id, MealPeriodService service, MealPeriodModel model) =>
            {
                model.Id = id;
                return service.SaveMealPeriod(model).ToApiResult();
            }
        );

        groupBuilder.MapGet(
            "/",
            (MealPeriodService service) =>
                service.GetAllMealPeriods().ToApiResult()
        );

        groupBuilder.MapGet(
            "/{id:int}",
            (MealPeriodService service, int id) =>
                service.GetMealPeriodById(id).ToApiResult()
        );

        groupBuilder.MapDelete(
            "/{id:int}",
            (MealPeriodService service, int id) =>
                service.DeleteMealPeriod(id).ToApiResult()
        );
    }
}