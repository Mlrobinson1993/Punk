using FluentValidation;
using MediatR;
using Punk.Application.Services;

namespace Punk.Application.Features.Auth.Queries;

public class ValidateTokenQuery : IRequest<bool>
{
    public ValidateTokenQuery(string token)
    {
        Token = token;
    }

    public string Token { get; set; }
}

public class ValidateTokenQueryHandler : IRequestHandler<ValidateTokenQuery, bool>
{
    private readonly IJwtService _jwtService;

    public ValidateTokenQueryHandler(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }

    public async Task<bool> Handle(ValidateTokenQuery request, CancellationToken cancellationToken)
    {
        var token = _jwtService.ValidateToken(request.Token);

        return token != null;
    }
}

public class ValidateTokenQueryValidator : AbstractValidator<ValidateTokenQuery>
{
    public ValidateTokenQueryValidator()
    {
        RuleFor(x => x.Token).NotEmpty().WithMessage("Unauthorized");
    }
}