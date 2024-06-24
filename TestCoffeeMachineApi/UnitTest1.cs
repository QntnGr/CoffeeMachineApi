using CoffeeMachineApi.Entities;
using CoffeeMachineApi.Infrastructure;
using CoffeeMachineApi.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;

namespace TestCoffeeMachineApi
{
    public class UnitTest1
    {
        private Mock<DbSet<T>> CreateMockDbSet<T>(IQueryable<T> data) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            return mockSet;
        }

        private Mock<TestCoffeeContext> CreateMockContext(IEnumerable<Drink> drinks, IEnumerable<Ingredient> ingredients)
        {
            var mockContext = new Mock<TestCoffeeContext>(new DbContextOptions<TestCoffeeContext>());

            var mockDbSetDrinks = CreateMockDbSet(drinks.AsQueryable());
            var mockDbSetIngredients = CreateMockDbSet(ingredients.AsQueryable());

            mockContext.Setup(c => c.Drinks).ReturnsDbSet(mockDbSetDrinks.Object);
            mockContext.Setup(c => c.Ingredients).ReturnsDbSet(mockDbSetIngredients.Object);

            return mockContext;
        }

        [Fact]
        public void GetPriceByDrinkId_ShouldReturnCorrectPrice()
        {
            var ingredients = new List<Ingredient>
            {
                new Ingredient { Id = 1, UnitPrice = 1.0m , Name = "Coffee"},
                new Ingredient { Id = 2, UnitPrice = 2.0m }
            };

            var drinks = new List<Drink>
            {
                new Drink
                {
                    Id = 1,
                    DrinkIngredients = new List<DrinkIngredient>
                    {
                        new DrinkIngredient { Id = 1, Count = 1, Ingredient = ingredients.FirstOrDefault(i=> i.Id == 1)},
                        new DrinkIngredient { Id = 2, Count = 2 , Ingredient = ingredients.FirstOrDefault(i=> i.Id == 2)}
                    }
                }
            };


            var mockContext = CreateMockContext(drinks, ingredients);
            var catalogService = new CatalogService(mockContext.Object);
            int drinkId = 1;
            decimal expectedPrice = (1.0m + 2.0m * 2) * 1.3m;

            // Act
            var price = catalogService.GetPriceByDrinkId(drinkId).Result;

            // Assert
            Assert.Equal(expectedPrice, price);
        }
    }
}