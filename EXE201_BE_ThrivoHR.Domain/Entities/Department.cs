using EXE201_BE_ThrivoHR.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE201_BE_ThrivoHR.Domain.Entities;

public class Department : AuditableEntity<int>
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public new int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
}
