using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Punk.Application.Dtos;
using Punk.Application.Features.Auth.Commands;
using Punk.Application.Features.Auth.Queries;
using Punk.Application.Services;
using Punk.Presentation.Models;


namespace Punk.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : BaseController
{
    public AuthController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost("authenticate")]
    [AllowAnonymous]
    public async Task<IActionResult> Authenticate([FromForm] AuthenticateUserCommand command)
    {
        var response = await ProcessMediatorForData(command, 200, 401);

        if (response.Success)
        {
            var authData = (UserDto)response.Data;
            AddCookies(authData.Token, authData.RefreshToken);
        }

        return new ObjectResult(response)
        {
            StatusCode = response.StatusCode
        };
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken()
    {
        var refreshToken = Request.Cookies["RefreshToken"];
        var expiredToken = Request.Cookies["AccessToken"];
        var response = await ProcessMediatorForData(new RefreshTokenCommand(refreshToken, expiredToken), 200, 401);

        if (response.Success)
        {
            var tokenData = (RefreshTokenDto)response.Data;
            AddCookies(tokenData.Token, tokenData.RefreshToken);
        }

        return new ObjectResult(response)
        {
            StatusCode = response.StatusCode
        };
    }

    [HttpPost("logout")]
    [AllowAnonymous]
    public async Task<IActionResult> Logout()
    {
        var token = Request.Cookies["AccessToken"];

        RemoveCookies();

        return await ProcessMediator(new DeauthenticateUserCommand(token), 200, 400);
    }

    [HttpGet("validate")]
    public async Task<IActionResult> Validate()
    {
        return await ProcessMediator(new ValidateTokenQuery(Request.Cookies["AccessToken"]), 200, 400);
    }

    private void AddCookies(string accessToken, string refreshToken)
    {
        SetCookie("AccessToken", accessToken, 720);
        SetCookie("RefreshToken", refreshToken, 1440);
    }

    private void RemoveCookies()
    {
        Response.Cookies.Delete("AccessToken");
        Response.Cookies.Delete("RefreshToken");
    }

    private void SetCookie(string key, string value, int expireTimeInMinutes)
    {
        var cookieOptions = new CookieOptions
        {
            Secure = true,
            HttpOnly = true,
            Expires = DateTimeOffset.UtcNow.AddMinutes(expireTimeInMinutes),
            SameSite = SameSiteMode.None,
        };

        Response.Cookies.Append(key, value, cookieOptions);
    }
}