using BC.Models;

namespace BC.Interfaces;

public interface ICashbackProviderService
{
    Task<ICollection<CashbackProvider>> GetCashbackProvidersAsync();
}