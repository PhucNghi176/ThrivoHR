namespace EXE201_BE_ThrivoHR.Domain.Common.Exceptions;

public abstract class DomainException : Exception
{
    protected DomainException(string title, string message)
        : base(message) =>
        Title = title;

    public string Title { get; }
}
