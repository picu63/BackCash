using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BC.Models;

[Table("ShopUriAssociations", Schema = "pgin")]
public class ShopUriAssociation
{
    [Key]
    public long Id { get; set; }
    public Shop Shop { get; set; }
    public CashbackProvider CashbackProvider { get; set; }
    public string RelativePath { get; set; }
}