using CoffeeMachineApi.Entities;
using CoffeeMachineApi.Helpers;
using CoffeeMachineApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeMachineApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Authorize]
public class CoffeeMachineController : ControllerBase
{
    private readonly ILogger<CoffeeMachineController> _logger;
    private readonly ICatalogService _catalogService;

    public CoffeeMachineController(ILogger<CoffeeMachineController> logger, ICatalogService catalogService)
    {
        _logger = logger;
        _catalogService = catalogService;
    }

    [HttpGet]
    public async Task<IEnumerable<Drink>> GetAll()
    {
        return  await _catalogService.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<decimal> GetById(int id)
    {
        return await _catalogService.GetPriceByDrinkId(id);
    }


}
