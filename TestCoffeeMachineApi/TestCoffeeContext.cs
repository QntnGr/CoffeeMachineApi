using CoffeeMachineApi.Entities;
using CoffeeMachineApi.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace TestCoffeeMachineApi
{
    public class TestCoffeeContext : CoffeeContext
    {
        public TestCoffeeContext(DbContextOptions<TestCoffeeContext> options) : base() { }

        public override DbSet<Drink> Drinks { get; set; }
        public override DbSet<Ingredient> Ingredients { get; set; }
    }
}
