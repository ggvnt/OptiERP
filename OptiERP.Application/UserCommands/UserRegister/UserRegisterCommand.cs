using ErrorOr;
using MediatR;
using OptiERP.Domain.Entities;
using OptiERP.Domain.Entities.UserAggregate.Model;

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
        UserType UserType,
        bool IsActive,
        DateTime CreatedAt,
        string Token);
}

