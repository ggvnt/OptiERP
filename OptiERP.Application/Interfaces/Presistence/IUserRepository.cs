using ErrorOr;
using OptiERP.Application.UserCommands.GetCurrentUser;
using OptiERP.Application.UserCommands.GetUserById;
using OptiERP.Application.UserCommands.Login.Normal;
namespace OptiERP.Application.UserCommands.UserRegister;

public interface IUserRepository
{
Task<ErrorOr<UserRegisterResult>> RegisterUserAsync(UserRegisterCommand command, CancellationToken cancellationToken = default);
Task<ErrorOr<UserLoginResult>> LoginUserAsync(UserLoginCommand command, CancellationToken cancellationToken = default);
Task<ErrorOr<GetUserByIdResult>> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default);
}