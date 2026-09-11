using ErrorOr;
using MediatR;

namespace OptiERP.Application.UserCommands.GetUserById;

public class GetUserByIdCommand : IRequest<ErrorOr<GetUserByIdResult>>
{
    public Guid UserId { get; internal set; }
}

public record GetUserByIdResult(
    Guid UserId,
    string Username,
    string Email,
    bool IsActive,
    DateTime CreatedAt
);