using System;
using BCModels;

namespace BCPlugin.Interfaces.Services;

public interface ICashbackService
{
    Uri BaseUri { get; }
    decimal GetCashback(Shop shop, Category category);
}