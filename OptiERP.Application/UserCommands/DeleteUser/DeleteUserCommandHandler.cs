using ErrorOr;
using MediatR;
using OptiERP.Application.UserCommands.DeleteUser;
using OptiERP.Application.UserCommands.Interfaces.Presistence;

namespace OptiEPR.Application.UserCommands.DeleteUser;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ErrorOr<Success>>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<Success>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        if (command.UserId == Guid.Empty)
        {
            return Error.Validation(
                "User.InvalidId",
                "The provided user ID is invalid.");
        }

        var isDeleted = await _userRepository.DeleteUserAsync(command.UserId);

        if (isDeleted.IsError)
        {
            return isDeleted.Errors;
        }

        return Result.Success;
    }
}