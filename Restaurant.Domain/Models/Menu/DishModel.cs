using System.Text.Json.Serialization;

namespace Restaurant.Domain.Models.Menu;

public class DishModel : TraceModel
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Image { get; private set; }
    public decimal Price { get; private set; }

    [JsonConstructor]
    public DishModel(int id, string name, string description, string image, decimal price)
    {
        if (id < 0)
            AddError(new Exception("El ID no puede ser negativo"));
        if (string.IsNullOrWhiteSpace(name))
            AddError(new Exception("El nombre del plato es requerido"));
        else if (name.Length > 50)
            AddError(new Exception("El nombre del plato no puede exceder 50 caracteres"));
        else if (!IsValidText(name))
            AddError(new Exception("El nombre solo puede contener letras, números y espacios"));

        if (string.IsNullOrWhiteSpace(description))
            AddError(new Exception("La descripción del plato es requerida"));
        else if (description.Length > 200)
            AddError(new Exception("La descripción no puede exceder 200 caracteres"));
        
        if (string.IsNullOrWhiteSpace(image))
            AddError(new Exception("La imagen del plato es requerida"));
        else if (image.Length > 255)
            AddError(new Exception("La URL de la imagen no puede exceder 255 caracteres"));
        
        if (price <= 0)
            AddError(new Exception("El precio debe ser mayor a 0"));
        else if (price > 9999.99m)
            AddError(new Exception("El precio no puede exceder 9999.99"));
        
        Id = id;
        Name = name?.Trim();
        Description = description?.Trim();
        Image = image?.Trim();
        Price = Math.Round(price, 2);
    }

    private static bool IsValidText(string text)
    {
        return text.All(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || 
                           c == 'ñ' || c == 'Ñ' || "áéíóúÁÉÍÓÚ".Contains(c));
    }
}