using BCPlugin.Models;
using Microsoft.EntityFrameworkCore;

namespace BCPlugin.Interfaces.Repositories;

public interface IPluginDbContext
{
    DbSet<ShopUriAssociation> ShopUriAssociations { get; set; }
}