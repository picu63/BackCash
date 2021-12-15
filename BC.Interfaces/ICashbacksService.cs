using BC.Models;

namespace BC.Interfaces;

public interface ICashbacksService
{
    IAsyncEnumerable<Cashback> GetCashbacks(long shopId, long? categoryId);
}