using CoffeeMachineApi.Infrastructure;
using CoffeeMachineApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Coffe Machine",
        Version = "v1"
    });
});


builder.Services.AddDbContext<CoffeeContext>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICatalogService, CatalogService>();

var app = builder.Build();

using (var context = new CoffeeContext())
{
    using (var init = new DbInitializer(context))
    {
        init.InitData();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
