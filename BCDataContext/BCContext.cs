using BC.Interfaces;
using BC.Models;
using Microsoft.EntityFrameworkCore;

namespace BC.DataContext
{
    public class BCContext : DbContext, IBCContext
    {
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
