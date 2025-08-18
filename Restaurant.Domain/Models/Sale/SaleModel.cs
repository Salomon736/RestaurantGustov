using System.Text.Json.Serialization;
using Restaurant.Domain.Models.Menu;

namespace Restaurant.Domain.Models.Sale;

public class SaleModel : TraceModel
{
    public int Id { get; set; }
    public DateTime SaleDate { get; private set; }
    public int IdMenu { get; private set; }
    public int QuantitySold { get; private set; }
    public decimal TotalPrice { get; private set; }
    public MenuModel Menu { get; set; }

    [JsonConstructor]
    public SaleModel(int id, DateTime saleDate, int idMenu, int quantitySold, decimal totalPrice)
    {
        if (saleDate.Date > DateTime.Today)
            AddError(new Exception("La fecha de venta no puede ser futura"));
        
        if (quantitySold <= 0)
            AddError(new Exception("La cantidad vendida debe ser mayor a 0"));
        
        if (quantitySold > 1000)
            AddError(new Exception("La cantidad vendida no puede ser mayor a 1000"));

        if (idMenu <= 0)
            AddError(new Exception("Debe seleccionar un menú válido"));

        if (totalPrice <= 0)
            AddError(new Exception("El precio total debe ser mayor a 0"));

        if (totalPrice > 999999.99m)
            AddError(new Exception("El precio total no puede ser mayor a 999,999.99"));

        Id = id;
        SaleDate = saleDate.Date;
        IdMenu = idMenu;
        QuantitySold = quantitySold;
        TotalPrice = totalPrice;
    }
}