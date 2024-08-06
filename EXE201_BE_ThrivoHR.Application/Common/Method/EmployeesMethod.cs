namespace EXE201_BE_ThrivoHR.Application.Common.Method;

public static class EmployeesMethod
{
    public static int ConvertEmployeeCodeToId(string employeeCode)
    {
        bool check = int.TryParse(employeeCode, out int code);
        return check ? code : -1;
    }
}
