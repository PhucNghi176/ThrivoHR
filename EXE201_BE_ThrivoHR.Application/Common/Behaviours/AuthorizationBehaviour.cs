using EXE201_BE_ThrivoHR.Application.Common.Exceptions;
using EXE201_BE_ThrivoHR.Application.Common.Security;
using MediatR;
using System.Reflection;

namespace EXE201_BE_ThrivoHR.Application.Common.Behaviours;

public class AuthorizationBehaviour<TRequest, TResponse>(
    ICurrentUserService currentUserService) : IPipelineBehavior<TRequest, TResponse>
   where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var authorizeAttributes = request.GetType().GetCustomAttributes<AuthorizeAttribute>();
        if (!authorizeAttributes.Any())
        {
            return await next();
        }

        EnsureAuthenticatedUserAsync();
        await PerformRoleBasedAuthorizationAsync(authorizeAttributes);
        await PerformPolicyBasedAuthorizationAsync(authorizeAttributes);

        return await next();
    }

    private void EnsureAuthenticatedUserAsync()
    {
        if (currentUserService.UserId == null)
        {
            throw new UnauthorizedAccessException();
        }
    }

    private async Task PerformRoleBasedAuthorizationAsync(IEnumerable<AuthorizeAttribute> authorizeAttributes)
    {
        var authorizeAttributesWithRoles = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Roles));
        foreach (var attribute in authorizeAttributesWithRoles)
        {
            var roles = attribute.Roles.Split(',').Select(r => r.Trim());
            if (!await IsInAnyRoleAsync(roles))
            {
                throw new ForbiddenAccessException();
            }
        }
    }

    private async Task<bool> IsInAnyRoleAsync(IEnumerable<string> roles)
    {
        foreach (var role in roles)
        {
            if (await currentUserService.IsInRoleAsync(role))
            {
                return true;
            }
        }
        return false;
    }

    private async Task PerformPolicyBasedAuthorizationAsync(IEnumerable<AuthorizeAttribute> authorizeAttributes)
    {
        var authorizeAttributesWithPolicies = authorizeAttributes.Where(a => !string.IsNullOrWhiteSpace(a.Policy));
        foreach (var attribute in authorizeAttributesWithPolicies)
        {
            if (!await currentUserService.AuthorizeAsync(attribute.Policy))
            {
                throw new ForbiddenAccessException();
            }
        }
    }
}
    