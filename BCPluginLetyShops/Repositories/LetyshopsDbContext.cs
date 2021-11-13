using BCPlugin.Interfaces.Repositories;
using BCPlugin.Models;
using Microsoft.EntityFrameworkCore;

namespace BCPlugin.LetyShops.Repositories;

public class LetyshopsDbContext : DbContext, IPluginDbContext
{
    public DbSet<ShopUriAssociation> ShopUriAssociations { get; set; }
}