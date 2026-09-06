using ErrorOr;
using OptiERP.Application.UserCommands.Login.Normal;
namespace OptiERP.Application.UserCommands.UserRegister;

public interface IUserRepository
{
Task<ErrorOr<UserRegisterResult>> RegisterUserAsync(UserRegisterCommand command, CancellationToken cancellationToken = default);

Task<ErrorOr<UserLoginResult>> LoginUserAsync(UserLoginCommand command, CancellationToken cancellationToken = default);
}