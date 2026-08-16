using ErrorOr;
namespace OptiERP.Application.UserCommands.UserRegister;

public interface IUserRepository
{
Task<ErrorOr<UserRegisterResult>> RegisterUserAsync(UserRegisterCommand command, CancellationToken cancellationToken = default);
}