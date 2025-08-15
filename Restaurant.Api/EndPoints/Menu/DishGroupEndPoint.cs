using Restaurant.Api.EndPoints.Common;
using Restaurant.Application.Services.Menu;
using Restaurant.Domain.Models.Menu;

namespace Restaurant.Api.EndPoints.Menu;

public static class DishGroupEndPoint
{
    internal static void MapDishEndpoints(this WebApplication webApp)
    {
        webApp.MapGroup("/dish").WithTags("Platos").MapGroupDish();
    }

    private static void MapGroupDish(this RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapPost(
            "/",
            (DishService service, DishModel model) =>
                service.SaveDish(model).ToApiResult()
        );

        groupBuilder.MapGet(
            "/",
            (DishService service) =>
                service.GetAllDishes().ToApiResult()
        );

        groupBuilder.MapGet(
            "/{id:int}",
            (DishService service, int id) =>
                service.GetDishById(id).ToApiResult()
        );

        groupBuilder.MapDelete(
            "/{id:int}",
            (DishService service, int id) =>
                service.DeleteDish(id).ToApiResult()
        );
    }
}