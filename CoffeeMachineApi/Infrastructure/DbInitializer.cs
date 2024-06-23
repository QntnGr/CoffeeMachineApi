using CoffeeMachineApi.Entities;

namespace CoffeeMachineApi.Infrastructure
{
    public class DbInitializer : IDisposable
    {
        private readonly CoffeeContext _context;
        public DbInitializer(CoffeeContext coffeeContext)
        {
            _context = coffeeContext;
        }

        public void InitData()
        {

            var ingredients = new List<Ingredient>
            {
                new Ingredient { Name = Constants.MilchPulver, UnitPrice = 0.1m },
                new Ingredient { Name = Constants.Coffee, UnitPrice = 0.3m },
                new Ingredient { Name = Constants.Chocolate, UnitPrice = 0.4m },
                new Ingredient { Name = Constants.Tea, UnitPrice = 0.3m },
                new Ingredient { Name = Constants.Water, UnitPrice = 0.05m },
            };

            ingredients.ForEach(i => _context.Ingredients.Add(i));
            _context.SaveChanges();

            var drinks = new List<Drink>();
            
            var expresso = new Drink { Name = Constants.Espresso, Ingredients = new List<Ingredient>(GetIngredientByNameAndCount(Constants.Water, 2)) };
            expresso.Ingredients.AddRange(GetIngredientByNameAndCount(Constants.Coffee, 1));
            drinks.Add(expresso);
            
            var milk = new Drink { Name = Constants.Milk, Ingredients = new List<Ingredient>(GetIngredientByNameAndCount(Constants.Milk, 2)) };
            milk.Ingredients.AddRange(GetIngredientByNameAndCount(Constants.Water, 1));
            drinks.Add(milk);
            
            var capuccino = new Drink { Name = Constants.Capuccino, Ingredients = new List<Ingredient>(GetIngredientByNameAndCount(Constants.Milk, 2)) };
            capuccino.Ingredients.AddRange(GetIngredientByNameAndCount(Constants.Water, 1));
            capuccino.Ingredients.AddRange(GetIngredientByNameAndCount(Constants.Coffee, 1));
            capuccino.Ingredients.AddRange(GetIngredientByNameAndCount(Constants.Chocolate, 1));
            drinks.Add(capuccino);
            
            var hotChocolate = new Drink { Name = Constants.HotChocolate, Ingredients = new List<Ingredient>(GetIngredientByNameAndCount(Constants.Water, 3)) };
            hotChocolate.Ingredients.AddRange(GetIngredientByNameAndCount(Constants.Chocolate, 2));
            drinks.Add(hotChocolate);

            var milkAndCoffee = new Drink { Name = Constants.MilkAndCoffee, Ingredients = new List<Ingredient>(GetIngredientByNameAndCount(Constants.Water, 2)) };
            milkAndCoffee.Ingredients.AddRange(GetIngredientByNameAndCount(Constants.MilchPulver, 1));
            milkAndCoffee.Ingredients.AddRange(GetIngredientByNameAndCount(Constants.Coffee, 1));
            drinks.Add(milkAndCoffee);

            var mokaccino = new Drink { Name = Constants.Mokaccino, Ingredients = new List<Ingredient>(GetIngredientByNameAndCount(Constants.Water, 2)) };
            mokaccino.Ingredients.AddRange(GetIngredientByNameAndCount(Constants.MilchPulver, 1));
            mokaccino.Ingredients.AddRange(GetIngredientByNameAndCount(Constants.Coffee, 1));
            mokaccino.Ingredients.AddRange(GetIngredientByNameAndCount(Constants.Chocolate, 2));
            drinks.Add(mokaccino);

            var tea = new Drink { Name = Constants.Mokaccino, Ingredients = new List<Ingredient>(GetIngredientByNameAndCount(Constants.Water, 2)) };
            tea.Ingredients.AddRange(GetIngredientByNameAndCount(Constants.Tea, 1));
            drinks.Add(tea);
        }

        public IEnumerable<Ingredient> GetIngredientByNameAndCount(string name, int doseCount)
        {
            for(int i = 0; i < doseCount; i++)
            {
                var ingredient = _context.Ingredients.FirstOrDefault(i => i.Name == name);
                if (ingredient != null)
                {
                    yield return ingredient;
                }
            }
        }

        public void Dispose()
        {
            ((IDisposable)_context).Dispose();
        }
    }
}
