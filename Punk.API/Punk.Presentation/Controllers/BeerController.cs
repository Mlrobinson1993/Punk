using MediatR;
using Microsoft.AspNetCore.Mvc;
using Punk.Application.Features.Beer.Queries;
using Punk.Application.Features.Beer.Commands;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Punk.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BeerController : BaseController
{
    public BeerController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetMany([FromQuery] GetBeersQuery query)
    {
        return await ProcessMediator(query);
    }

    [HttpGet("search")]
    public async Task<IActionResult> GetByName([FromQuery] string name)
    {
        return await ProcessMediator(new GetBeerByNameQuery(name));
    }


    [HttpPut("favourite")]
    public async Task<IActionResult> Favourite([FromBody] FavouriteBeerCommand command)
    {
        return await ProcessMediator(command, 200, 400);
    }

    [HttpGet("favourites")]
    public async Task<IActionResult> GetFavourites()
    {
        var token = Request.Cookies["AccessToken"];
        var pageSize = Int32.TryParse(Request.Query["pageSize"].ToString(), out int size) ? size : 25;
        var page = Int32.TryParse(Request.Query["page"].ToString(), out int p) ? p : 1;
        return await ProcessMediator(new GetFavouriteBeersQuery(token, page, pageSize));
    }
}