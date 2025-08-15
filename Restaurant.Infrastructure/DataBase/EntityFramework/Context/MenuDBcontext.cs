using Microsoft.EntityFrameworkCore;
using Restaurant.Infrastructure.DataBase.EntityFramework.Context.Menu;
using Restaurant.Infrastructure.DataBase.EntityFramework.Entities;
using Restaurant.Infrastructure.DataBase.EntityFramework.Entities.Menu;

namespace Restaurant.Infrastructure.DataBase.EntityFramework.Context;

public class MenuDBcontext:DbContext
{
    public DbSet<DishEntity> Dish { get; set; } 
    public MenuDBcontext(DbContextOptions<MenuDBcontext> options) : base(options) {}
   protected override void OnModelCreating(ModelBuilder entity)
    {
        base.OnModelCreating(entity);
        entity.ApplyConfiguration(new DishConfiguration());
    }
   
    public override int SaveChanges()
    {
        UpdateAuditFields();
        return base.SaveChanges();
    }
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditFields();
        return base.SaveChangesAsync(cancellationToken);
    }
    private void UpdateAuditFields()
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.createdAt = DateTime.UtcNow;
                    entry.Entity.createdBy = GetCurrentUserId();
                    entry.Entity.lastModifiedAt = DateTime.UtcNow;
                    entry.Entity.lastModifiedBy= GetCurrentUserId();
                    break;

                case EntityState.Modified:
                    entry.Property(nameof(BaseEntity.createdAt)).IsModified = false;
                    entry.Property(nameof(BaseEntity.createdBy)).IsModified = false;
                    entry.Entity.lastModifiedAt = DateTime.UtcNow;
                    entry.Entity.lastModifiedBy = 104;
                    break;
            }
        }
    }

    private int GetCurrentUserId()
    {
        return 106;
    } 
}