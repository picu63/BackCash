using BC.Interfaces;
using BC.Models;
using Microsoft.EntityFrameworkCore;

namespace BC.DataContext
{
    public class BCContext : DbContext, IBCContext
    {
        public BCContext(DbContextOptions<BCContext> options) : base(options)
        {
            base.Database.EnsureCreated();
        }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shop>()
                .Property(s => s.Id)
                .ValueGeneratedOnAdd();
            modelBuilder.Entity<Category>()
                .Property(s => s.Id)
                .ValueGeneratedOnAdd();
            base.OnModelCreating(modelBuilder);
        }
    }
}
