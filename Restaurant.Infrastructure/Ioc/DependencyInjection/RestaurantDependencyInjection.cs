using System.Net.Http.Headers;
using System.Reflection;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Application.Services.Menu;
using Restaurant.Domain.Repositories.Menu;
using Restaurant.Infrastructure.DataBase.EntityFramework.Context;
using Restaurant.Infrastructure.DataBase.EntityFramework.Repositories.Menu;

namespace Restaurant.Infrastructure.Ioc.DependencyInjection;

public static class RestaurantDependencyInjection
{
    public static IServiceCollection RegisterDataBase(this IServiceCollection collection, IConfiguration configuration)
    {
        string connectionString = configuration["ConnectionStrings:remoteConnection"];

        collection.AddDbContext<MenuDBcontext>(options =>
            {
                options.UseSqlServer(connectionString);
            }
        );
        return collection;
    }
    public static IServiceCollection RegisterLibraries(this IServiceCollection collection)
    {
        collection.AddValidatorsFromAssembly(Assembly.Load("Restaurant.Application"));

        return collection;
    }

    public static IServiceCollection RegisterProviders(this IServiceCollection collection, IConfiguration configuration)
    {
        return collection;
    }

    public static IServiceCollection RegisterServices(this IServiceCollection collection)
    {
        collection.AddTransient<DishService>();
        return collection;
    }

    public static IServiceCollection RegisterRepositories(this IServiceCollection collection)
    {
        collection.AddTransient<IDishRepository, DishRepository>();
        return collection;
    }
}