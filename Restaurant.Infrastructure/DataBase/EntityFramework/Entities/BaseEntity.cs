namespace Restaurant.Infrastructure.DataBase.EntityFramework.Entities;

public abstract class BaseEntity
{
    public DateTime createdAt { get; set; }
    public int createdBy { get; set; }
    public DateTime lastModifiedAt { get; set; }
    public int lastModifiedBy { get; set; }
    
}