using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BC.Interfaces;

public interface IShopsApi
{
    Task<IActionResult> AddShop(ShopRequest shop);
    Task<IActionResult> GetShops();
    public class ShopRequest
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}