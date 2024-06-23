using SQLiteNetExtensions.Attributes;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CoffeeMachineApi.Entities;

public class Drink
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }

    [OneToMany(CascadeOperations = CascadeOperation.All)]
    public List<DrinkIngredient> DrinkIngredients { get; set; }
}
