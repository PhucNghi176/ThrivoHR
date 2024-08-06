﻿using EXE201_BE_ThrivoHR.Domain.Common.Exceptions;

namespace EXE201_BE_ThrivoHR.Application.Common.Exceptions;

public static class Employee
{
    public class NotFoundException(string name) : Domain.Common.Exceptions.NotFoundException($"Employee Code {name} was not found.") { }
    public class PasswordMismatchException : BadRequestException
    {
        public PasswordMismatchException() : base("Password mismatch")
        {
        }
    }
    public class CreateFailureException(string name, object key) : BadRequestException($"Failed to create {name} with key {key}")
    {
    }
    public class RoleNotFoundException : BadRequestException
    {
        public RoleNotFoundException() : base("Role not found")
        {
        }
    }
}
