using System.Text.Json.Serialization;

namespace Restaurant.Domain.Models.Menu;

public class MealPeriodModel : TraceModel
{
    public int Id { get;  set; }
    public string NameMealPeriod { get; private set; }
    public string  StartTime { get; private set; }
    public string  EndTime { get; private set; }
    public string Color { get; private set; }
    [JsonConstructor]
    public MealPeriodModel(
        int id,
        string nameMealPeriod,
        string startTime,
        string endTime,
        string color
        
    )
    {
        if (id < 0)
            AddError(new Exception("El ID no puede ser negativo"));
        if (string.IsNullOrWhiteSpace(nameMealPeriod))
            AddError(new Exception("El nombre del periodo de comida es requerido"));
        if (!IsValidTimeFormat(startTime))
            AddError(new Exception("Formato de hora de inicio inválido. Use HH:mm (ej: 08:30)"));

        if (!IsValidTimeFormat(endTime))
            AddError(new Exception("Formato de hora de fin inválido. Use HH:mm (ej: 14:30)"));
        else if (nameMealPeriod.Length > 20)
            AddError(new Exception("El nombre del periodo de comida no puede exceder 20 caracteres"));
        else if (!IsValidText(nameMealPeriod))
            AddError(new Exception("El nombre solo puede contener letras, números y espacios"));
        if (IsValidTimeFormat(startTime) && IsValidTimeFormat(endTime))
        {
            var startTimeSpan = TimeSpan.Parse(startTime);
            var endTimeSpan = TimeSpan.Parse(endTime);
            if (startTimeSpan >= endTimeSpan)
                AddError(new Exception("Hora de inicio debe ser menor a hora de fin"));
        }

        if (string.IsNullOrWhiteSpace(color) || color.Length > 20)
            AddError(new Exception("Color es requerido, máximo 20 caracteres"));
        
        Id = id;
        NameMealPeriod = nameMealPeriod?.Trim();
        StartTime = startTime?.Trim();
        EndTime = endTime?.Trim();
        Color = color?.Trim();
    }
    private static bool IsValidText(string text)
    {
        return text.All(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || 
                             c == 'ñ' || c == 'Ñ' || "áéíóúÁÉÍÓÚ".Contains(c));
    }
    private bool IsValidTimeFormat(string time)
    {
        return TimeSpan.TryParseExact(time, @"hh\:mm", null, out _) || 
               TimeSpan.TryParseExact(time, @"h\:mm", null, out _);
    }
}