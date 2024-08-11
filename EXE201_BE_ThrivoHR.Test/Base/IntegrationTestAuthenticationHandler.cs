using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace EXE201_BE_ThrivoHR.Test.Base;

internal class IntegrationTestAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{

    public IntegrationTestAuthenticationHandler(
     IOptionsMonitor<AuthenticationSchemeOptions> options,
     ILoggerFactory logger,
     UrlEncoder encoder)
     : base(options, logger, encoder)
    {
        // Configure TimeProvider if needed
        Options.TimeProvider = TimeProvider.System; // or use a custom TimeProvider
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, "000001"),
            new Claim(ClaimTypes.Name, "integrationtestuser"),
            new Claim(ClaimTypes.Role,"Admin"),

        };
        var identity = new ClaimsIdentity(claims, "Test");
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "Test");
        var result = AuthenticateResult.Success(ticket);
        return Task.FromResult(result);
    }
}