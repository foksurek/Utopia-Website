using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Utopia.Models.SqlModels;

[Table("stats")]
public class Stat
{

    [Column("id")]
    public int Id { get; set; }

    [Column("mode")]
    public byte Mode { get; set; }
    
    [Column("pp")]
    public int PP { get; set; }
    
    [Column("acc")]
    public double Acc { get; set; }
    
    [Column("plays")]
    public int PlayCount { get; set; }
    
    [Column("tscore")]
    public long TScore;
    
}