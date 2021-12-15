using BC.Interfaces;
using BC.Models;
using Microsoft.EntityFrameworkCore;

namespace BC.Server.Services;

public class CashbacksService : ICashbacksService
{
    private readonly IBCContext context;
    private readonly ICashbackProviderService cashbackProviderService;

    public CashbacksService(IBCContext context, ICashbackProviderService cashbackProviderService)
    {
        this.context = context;
        this.cashbackProviderService = cashbackProviderService;
    }

    public async IAsyncEnumerable<Cashback> GetCashbacks(long shopId, long? categoryId)
    {
        var queryUri = categoryId is null ? $"shopId={shopId}" : $"shopId={shopId}&categoryId={categoryId}";

        foreach (var plugin in await cashbackProviderService.GetCashbackProvidersAsync())
        {
            var baseUri = new Uri(plugin.Url);
            var requestUri = new Uri(baseUri, $"cashback?{queryUri}");
            yield return await new HttpClient().GetFromJsonAsync<Cashback>(requestUri);
        }
    }
}