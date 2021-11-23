using BC.Interfaces;
using BC.Models;
using BC.Server.Models;
using Microsoft.Extensions.Options;

namespace BC.Server.Services;

public class PluginsService : IPluginsService
{
    private readonly IBCContext context;
    private readonly List<PluginOption> plugins;


    public PluginsService(IBCContext context, IOptions<List<PluginOption>> pluginsOptions)
    {
        this.context = context;
        this.plugins = pluginsOptions.Value;
    }
    public async IAsyncEnumerable<Cashback> GetCashback(long shopId, long? categoryId)
    {
        var queryUri = (categoryId is null) ? $"shopId={shopId}" : $"shopId={shopId}&categoryId={categoryId}";

        foreach (var plugin in plugins)
        {
            var baseUri = new Uri(plugin.Url);
            var requestUri = new Uri(baseUri, $"cashback?{queryUri}");
            yield return await new HttpClient().GetFromJsonAsync<Cashback>(requestUri);
        }
    }
}