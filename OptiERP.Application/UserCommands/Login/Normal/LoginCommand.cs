using ErrorOr;
using MediatR;

namespace OptiERP.Application.UserCommands.Login.Normal;

public record UserLoginCommand(
    string Email,
    string Password
) : IRequest<ErrorOr<UserLoginResult>>;

public record UserLoginResult(
    string Token,
    Guid UserId,
    string Email
);