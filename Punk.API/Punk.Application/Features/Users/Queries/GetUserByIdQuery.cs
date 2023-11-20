using FluentValidation;
using MediatR;
using Punk.Common.Exceptions;
using Punk.Domain.Interfaces.Repositories;
using Punk.Domain.Models;

namespace Punk.Application.Features.Users.Queries;

public class GetUserByIdQuery : IRequest<User>
{
    public GetUserByIdQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, User>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);

        if (user == null)
            throw new NotFoundException("User not found with id " + request.Id);

        return user;
    }
}

public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(query => query.Id).NotEmpty().WithMessage("User Id is required.");
    }
}