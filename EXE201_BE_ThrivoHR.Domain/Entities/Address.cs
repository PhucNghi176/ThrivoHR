using EXE201_BE_ThrivoHR.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_BE_ThrivoHR.Domain.Entities;

public class Address : AuditableEntity<int>
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string AddressLine { get; set; }
    public string Ward { get; set; }
    public string District { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public string FullAddress
    {
        get
        {
            return $"{AddressLine}, {Ward}, {District}, {City}, {Country}";
        }
    }


}
