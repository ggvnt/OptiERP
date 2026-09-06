using MediatR;
using ErrorOr;
using OptiERP.Application.Interfaces;

using OptiERP.Application.UserCommands.UserRegister;

namespace OptiERP.Application.UserCommands.Login.Normal;

public class LoginCommandHandler : IRequestHandler<UserLoginCommand, ErrorOr<UserLoginResult>>
{
    private readonly IUserRepository _userRepository;

    public LoginCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<UserLoginResult>> Handle(UserLoginCommand command, CancellationToken cancellationToken)
    {
        var result = await _userRepository.LoginUserAsync(command, cancellationToken);
        return result;
    }
}