using System.Text.Json.Serialization;

namespace Punk.Application.Dtos;

public class RefreshTokenDto
{
    public string RefreshToken { get; set; }
    public string Token { get; set; }
}