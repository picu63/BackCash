using BC.DataContext;
using BC.Interfaces;
using BC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static BC.Interfaces.IShopsApi;

namespace BC.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class ShopsController : ControllerBase, IShopsApi
{
    private readonly BCContext context;

    public ShopsController(BCContext context)
    {
        this.context = context;
    }

    [HttpPut]
    [ProducesDefaultResponseType(typeof(Shop))]
    public async Task<IActionResult> AddShop([FromBody]ShopRequest request)
    {
        Shop shop = new Shop() { Name = request.Name };
        await context.Shops.AddAsync(shop);
        await context.SaveChangesAsync();
        return Ok(request);
    }

    [HttpGet]
    [ProducesDefaultResponseType(typeof(List<Shop>))]
    public async Task<IActionResult> GetShops()
    {
        return Ok(await context.Shops.ToListAsync());
    }
}