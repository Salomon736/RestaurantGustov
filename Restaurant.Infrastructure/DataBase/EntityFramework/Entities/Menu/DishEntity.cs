using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Infrastructure.DataBase.EntityFramework.Entities.Menu;

[Table("dish")]
public class DishEntity : BaseEntity, IIdentifiable
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    [Column("name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    [Column("description")]
    public string Description { get; set; } = string.Empty;

    [Required]
    [StringLength(255)]
    [Column("image")]
    public string Image { get; set; } = string.Empty;

    [Required]
    [Column("price", TypeName = "decimal(10,2)")]
    public decimal Price { get; set; }
}