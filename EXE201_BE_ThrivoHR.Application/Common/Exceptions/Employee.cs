namespace EXE201_BE_ThrivoHR.Application.Common.Exceptions;

public static class Employee
{
    public class NotFoundException(string name) : Domain.Common.Exceptions.NotFoundException($"Employee Code {name} was not found.")
    {
    }
}
