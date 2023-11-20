using FluentValidation;
using MediatR;
using Punk.Application.Dtos;
using Punk.Application.Services;
using Punk.Common.Exceptions;
using Punk.Domain.Interfaces.Repositories;

namespace Punk.Application.Features.Auth.Commands;

public class RefreshTokenCommand : IRequest<RefreshTokenDto>
{
    public RefreshTokenCommand(string refreshToken, string expiredToken)
    {
        RefreshToken = refreshToken;
        ExpiredToken = expiredToken;
    }

    public string RefreshToken { get; set; }
    public string ExpiredToken { get; set; }
}

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenDto>
{
    private readonly IJwtService _jwtService;
    private readonly IUserRepository _userRepository;

    public RefreshTokenCommandHandler(IJwtService jwtService, IUserRepository userRepository)
    {
        _jwtService = jwtService;
        _userRepository = userRepository;
    }

    public async Task<RefreshTokenDto> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var username = _jwtService.GetUsernameFromToken(request.ExpiredToken);
        var user = await _userRepository.GetByUsernameAsync(username);

        if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            throw new UnauthorisedException("Token expired. Please login again");
        }

        var newAccessToken = _jwtService.GenerateTokenForUser(user);
        user.RefreshToken = _jwtService.GenerateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddHours(24);

        await _userRepository.UpdateAsync(user);

        return new RefreshTokenDto()
        {
            RefreshToken = user.RefreshToken,
            Token = newAccessToken,
        };
    }
}

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(x => x.RefreshToken)
            .NotEmpty().WithMessage("Unauthorised");
        RuleFor(x => x.ExpiredToken)
            .NotEmpty().WithMessage("Unauthorised");
    }
}