using CoffeeMachineApi.Entities;
using CoffeeMachineApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeMachineApi.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CoffeeMachineController : ControllerBase
{
    private readonly ILogger<CoffeeMachineController> _logger;
    private readonly ICatalogService _catalogService;

    public CoffeeMachineController(ILogger<CoffeeMachineController> logger, ICatalogService catalogService)
    {
        _logger = logger;
        _catalogService = catalogService;
    }

    [HttpGet(Name = "GetDrinks")]
    public async Task<IEnumerable<Drink>> GetAll()
    {
        return  await _catalogService.GetAll();
    }

    [HttpGet("{id}", Name = "GetPriceByDrinkId")]
    public async Task<decimal> GetById(int id)
    {
        return await _catalogService.GetPriceByDrinkId(id);
    }


}
