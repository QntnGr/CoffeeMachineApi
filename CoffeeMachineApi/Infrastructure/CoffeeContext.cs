using CoffeeMachineApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachineApi.Infrastructure;

public class CoffeeContext : DbContext
{
    public virtual DbSet<Ingredient> Ingredients { get; set;}
    public virtual DbSet<Drink> Drinks { get; set;}
    public DbSet<User> Users { get; set; }
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
