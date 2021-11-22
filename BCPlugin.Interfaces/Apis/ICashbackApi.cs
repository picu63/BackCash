using System;
using System.Threading.Tasks;
using BC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BCPlugin.Interfaces.Apis;

public interface ICashbackApi
{
    /// <summary>
    /// Gets cashback percentage for given shop and category
    /// </summary>
    /// <param name="shopId">Unique id of shop.</param>
    /// <param name="categoryId">Unique id of category.</param>
    /// <returns>Cashback for purchase.</returns>
    Task<ActionResult<Cashback>> GetCashback(long shopId, long? categoryId);
}