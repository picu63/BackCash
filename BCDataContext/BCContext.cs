using System;
using BC.Interfaces;
using BCModels;
using Microsoft.EntityFrameworkCore;

namespace BCDataContext
{
    public class BCContext : DbContext, IBCContext
    {
        public DbSet<Shop> Shops { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
