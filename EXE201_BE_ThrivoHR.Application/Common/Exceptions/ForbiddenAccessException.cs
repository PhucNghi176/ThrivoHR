using System.Net;

namespace EXE201_BE_ThrivoHR.Application.Common.Exceptions
{
    public class ForbiddenAccessException : Exception
    {
        public ForbiddenAccessException() : base() { }
        public ForbiddenAccessException(string message) : base(message) { }
    }
}
