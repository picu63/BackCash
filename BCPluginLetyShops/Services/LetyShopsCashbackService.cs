using System;
using BCModels;
using BCPlugin.Interfaces.Services;

namespace BCPluginLetyShops.Services;

public class LetyShopsCashbackService : ICashbackService
{
    public Uri BaseUri { get; }
    public decimal GetCashback(Shop shop, Category category)
    {
        throw new NotImplementedException();
    }
}