using System.Text.Json.Serialization;

namespace Restaurant.Domain.Models.Menu;

public class MenuModel : TraceModel
{
    public int Id { get;  set; }
    public DateTime MenuDate { get; private set; }
    public int Quantity { get;  set; }
    public int IdDish { get; private set; }
    public int IdMealPeriod { get; private set; }
    public DishModel Dish { get; set; }
    public MealPeriodModel MealPeriod { get; set; }

    [JsonConstructor]
    public MenuModel(int id, DateTime menuDate, int quantity, int idDish, int idMealPeriod)
    {
        if (menuDate.Date < DateTime.Today)
            AddError(new Exception("La fecha del menú no puede ser anterior a hoy"));
        
        if (quantity <= 0)
            AddError(new Exception("La cantidad debe ser mayor a 0"));
        if (quantity > 1000)
            AddError(new Exception("La cantidad no puede ser mayor a 1000"));

        if (idDish <= 0)
            AddError(new Exception("Debe seleccionar un plato válido"));

        if (idMealPeriod <= 0)
            AddError(new Exception("Debe seleccionar un período de comida válido"));

        Id = id;
        MenuDate = menuDate.Date;
        Quantity = quantity;
        IdDish = idDish;
        IdMealPeriod = idMealPeriod;
    }
}