using CoffeeMachineApi.Entities;

namespace CoffeeMachineApi.Infrastructure;

public class DbInitializer : IDisposable
{
    private readonly CoffeeContext _context;

    public DbInitializer(CoffeeContext coffeeContext)
    {
        _context = coffeeContext;
    }

    public void InitData()
    {
        if (_context.Drinks.Any() || _context.Ingredients.Any())
        {
            return;
        }

        var ingredients = new List<Ingredient>
        {
            new Ingredient { Id = 1, Name = Constants.MilchPulver, UnitPrice = 0.1m },
            new Ingredient { Id = 2, Name = Constants.Coffee, UnitPrice = 0.3m },
            new Ingredient { Id = 3, Name = Constants.Chocolate, UnitPrice = 0.4m },
            new Ingredient { Id = 4, Name = Constants.Tea, UnitPrice = 0.3m },
            new Ingredient { Id = 5, Name = Constants.Water, UnitPrice = 0.05m },
        };
        _context.Ingredients.AddRange(ingredients);
        _context.SaveChanges();

        var drinks = new List<Drink>();

        var expresso = new Drink
        {
                Name = Constants.Espresso,
                DrinkIngredients = new List<DrinkIngredient> {
                    GetDrinkIngredientByNameAndCount(Constants.Water, 2),
                    GetDrinkIngredientByNameAndCount(Constants.Coffee, 1)
            }
        };
        drinks.Add(expresso);

        var milk = new Drink { Name = Constants.Milk,
            DrinkIngredients = new List<DrinkIngredient> {
                    GetDrinkIngredientByNameAndCount(Constants.MilchPulver, 2),
                    GetDrinkIngredientByNameAndCount(Constants.Water, 1)
            }
        };
        drinks.Add(milk);

        var capuccino = new Drink { Name = Constants.Capuccino,
            DrinkIngredients = new List<DrinkIngredient> {
                    GetDrinkIngredientByNameAndCount(Constants.MilchPulver, 2),
                    GetDrinkIngredientByNameAndCount(Constants.Water, 1),
                    GetDrinkIngredientByNameAndCount(Constants.Coffee, 1),
                    GetDrinkIngredientByNameAndCount(Constants.Chocolate, 1)
            }
        };
        drinks.Add(capuccino);

        var hotChocolate = new Drink { Name = Constants.HotChocolate,
            DrinkIngredients = new List<DrinkIngredient> {
                    GetDrinkIngredientByNameAndCount(Constants.Water, 3),
                    GetDrinkIngredientByNameAndCount(Constants.Chocolate, 2)
            }
        };
        drinks.Add(hotChocolate);

        var milkAndCoffee = new Drink { Name = Constants.MilkAndCoffee,
            DrinkIngredients = new List<DrinkIngredient> {
                    GetDrinkIngredientByNameAndCount(Constants.MilchPulver, 1),
                    GetDrinkIngredientByNameAndCount(Constants.Water, 2),
                    GetDrinkIngredientByNameAndCount(Constants.Coffee, 1),
            }
        };
        drinks.Add(milkAndCoffee);

        var mokaccino = new Drink { Name = Constants.Mokaccino,
            DrinkIngredients = new List<DrinkIngredient> {
                    GetDrinkIngredientByNameAndCount(Constants.MilchPulver, 1),
                    GetDrinkIngredientByNameAndCount(Constants.Water, 2),
                    GetDrinkIngredientByNameAndCount(Constants.Coffee, 1),
                    GetDrinkIngredientByNameAndCount(Constants.Chocolate, 2),
            }
        };
        drinks.Add(mokaccino);

        var tea = new Drink { Name = Constants.Tea,
            DrinkIngredients = new List<DrinkIngredient> {
                    GetDrinkIngredientByNameAndCount(Constants.Water, 2),
                    GetDrinkIngredientByNameAndCount(Constants.Coffee, 1)
            }
        };
        drinks.Add(tea);

        _context.Drinks.AddRange(drinks);
        _context.SaveChanges();
    }

    public DrinkIngredient GetDrinkIngredientByNameAndCount(string name, int doseCount)
    {
        var ingredient = _context.Ingredients.FirstOrDefault(i => i.Name == name);
        return new DrinkIngredient()
        {
            Count = doseCount,
            Ingredient = ingredient
        };
    }

    public void Dispose()
    {
        ((IDisposable)_context).Dispose();
    }
}