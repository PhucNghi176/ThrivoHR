using System.ComponentModel.DataAnnotations.Schema;

namespace EXE201_BE_ThrivoHR.Domain.Entities.Contracts;

public class EmployeeContract : BaseContract
{
    public string? WorkAddress { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Salary { get; set; }

}
