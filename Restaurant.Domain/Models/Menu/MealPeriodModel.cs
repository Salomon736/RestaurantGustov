using System.Text.Json.Serialization;

namespace Restaurant.Domain.Models.Menu;

public class MealPeriodModel : TraceModel
{
    public int Id { get;  set; }
    public string Name { get; private set; }
    public TimeOnly  StartTime { get; private set; }
    public TimeOnly  EndTime { get; private set; }
    public string Color { get; private set; }
    [JsonConstructor]
    public MealPeriodModel(
        int id,
        string name,
        TimeOnly startTime,
        TimeOnly endTime,
        string color
        
    )
    {
        if (id < 0)
            AddError(new Exception("El ID no puede ser negativo"));
        if (string.IsNullOrWhiteSpace(name))
            AddError(new Exception("El nombre del periodo de comida es requerido"));
        if (startTime >= endTime)
            AddError(new Exception("Hora de inicio debe ser menor a hora de fin"));
        else if (name.Length > 20)
            AddError(new Exception("El nombre del periodo de comida no puede exceder 20 caracteres"));
        else if (!IsValidText(name))
            AddError(new Exception("El nombre solo puede contener letras, números y espacios"));
        
        
        Id = id;
        Name = name?.Trim();
        StartTime = startTime;
        EndTime = endTime;
        Color = color?.Trim();
    }
    private static bool IsValidText(string text)
    {
        return text.All(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || 
                             c == 'ñ' || c == 'Ñ' || "áéíóúÁÉÍÓÚ".Contains(c));
    }
}