using ErrorOr;
using MediatR;

namespace OptiERP.Application.UserCommands.UpdateUser;

public record UpdateUserCommand(
    Guid UserId,
    string Username,
    string Email
) : IRequest<ErrorOr<UpdateUserResult>>;

public record UpdateUserResult(
    Guid UserId,
    string Username,
    string Email,
    bool IsActive,
    DateTime CreatedAt
);