namespace CoffeeMachineApi.Entities
{
    public class DrinkIngredient
    {
        public int Id {  get; set; }
        public int Count { get; set; }
        public Ingredient? Ingredient { get; set; }
    }
}
