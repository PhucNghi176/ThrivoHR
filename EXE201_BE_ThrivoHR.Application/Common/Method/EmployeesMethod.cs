using EXE201_BE_ThrivoHR.Domain.Entities.Contracts;
using EXE201_BE_ThrivoHR.Domain.Entities.Identity;

namespace EXE201_BE_ThrivoHR.Application.Common.Method;

public static class EmployeesMethod
{
    public static int ConvertEmployeeCodeToId(string employeeCode)
    {
        bool check = int.TryParse(employeeCode, out int code);
        return check ? code : -1;
    }

    public static AppUser SetDepartmentAndPostionForEmployee(AppUser employee, EmployeeContract contract)
    {
        employee.DepartmentId = contract.DepartmentId;
        employee.PositionId = contract.PositionId;
        return employee;
    }
    public static AppUser SetDepartmentAndPostionForEmployee(AppUser employee)
    {
        employee.DepartmentId = null;
        employee.PositionId = null;
        return employee;
    }

}
