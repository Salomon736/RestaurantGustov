using Restaurant.Api.EndPoints.Common;
using Restaurant.Application.Services.Sale;
using Restaurant.Domain.Models.Sale;

namespace Restaurant.Api.EndPoints.Sale;

public static class SaleGroupEndpoint
{
    public static void MapSaleEndpoints(this WebApplication webApp)
    {
        webApp.MapGroup("/sale").WithTags("Sale").MapGroupSale();
    }

    private static void MapGroupSale(this RouteGroupBuilder groupBuilder)
    {
        groupBuilder.MapPost(
            "/",
            (SaleService service, SaleModel model) =>
                service.SaveSale(model).ToApiResult()
        );

        groupBuilder.MapPut(
            "/{id:int}",
            (int id, SaleService service, SaleModel model) =>
            {
                model.Id = id;
                return service.SaveSale(model).ToApiResult();
            }
        );

        groupBuilder.MapGet(
            "/",
            (SaleService service) =>
                service.GetAllSales().ToApiResult()
        );

        groupBuilder.MapGet(
            "/{id:int}",
            (SaleService service, int id) =>
                service.GetSaleById(id).ToApiResult()
        );

        groupBuilder.MapDelete(
            "/{id:int}",
            (SaleService service, int id) =>
                service.DeleteSale(id).ToApiResult()
        );

        groupBuilder.MapGet(
            "/date/{date:datetime}",
            (SaleService service, DateTime date) =>
                service.GetSalesByDate(date).ToApiResult()
        );

        groupBuilder.MapGet(
            "/date-range/{startDate:datetime}/{endDate:datetime}",
            (SaleService service, DateTime startDate, DateTime endDate) =>
                service.GetSalesByDateRange(startDate, endDate).ToApiResult()
        );

        groupBuilder.MapGet(
            "/menu/{idMenu:int}",
            (SaleService service, int idMenu) =>
                service.GetSalesByMenu(idMenu).ToApiResult()
        );

        groupBuilder.MapGet(
            "/total/date/{date:datetime}",
            (SaleService service, DateTime date) =>
                service.GetTotalSalesByDate(date).ToApiResult()
        );

        groupBuilder.MapGet(
            "/total/date-range/{startDate:datetime}/{endDate:datetime}",
            (SaleService service, DateTime startDate, DateTime endDate) =>
                service.GetTotalSalesByDateRange(startDate, endDate).ToApiResult()
        );
    }
}