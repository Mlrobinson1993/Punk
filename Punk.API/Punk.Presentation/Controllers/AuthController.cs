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
        return await ProcessMediator(command, 200, 401);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand command)
    {
        return await ProcessMediator(command, 200, 401);
    }

    [HttpPost("logout")]
    [AllowAnonymous]
    public async Task<IActionResult> Logout()
    {
        return await ProcessMediator(new DeauthenticateUserCommand(GetTokenFromHeader()), 200, 400);
    }

    [HttpGet("validate")]
    public async Task<IActionResult> Validate()
    {
        return await ProcessMediator(new ValidateTokenQuery(GetTokenFromHeader()), 200, 400);
    }
}