using System;
using System.Linq;
using System.Threading.Tasks;
using BC.Interfaces;
using BC.Models;
using BCPlugin.Interfaces.Apis;
using BCPlugin.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace BCPlugin.LetyShops.Controllers;

[ApiController]
[Route("[controller]")]
public class CashbackController : ControllerBase, ICashbackApi
{
    private readonly IBCContext dbContext;
    private readonly ICashbackService cashbackService;

    public CashbackController(IBCContext dbContext, ICashbackService cashbackService)
    {
        this.dbContext = dbContext;
        this.cashbackService = cashbackService;
    }

    [HttpGet]
    public async Task<ActionResult<Cashback>> GetCashback([FromQuery]long shopId, [FromQuery]long? categoryId)
    {
        var shop = dbContext.Shops.SingleOrDefault(s => s.Id == shopId);
        if (shop is null) return NotFound($"No shop with given id: {shopId}");
        if (categoryId is null) return await cashbackService.GetCashback(shop, null);
        var category = dbContext.Categories.SingleOrDefault(c => c.Id == categoryId);
        if (category is null) return NotFound($"No category with given id: {shopId}");
        return await cashbackService.GetCashback(shop, category);
    }
}