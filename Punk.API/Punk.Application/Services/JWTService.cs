using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Punk.Domain.Models;


namespace Punk.Application.Services;

public interface IJwtService
{
    string GenerateTokenForUser(User user);
    ClaimsPrincipal? ValidateToken(string token);
    string GetUsernameFromToken(string token);
    Guid GetUserIdFromToken(string token);

    string GenerateRefreshToken();
}

public class JwtService : IJwtService
{
    private readonly string _secret;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly int _expirationMinutes;

    public JwtService(JwtConfig jwtConfig)
    {
        _secret = jwtConfig.Secret;
        _issuer = jwtConfig.Issuer;
        _audience = jwtConfig.Audience;
        _expirationMinutes = jwtConfig.ExpirationMinutes;
    }

    public string GenerateTokenForUser(User user) => GenerateToken(new[]
    {
        new Claim(ClaimTypes.Email, user.Username),
        new Claim(ClaimTypes.PrimarySid, user.Id.ToString()),
    });

    public ClaimsPrincipal? ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var validationParameters = GetValidationParameters();

        try
        {
            return tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
        }
        catch
        {
            return null;
        }
    }

    public string GetUsernameFromToken(string token)
    {
        var principal = GetPrincipalFromExpiredToken(token);
        return principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value ?? "";
    }

    public Guid GetUserIdFromToken(string token)
    {
        var principal = GetPrincipalFromExpiredToken(token);
        return Guid.Parse(principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.PrimarySid)?.Value ?? "");
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = GetValidationParameters();
        tokenValidationParameters.ValidateLifetime = false; // We are checking expired token here

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return principal;
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }

    private string GenerateToken(Claim[] claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_expirationMinutes),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private TokenValidationParameters GetValidationParameters()
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _issuer,
            ValidAudience = _audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret))
        };
    }
}