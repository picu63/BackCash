using System;
using System.Linq;
using System.Threading.Tasks;
using BC.Interfaces;
using BC.Models;
using BCPlugin.Interfaces.Apis;
using BCPlugin.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace BCPlugin.LetyShops.Controllers
{
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
        public async Task<ActionResult<Cashback>> GetCashback([FromQuery]Guid shopId, [FromQuery]Guid? categoryId)
        {
            var shop = dbContext.Shops.SingleOrDefault(s => s.Id == shopId);
            if (shop == null) return NotFound($"No shop with given id: {shopId}");
            var category = dbContext.Categories.SingleOrDefault(c => c.Id == categoryId);
            if (category == null) return NotFound($"No category with given id: {shopId}");
            return await cashbackService.GetCashback(shop, category);
        }
    }
}
