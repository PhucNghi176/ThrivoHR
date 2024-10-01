using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE201_BE_ThrivoHR.Domain.Entities;

public class SystemConfig
{
    [Key]
    public required string Key { get; set; }
    [Column(TypeName = "decimal(18,4)")]
    public decimal Value { get; set; }
    

}
