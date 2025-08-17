using Restaurant.Api.EndPoints.Menu;
using Restaurant.Infrastructure.Ioc.DependencyInjection;
using Restaurant.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .RegisterDataBase(builder.Configuration)
    .RegisterRepositories()
    .RegisterServices()
    .RegisterProviders(builder.Configuration)
    .RegisterLibraries();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy",
        policy => policy
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .SetIsOriginAllowed((hosts)=> true));
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "APIS RESTAURANT V1.0");
    c.RoutePrefix = "swagger";
    c.EnableFilter();
});
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.MapDishEndpoints();
app.MapMealPeriodEndpoints();
app.MapMenuEndpoints();
app.UseCors("CORSPolicy");
app.UseMiddleware<MiddlewareException>();

app.Run();