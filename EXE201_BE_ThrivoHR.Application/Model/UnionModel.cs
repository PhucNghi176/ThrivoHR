using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXE201_BE_ThrivoHR.Application.Model;

public class UnionModel
{
    public string? EmployeeCode { get; set; }
    public string? Title { get; set; }
    public DateOnly? DateJoined { get; set; }
}
