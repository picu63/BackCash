using BC.Models;
using Microsoft.EntityFrameworkCore;

namespace BC.Interfaces
{
    public interface IBCContext
    {
        DbSet<Shop> Shops { get; set; }
        DbSet<Category> Categories { get; set; }
    }
}