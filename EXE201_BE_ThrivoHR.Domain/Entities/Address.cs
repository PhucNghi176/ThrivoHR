using EXE201_BE_ThrivoHR.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE201_BE_ThrivoHR.Domain.Entities;

public class Address : AuditableEntity<int>
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public new int Id { get; set; }
    public required string AddressLine { get; set; }
    public required string Ward { get; set; }
    public required string District { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }
    public string FullAddress
    {
        get
        {
            return $"{AddressLine}, {Ward}, {District}, {City}, {Country}";
        }
    }


}
