using EXE201_BE_ThrivoHR.Domain.Common.Exceptions;

namespace EXE201_BE_ThrivoHR.Application.Common.Exceptions;

public static class AbsentFormException
{
    public class InvalidHoursException : BadRequestException
    {
        public InvalidHoursException() : base("Invalid hours") { }
    }
    public class ExceedLeaveException : BadRequestException
    {
        public ExceedLeaveException() : base("Exceed leave") { }
    }
}
