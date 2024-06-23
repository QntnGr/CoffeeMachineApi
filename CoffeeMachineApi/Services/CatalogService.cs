using CoffeeMachineApi.Entities;
using CoffeeMachineApi.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CoffeeMachineApi.Services
{
    public class CatalogService : ICatalogService
    {
        readonly CoffeeContext _coffeeContext;
        private static decimal ProfitMargin = 1.3m; // 30percent of profit margin 

        public CatalogService(CoffeeContext coffeeContext)
        {
            _coffeeContext = coffeeContext;
        }

        public async Task<IEnumerable<Drink>> GetAll()
        {
            return await _coffeeContext.Drinks.Include(dr => dr.DrinkIngredients).ToListAsync();
        }

        public async Task<decimal> GetPriceByDrinkId(int id)
        {
            var drink = await _coffeeContext.Drinks.Include(dr => dr.DrinkIngredients)
                .FirstOrDefaultAsync(d => d.Id == id);

            return drink?.DrinkIngredients.Sum(di => _coffeeContext.Ingredients.FirstOrDefault(i=>i.Id == di.Id)?.UnitPrice *  ProfitMargin) ?? decimal.MinusOne;
        }
    }
}
