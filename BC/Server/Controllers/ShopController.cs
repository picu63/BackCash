using BC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BC.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class ShopsController : ControllerBase, IShopsApi
{
    [HttpGet]
    public async Task<IActionResult> GetShops()
    {
        return Ok(new List<Shop>()
        {
            new Shop() { Name = "Allegro" },
            new Shop() { Name = "Olx" },
            new Shop() { Name = "Carrefour" },
            new Shop() {Name = "CCC"}
        });
    }
}

public interface IShopsApi
{
    Task<IActionResult> GetShops();
}