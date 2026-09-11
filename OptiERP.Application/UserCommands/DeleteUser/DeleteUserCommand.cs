using ErrorOr;
using MediatR;

namespace OptiERP.Application.UserCommands.DeleteUser;

public record DeleteUserCommand(
    Guid UserId
) : IRequest<ErrorOr<Success>>;