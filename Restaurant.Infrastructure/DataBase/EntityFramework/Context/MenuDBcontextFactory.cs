using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Restaurant.Infrastructure.DataBase.EntityFramework.Context;

public class MenuDBcontextFactory : IDesignTimeDbContextFactory<MenuDBcontext>
{
    public MenuDBcontext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) 
            .AddUserSecrets<MenuDBcontextFactory>()  
            .Build();
        
        string connectionString = configuration["ConnectionStrings:remoteConnection"];

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentNullException(nameof(connectionString), "La cadena de conexión no puede ser nula ni estar vacía.");
        }
        
        var optionsBuilder = new DbContextOptionsBuilder<MenuDBcontext>();
        optionsBuilder.UseSqlServer(connectionString);
        return new MenuDBcontext(optionsBuilder.Options);
    }
    
}