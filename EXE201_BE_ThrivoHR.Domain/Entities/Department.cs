using EXE201_BE_ThrivoHR.Domain.Entities.Base;
using EXE201_BE_ThrivoHR.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE201_BE_ThrivoHR.Domain.Entities;

public class Department : AuditableEntity<int>
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public new int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }

    public string? HeadOfDepartmentId { get; set; }
    public virtual AppUser? HeadOfDepartment { get; set; }

}
