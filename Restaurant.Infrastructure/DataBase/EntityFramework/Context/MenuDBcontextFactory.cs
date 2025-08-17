using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Restaurant.Infrastructure.DataBase.EntityFramework.Context;

public class MenuDBcontextFactory : IDesignTimeDbContextFactory<MenuDBcontext>
{
    public MenuDBcontext CreateDbContext(string[] args)
    {
        string connectionString = "Server=DESKTOP-8HBKT6U\\SQLEXPRESS;Database=restaurantGustov;User=sa;Password=12345678;TrustServerCertificate=true;";

        var optionsBuilder = new DbContextOptionsBuilder<MenuDBcontext>();
        optionsBuilder.UseSqlServer(connectionString);

        return new MenuDBcontext(optionsBuilder.Options);
    }
    
}