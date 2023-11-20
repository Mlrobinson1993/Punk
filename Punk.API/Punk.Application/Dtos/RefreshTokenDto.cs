using System.Text.Json.Serialization;

namespace Punk.Application.Dtos;

public class RefreshTokenDto
{
    [JsonIgnore] public string RefreshToken { get; set; }
    [JsonIgnore] public string Token { get; set; }
}