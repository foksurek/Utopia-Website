using System.ComponentModel.DataAnnotations.Schema;

namespace Utopia.Models.SqlModels;

[Table("users")]
public class SqlUser
{
    [Column("id")]
    public int Id { get; set; }
    [Column("pw_bcrypt")]
    public string Bcrypt { get; set; }
    [Column("name")]
    public string Name { get; set; }
    [Column("safe_name")]
    public string SafeName { get; set; }
    
    [Column("country")]
    public string Country { get; set; }
    
    [Column("donor_end")]
    public long DonorEnd {get; set; }

    [Column("priv")]
    public int Priv { get; set; }
}