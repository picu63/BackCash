using System;
using System.Threading.Tasks;

namespace BCPlugin.Interfaces.Api
{
    public interface ICashbackApi
    {
        /// <summary>
        /// Gets cashback percentage for given shop and category
        /// </summary>
        /// <param name="shopId">Unique id of shop.</param>
        /// <param name="categoryId">Unique id of category.</param>
        /// <returns>Percentage of cashback.</returns>
        Task<decimal> GetCashback(Guid shopId, Guid? categoryId);
    }
}