﻿using EXE201_BE_ThrivoHR.Domain.Common.Exceptions;

namespace EXE201_BE_ThrivoHR.Application.Common.Exceptions;

public static class EmployeeContractExceptions
{
    public class NotFoundException(string name) : Domain.Common.Exceptions.NotFoundException($"Employee Contract Code {name} was not found.") { }
    public class CreateFailureException(string name) : BadRequestException($"Failed to create {name}");
    public class DuplicateException(string name, object key) : BadRequestException($"Failed to create {name} with dupplication key {key}")
    {
    }

    public class CurrentContractNotEndedException(string name, object key) : BadRequestException($"Failed to create {name} with dupplication key {key} because current contract is still valid.")
    {
    }
}