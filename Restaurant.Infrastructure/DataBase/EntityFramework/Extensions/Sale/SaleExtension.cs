using Restaurant.Domain.Models.Sale;
using Restaurant.Infrastructure.DataBase.EntityFramework.Entities.Sale;
using Restaurant.Infrastructure.DataBase.EntityFramework.Extensions.Menu;

namespace Restaurant.Infrastructure.DataBase.EntityFramework.Extensions.Sale;

public static class SaleExtension
{
    public static SaleEntity ToEntity(this SaleModel model)
    {
        return new SaleEntity
        {
            Id = model.Id,
            SaleDate = model.SaleDate,
            IdMenu = model.IdMenu,
            QuantitySold = model.QuantitySold,
            TotalPrice = model.TotalPrice
        };
    }

    public static SaleModel ToModel(this SaleEntity entity)
    {
        var saleModel = new SaleModel(
            entity.Id, 
            entity.SaleDate, 
            entity.IdMenu, 
            entity.QuantitySold, 
            entity.TotalPrice
        );
        
        if (entity.Menu != null)
        {
            saleModel.Menu = entity.Menu.ToModel();
        }
        
        return saleModel;
    }

    public static List<SaleModel> ToModelList(this List<SaleEntity> entities)
    {
        return entities.Select(entity => entity.ToModel()).ToList();
    }
}