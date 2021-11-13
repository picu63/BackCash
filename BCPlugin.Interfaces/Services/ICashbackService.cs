using System;
using System.Net.Http;
using System.Threading.Tasks;
using BC.Models;

namespace BCPlugin.Interfaces.Services;

public interface ICashbackService
{
    HttpClient HttpClient { get; }
    Task<Cashback> GetCashback(Shop shop, Category category);
}