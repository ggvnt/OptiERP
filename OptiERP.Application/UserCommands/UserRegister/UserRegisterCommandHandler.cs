using ErrorOr;
using MediatR;

namespace OptiERP.Application.UserCommands.UserRegister;

public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand, ErrorOr<UserRegisterResult>>
{
    private readonly IUserRepository _userRepository;

    public UserRegisterCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<UserRegisterResult>> Handle(UserRegisterCommand command, CancellationToken cancellationToken)
    {
        try
        {
            return await _userRepository.RegisterUserAsync(command);
        }
        catch (Exception ex)
        {
            // Log the exception if needed
            return Error.Failure(description: $"An error occurred while signing up user: {ex.Message}");
        }
    }
}