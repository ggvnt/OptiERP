using ErrorOr;
using OptiERP.Application.UserCommands.GetAllUsers;
using OptiERP.Application.UserCommands.GetUserById;
using OptiERP.Application.UserCommands.Login.Normal;
using OptiERP.Application.UserCommands.UpdateUser;
using OptiERP.Application.UserCommands.UserRegister;
namespace OptiERP.Application.UserCommands.Interfaces.Presistence;

public interface IUserRepository
{
    Task<ErrorOr<UserRegisterResult>> RegisterUserAsync(UserRegisterCommand command, CancellationToken cancellationToken = default);
    Task<ErrorOr<UserLoginResult>> LoginUserAsync(UserLoginCommand command, CancellationToken cancellationToken = default);
    Task<ErrorOr<GetUserByIdResult>> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<ErrorOr<List<GetAllUsersResult>>> GetAllUsersAsync(
        CancellationToken cancellationToken = default);
    Task<ErrorOr<UpdateUserResult>> UpdateUserAsync(
    Guid userId,
    string username,
    string email,
    CancellationToken cancellationToken = default);
    Task<ErrorOr<bool>> DeleteUserAsync(
        Guid userId,
        CancellationToken cancellationToken = default);
}