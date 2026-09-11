using ErrorOr;
using MediatR;

namespace OptiERP.Application.UserCommands.GetCurrentUser;

public class GetCurrentUserQuery : IRequest<ErrorOr<GetCurrentUserResult>>;
public record GetCurrentUserResult(
    Guid UserId,
    string Email,
    string Username
);