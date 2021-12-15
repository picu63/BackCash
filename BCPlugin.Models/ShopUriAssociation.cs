using System.ComponentModel.DataAnnotations;

namespace BCPlugin.Models;

public class ShopUriAssociation
{
    [Key]
    public long Id { get; set; }
    public long ShopId { get; set; }
    public string RelativePath { get; set; }
}