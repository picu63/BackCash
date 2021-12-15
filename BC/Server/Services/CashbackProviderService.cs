using BC.Interfaces;
using BC.Models;
using BC.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BC.Server.Services;

public class CashbackProviderService : ICashbackProviderService
{
    private readonly IBCContext context;
    private readonly List<PluginOption> plugins;


    public CashbackProviderService(IBCContext context)
    {
        this.context = context;
    }
    public async Task<ICollection<CashbackProvider>> GetCashbackProvidersAsync()
    {
        return await context.CashbackProviders.ToListAsync();
    }
}