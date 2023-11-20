using MediatR;
using Microsoft.AspNetCore.Mvc;
using Punk.Application.Features.Users.Queries;
using Punk.Application.Features.Users.Commands;
using Microsoft.AspNetCore.Authorization;
using Punk.Application.Services;


namespace Punk.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : BaseController
{
    public UserController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet("{id}", Name = "GetUser")]
    public async Task<IActionResult> Get(Guid id)
    {
        return await ProcessMediator(new GetUserByIdQuery(id));
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create([FromForm] CreateUserCommand command)
    {
        return await ProcessMediator(command, 201, 400);
    }
}