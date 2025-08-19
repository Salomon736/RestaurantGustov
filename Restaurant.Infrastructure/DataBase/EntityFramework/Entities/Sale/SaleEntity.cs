using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Restaurant.Infrastructure.DataBase.EntityFramework.Entities.Menu;

namespace Restaurant.Infrastructure.DataBase.EntityFramework.Entities.Sale;

[Table("sale")]
public class SaleEntity : BaseEntity, IIdentifiable
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Required]
    [Column("idMenu")]
    public int IdMenu { get; set; }
    
    [Required]
    [Range(1, 1000)]
    [Column("quantitySold")]
    public int QuantitySold { get; set; }
    
    [Required]
    [Range(0.01, 999999.99)]
    [Column("totalPrice", TypeName = "decimal(10,2)")]
    public decimal TotalPrice { get; set; }
    
    [ForeignKey("IdMenu")]
    public virtual MenuEntity Menu { get; set; }
}
