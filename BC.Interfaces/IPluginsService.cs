using BC.Models;

namespace BC.Interfaces;

public interface IPluginsService
{
    IAsyncEnumerable<Cashback> GetCashback(long shopId, long? categoryId);
}