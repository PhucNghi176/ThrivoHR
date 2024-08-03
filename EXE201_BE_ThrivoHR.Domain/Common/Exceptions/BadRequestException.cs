namespace EXE201_BE_ThrivoHR.Domain.Common.Exceptions;

public abstract class BadRequestException : DomainException
{
    protected BadRequestException(string message)
        : base("Bad Request", message)
    {
    }
}
