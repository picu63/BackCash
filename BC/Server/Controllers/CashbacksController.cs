using BC.Interfaces;
using BC.Models;
using BC.Server.Models;
using BC.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace BC.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class CashbacksController : ControllerBase
{
    private readonly ICashbacksService cashbacksService;

    public CashbacksController(ICashbacksService cashbacksService)
    {
        this.cashbacksService = cashbacksService;
    }

    [HttpGet]
    [ProducesDefaultResponseType(typeof(Cashback[]))]
    public async Task<IActionResult> GetCashbacks([FromQuery]long shopId, [FromQuery] long? categoryId)
    {
        var cashbacks = new List<Cashback>();
        await foreach (var cashback in cashbacksService.GetCashbacks(shopId, categoryId))
        {
            cashbacks.Add(cashback);
        }

        return Ok(cashbacks);
    }
}