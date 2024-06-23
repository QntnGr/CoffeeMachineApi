using CoffeeMachineApi.Entities;

namespace CoffeeMachineApi.Services
{
    public interface ICatalogService
    {
        /// <summary>
        /// retoune la liste des boissons
        /// </summary>
        /// <returns>list des boissons</returns>
        Task<IEnumerable<Drink>> GetAll();
        
        /// <summary>
        /// Obtenir le prix d'une boisson
        /// </summary>
        /// <param name="id"> identifiant de la boisson</param>
        /// <returns></returns>
        Task<decimal> GetPriceByDrinkId(int id);
    }
}
