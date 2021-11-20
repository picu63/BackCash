using System.ComponentModel.DataAnnotations;

namespace BC.Models;

public class Shop
{
    [Key]
    public long Id { get; set; }
    public string Name { get; set; }
}