using Restaurant.Domain.Models.Menu;
using Restaurant.Infrastructure.DataBase.EntityFramework.Entities.Menu;

namespace Restaurant.Infrastructure.DataBase.EntityFramework.Extensions.Menu;

public static class DishExtension
{
    public static DishEntity ToEntity(this DishModel model)
    {
        return new DishEntity
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            Image = model.Image,
            Price = model.Price
        };
    }

    public static DishModel ToModel(this DishEntity entity)
    {
        return new DishModel(
            entity.Id,
            entity.Name,
            entity.Description,
            entity.Image,
            entity.Price
        );
    }

    public static List<DishModel> ToModelList(this List<DishEntity> entities)
    {
        return entities.Select(entity => entity.ToModel()).ToList();
    }
}