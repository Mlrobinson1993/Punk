using FluentValidation;
using MediatR;
using Punk.Common.Exceptions;
using Punk.Domain.Interfaces.Repositories;
using Punk.Domain.Models;
using Punk.Services.Helpers;


namespace Punk.Application.Features.Users.Commands;

public class CreateUserCommand : IRequest<bool>
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordService _passwordService;

    public CreateUserCommandHandler(IUserRepository userRepository, IPasswordService passwordService)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
    }

    public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var salt = _passwordService.GenerateSalt();
        var passwordHash = _passwordService.HashPassword(request.Password, salt);

        var userCreated = await _userRepository.CreateAsync(new User
        {
            Id = Guid.NewGuid(),
            Username = request.Email,
            Name = request.Name,
            PasswordHash = passwordHash,
            Salt = salt,
            FavouriteBeers = new List<FavouriteBeer>()
        });

        if (!userCreated)
            throw new BadRequestException("Error creating user");

        return true;
    }
}

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email must be a valid email address");

        RuleFor(p => p.Password)
            .NotEmpty().WithMessage("Password is required")
            .MinimumLength(8).WithMessage("Your password length must be at least 8.")
            .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
            .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
            .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
            .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
            .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).");
    }
}