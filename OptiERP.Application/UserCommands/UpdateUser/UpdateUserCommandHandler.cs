using ErrorOr;
using MediatR;
using OptiERP.Application.UserCommands.Interfaces.Presistence;
namespace OptiERP.Application.UserCommands.UpdateUser;

public class UpdateUserCommandHandler
    : IRequestHandler<
        UpdateUserCommand,
        ErrorOr<UpdateUserResult>>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<UpdateUserResult>> Handle(
        UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        return await _userRepository.UpdateUserAsync(
            request.UserId,
            request.Username,
            request.Email,
            cancellationToken);
    }
}