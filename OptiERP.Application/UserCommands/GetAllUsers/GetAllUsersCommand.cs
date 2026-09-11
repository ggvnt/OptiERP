using ErrorOr;
using MediatR;

namespace OptiERP.Application.UserCommands.GetAllUsers;

public record GetAllUsersCommand
    : IRequest<ErrorOr<List<GetAllUsersResult>>>;

public record GetAllUsersResult(
    Guid UserId,
    string Username,
    string Email,
    bool IsActive,
    DateTime CreatedAt
);