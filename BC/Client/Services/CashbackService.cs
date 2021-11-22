using System.Net.Http.Json;
using BC.Models;

namespace BC.Client.Services;

internal class CashbackService
{
    private readonly HttpClient client;

    public CashbackService(HttpClient client)
    {
        this.client = client;
    }

    public async Task<List<Cashback>> GetCashbacks(long shopId)
    {
        return await client.GetFromJsonAsync<List<Cashback>>($"cashbacks?shopId={shopId}");
    }
}