using EXE201_BE_ThrivoHR.Domain.Services;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace EXE201_BE_ThrivoHR.Application.Common.Behaviours;

public class LoggingBehaviour<TRequest>(ILogger<TRequest> logger, ICurrentUserService currentUserService) : IRequestPreProcessor<TRequest>
     where TRequest : notnull
{
    private readonly ILogger _logger = logger;

    public Task Process(TRequest request, CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;
        string userId = currentUserService.UserId ?? string.Empty;
        string userName = currentUserService.UserName ?? string.Empty;

        _logger.LogInformation("ThrivoHR Request: {Name} {@UserId} {@UserName} {@Request}",
            requestName, userId, userName, request);
        return Task.CompletedTask;
    }
}
