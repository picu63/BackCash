using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BC.Interfaces;
using BCDataContext;
using BCPlugin.Interfaces.Api;
using BCPlugin.Interfaces.Services;

namespace BCPluginLetyShops.Controllers
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
        public async Task<decimal> GetCashback([FromQuery]Guid shopId, [FromQuery]Guid? categoryId)
        {
            var shop = dbContext.Shops.SingleOrDefault(s => s.Id == shopId);
            var category = dbContext.Categories.SingleOrDefault(c => c.Id == categoryId);

            return cashbackService.GetCashback(shop, category);
        }
    }
}
