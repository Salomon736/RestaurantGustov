using System.Text.Json.Serialization;

namespace Restaurant.Domain.Models.Menu;

public class MenuModel : TraceModel
{
    public int Id { get; private set; }
    public DateTime MenuDate { get; private set; }
    public int IdDish { get; private set; }
    public int IdMealPeriod { get; private set; }

    [JsonConstructor]
    public MenuModel(int id, DateTime menuDate, int idDish, int idMealPeriod)
    {
        if (menuDate.Date < DateTime.Today)
            AddError(new Exception("La fecha del menú no puede ser anterior a hoy"));

        if (idDish <= 0)
            AddError(new Exception("Debe seleccionar un plato válido"));

        if (idMealPeriod <= 0)
            AddError(new Exception("Debe seleccionar un período de comida válido"));

        Id = id;
        MenuDate = menuDate.Date;
        IdDish = idDish;
        IdMealPeriod = idMealPeriod;
    }
}