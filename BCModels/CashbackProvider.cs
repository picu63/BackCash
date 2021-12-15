using System.ComponentModel.DataAnnotations;

namespace BC.Models;

public class CashbackProvider
{
    [Key]
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Url { get; set; }
}