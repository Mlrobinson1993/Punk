namespace Punk.Application.Dtos;

public class AuthDto
{
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public UserDto User { get; set; }
}