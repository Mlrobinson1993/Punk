using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Punk.Application.Services;

namespace Punk.Presentation.Handlers;

public class JwtAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    private readonly IJwtService _jwtService;

    public JwtAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        ISystemClock clock,
        IJwtService jwtService)
        : base(options, logger, encoder, clock)
    {
        _jwtService = jwtService;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var accessToken = Request.Cookies["AccessToken"];

        if (accessToken == null)
        {
            return AuthenticateResult.NoResult();
        }

        var validatedToken = _jwtService.ValidateToken(accessToken);

        if (string.IsNullOrEmpty(accessToken) || validatedToken == null)
        {
            return AuthenticateResult.Fail("Invalid token");
        }

        var claims = validatedToken.Claims;
        var identity = new ClaimsIdentity(claims, Scheme.Name);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);

        return AuthenticateResult.Success(ticket);
    }
}