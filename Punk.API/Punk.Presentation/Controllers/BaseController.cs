using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Punk.Common.Exceptions;
using Punk.Presentation.Models;

namespace Punk.Presentation.Controllers;

[Authorize(AuthenticationSchemes = "JwtAuthScheme")]
public class BaseController : ControllerBase
{
    public IMediator _mediator;

    public BaseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected async Task<IActionResult> ProcessMediator<T>(T request, int successStatusCode = 200,
        int failedStatusCode = 404) where T : class
    {
        try
        {
            var result = await _mediator.Send(request);

            var apiResponse = result == null
                ? new ApiResponse()
                {
                    StatusCode = failedStatusCode,
                    Success = false,
                    Message = "Unsuccessful",
                    Data = { }
                }
                : new ApiResponse()
                {
                    StatusCode = successStatusCode,
                    Success = true,
                    Message = "Success",
                    Data = result
                };

            return new ObjectResult(apiResponse)
            {
                StatusCode = successStatusCode
            };
        }
        catch (Exception e)
        {
            var apiResponse = HandleException(e, failedStatusCode);
            return new ObjectResult(apiResponse)
            {
                StatusCode = apiResponse.StatusCode
            };
        }
    }

    protected async Task<ApiResponse> ProcessMediatorForData<T>(T request, int successStatusCode = 200,
        int failedStatusCode = 404) where T : class
    {
        try
        {
            var result = await _mediator.Send(request);

            return result == null
                ? new ApiResponse()
                {
                    StatusCode = failedStatusCode,
                    Success = false,
                    Message = "Unsuccessful",
                    Data = { }
                }
                : new ApiResponse()
                {
                    StatusCode = successStatusCode,
                    Success = true,
                    Message = "Success",
                    Data = result
                };
        }
        catch (Exception e)
        {
            return HandleException(e, failedStatusCode);
        }
    }


    private static ApiResponse HandleException(Exception exception, int failedStatusCode = 404)
    {
        var apiResponse = new ApiResponse
        {
            Success = false,
            Message = exception.Message,
            Data = { }
        };

        switch (exception)
        {
            case UnauthorisedException:
                apiResponse.StatusCode = StatusCodes.Status401Unauthorized;
                break;
            case ForbiddenException:
                apiResponse.StatusCode = StatusCodes.Status403Forbidden;
                break;
            case BadRequestException:
                apiResponse.StatusCode = StatusCodes.Status400BadRequest;
                break;
            case NotFoundException:
                apiResponse.StatusCode = StatusCodes.Status404NotFound;
                break;
            case ServiceUnavailableException:
                apiResponse.StatusCode = StatusCodes.Status503ServiceUnavailable;
                break;
            default:
                apiResponse.Message = "An error occurred";
                apiResponse.StatusCode = failedStatusCode;
                break;
        }

        return apiResponse;
    }
}