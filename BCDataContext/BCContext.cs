using BC.Interfaces;
using BC.Models;
using Microsoft.EntityFrameworkCore;

namespace BC.DataContext;

public class BCContext : DbContext, IBCContext
{
    public BCContext(DbContextOptions<BCContext> options) : base(options)
    {
        base.Database.EnsureCreated();
    }
    public DbSet<Shop> Shops { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<CashbackProvider> CashbackProviders { get; set; }
    public DbSet<ShopUriAssociation> ShopUriAssociations { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Shop>()
            .Property(s => s.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<Category>()
            .Property(c => c.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<ShopUriAssociation>()
            .Property(sua => sua.Id)
            .ValueGeneratedOnAdd();
        modelBuilder.Entity<CashbackProvider>()
            .Property(cp => cp.Id)
            .ValueGeneratedOnAdd();
        base.OnModelCreating(modelBuilder);
    }
}