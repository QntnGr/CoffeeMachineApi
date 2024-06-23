namespace CoffeeMachineApi.Entities
{
    public class Drink
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
