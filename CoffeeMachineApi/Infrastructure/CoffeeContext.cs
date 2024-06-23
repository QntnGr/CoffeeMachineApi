using CoffeeMachineApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachineApi.Infrastructure;

public class CoffeeContext : DbContext
{
    public DbSet<Ingredient> Ingredients { get; set;}
    public DbSet<Drink> Drinks { get; set;}
    public string DbPath { get; set;}

    public CoffeeContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "coffee-machine.db");
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlite($"Data Source={DbPath}");
}
