using Azure.Core;
using FluentValidation;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Punk.Application.Services;
using Punk.Common.Exceptions;
using Punk.Domain.Interfaces.Repositories;

namespace Punk.Application.Features.Auth.Commands;

public class DeauthenticateUserCommand : IRequest<bool>
{
    public DeauthenticateUserCommand(string token)
    {
        Token = token;
    }

    public string Token { get; set; }
}

public class DeauthenticateUserCommandHandler : IRequestHandler<DeauthenticateUserCommand, bool>
{
    private readonly IJwtService _jwtService;
    private readonly IUserRepository _userRepository;

    public DeauthenticateUserCommandHandler(IJwtService jwtService, IUserRepository userRepository)
    {
        _jwtService = jwtService;
        _userRepository = userRepository;
    }

    public async Task<bool> Handle(DeauthenticateUserCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Token)) return true;

        var userId = _jwtService.GetUserIdFromToken(request.Token);
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null)
            throw new NotFoundException("User not found");

        user.RefreshToken = null;

        return await _userRepository.UpdateAsync(user);
    }
}

public class DeauthenticateUserCommandValidator : AbstractValidator<DeauthenticateUserCommand>
{
    public DeauthenticateUserCommandValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Unauthorised");
    }
}