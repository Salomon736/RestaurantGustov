using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Restaurant.Infrastructure.DataBase.EntityFramework.Entities.Menu;
[Table("mealPeriod")]
public class MealPeriodEntity : BaseEntity, IIdentifiable
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    [Column("nameMealPeriod")]
    public string NameMealPeriod { get; set; }

    [Required]
    [Column("startTime")]
    public TimeSpan StartTime { get; set; }

    [Required]
    [Column("endTime")]
    public TimeSpan EndTime { get; set; }

    [Required]
    [StringLength(20)]
    [Column("color")]
    public string Color { get; set; }
}