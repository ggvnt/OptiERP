using ErrorOr;
using MediatR;

namespace OptiERP.Application.UserCommands.UserRegister
{
    public record UserRegisterCommand(
    string Username,
    string Email, 
    string Password) : IRequest<ErrorOr<UserRegisterResult>>;

    public record UserRegisterResult(
        Guid UserId,
        string Username,
        string Email,
        bool IsActive,
        DateTime CreatedAt);
}

