using Restaurant.Api.EndPoints.Common;
using Restaurant.Application.Services.Menu;
using Restaurant.Domain.Models.Menu;

namespace Restaurant.Api.EndPoints.Menu;

public static class MenuGroupEndpoint
{
    public static void MapMenuEndpoints(this WebApplication webApp)
    {
        webApp.MapGroup("/menu").WithTags("Menu").MapGroupMenu();
    }

    private static void MapGroupMenu(this RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapPost(
            "/",
            (MenuService service, MenuModel model) =>
                service.SaveMenu(model).ToApiResult()
        );

        groupBuilder.MapPut(
            "/{id:int}",
            (int id, MenuService service, MenuModel model) =>
            {
                model.Id = id;
                return service.SaveMenu(model).ToApiResult();
            }
        );

        groupBuilder.MapGet(
            "/",
            (MenuService service) =>
                service.GetAllMenus().ToApiResult()
        );

        groupBuilder.MapGet(
            "/{id:int}",
            (MenuService service, int id) =>
                service.GetMenuById(id).ToApiResult()
        );

        groupBuilder.MapDelete(
            "/{id:int}",
            (MenuService service, int id) =>
                service.DeleteMenu(id).ToApiResult()
        );

        groupBuilder.MapGet(
            "/date/{date:datetime}",
            (MenuService service, DateTime date) =>
                service.GetMenuByDate(date).ToApiResult()
        );
    }
}