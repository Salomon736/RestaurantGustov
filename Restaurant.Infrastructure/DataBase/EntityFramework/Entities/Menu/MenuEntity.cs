using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Infrastructure.DataBase.EntityFramework.Entities.Menu;
[Table("menu")]
public class MenuEntity : BaseEntity, IIdentifiable
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Required]
    [Column("menuDate")]
    public DateTime MenuDate { get; set; }
    
    [Required]
    [Range(0, 1000)] 
    [Column("quantity")]
    public int Quantity { get; set; }

    [Required]
    [Column("idDish")]
    public int IdDish { get; set; }

    [Required]
    [Column("idMealPeriod")]
    public int IdMealPeriod { get; set; }
    
    [ForeignKey("IdDish")]
    public virtual DishEntity Dish { get; set; }

    [ForeignKey("IdMealPeriod")]
    public virtual MealPeriodEntity MealPeriod { get; set; }
}