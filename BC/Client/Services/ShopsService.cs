using System.Net.Http.Json;
using BC.Models;

namespace BC.Client.Services;

public class ShopsService
{
    private readonly HttpClient client;

    public ShopsService(HttpClient client)
    {
        this.client = client;
    }
    public async Task<IEnumerable<Shop>> GetShops()
    {
        return await client.GetFromJsonAsync<IEnumerable<Shop>>("shops");
    }
}