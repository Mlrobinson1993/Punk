using System.Security.Claims;
using FluentValidation;
using MediatR;
using Punk.Application.Dtos;
using Punk.Application.Services;
using Punk.Common.Exceptions;
using Punk.Domain.Interfaces.Repositories;
using Punk.Domain.Models;
using Punk.Services.Helpers;

namespace Punk.Application.Features.Auth.Commands;

public class AuthenticateUserCommand : IRequest<UserDto>
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;
    private readonly IJwtService _jwtService;

    public AuthenticateUserCommandHandler(IUserRepository userRepository, IPasswordService passwordService,
        IJwtService jwtService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _jwtService = jwtService;
    }

    public async Task<UserDto> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Email);

        if (user == null)
            throw new BadRequestException("Username or password is incorrect");

        if (!_passwordService.VerifyPassword(request.Password, user.PasswordHash, user.Salt))
            throw new UnauthorisedException("Username or password is incorrect");

        var token = _jwtService.GenerateTokenForUser(user);
        user.RefreshToken = _jwtService.GenerateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddHours(24);


        await _userRepository.UpdateAsync(user);

        return new UserDto()
        {
            Token = token,
            RefreshToken = user.RefreshToken,
            Id = user.Id,
            Name = user.Name,
            Username = user.Username,
            FavouriteBeers = user.FavouriteBeers?.ToList() ?? new List<FavouriteBeer>()
        };
    }
}

public class AuthenticateUserCommandValidator : AbstractValidator<AuthenticateUserCommand>
{
    public AuthenticateUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required");
        
        RuleFor(p => p.Password)
            .NotEmpty().WithMessage("Password is required");
    }
}