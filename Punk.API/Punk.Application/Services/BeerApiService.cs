using System.Net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Punk.Application.Dtos;
using Punk.Common.Exceptions;
using Punk.Domain;
using Punk.Domain.Interfaces.Services;
using RestSharp;

namespace Punk.Application.Services;

public class BeerApiService : IBeerApiService
{
    private readonly IRestClient _client;


    public BeerApiService(IConfiguration configuration)
    {
        var baseUrl = configuration.GetValue<string>("PunkBaseUrl");
        _client = new RestClient(baseUrl);
    }

    public async Task<List<BeerDto>> GetBeerAsync(string beerName)
    {
        var request = new RestRequest($"beers?beer_name={beerName}");
        var response = await _client.ExecuteAsync(request);

        if (!response.IsSuccessful)
            HandleErrorResponse(response.StatusCode);


        var beerData = JsonConvert.DeserializeObject<List<BeerDto>>(response?.Content ?? "");

        return beerData;
    }

    public async Task<List<BeerDto>> GetBeersAsync(int page = 1, int perPage = 25, List<int>? ids = null)
    {
        var url = $"beers?page={page}&per_page={25}";

        if (ids?.Any() == true)
        {
            url += $"&ids={string.Join("|", ids)}";
        }

        var request = new RestRequest(url);

        var response = await _client.ExecuteAsync(request);

        if (!response.IsSuccessful)
            HandleErrorResponse(response.StatusCode);

        var beerData = JsonConvert.DeserializeObject<List<BeerDto>>(response?.Content ?? "");

        return beerData ?? new List<BeerDto>();
    }

    private void HandleErrorResponse(HttpStatusCode statusCode)
    {
        switch (statusCode)
        {
            case HttpStatusCode.NotFound:
                throw new NotFoundException("Beer not found");
            case HttpStatusCode.Unauthorized:
                throw new UnauthorisedException("Unauthorized");
            case HttpStatusCode.ServiceUnavailable:
                throw new ServiceUnavailableException("Service unavailable");
            case HttpStatusCode.Forbidden:
                throw new ForbiddenException("Forbidden");
            case HttpStatusCode.BadRequest:
                throw new BadRequestException("Bad request");
            default:
                throw new Exception("Something went wrong");
        }
    }
}