using ErrorOr;
using MediatR;
using OptiERP.Application.UserCommands.Interfaces.Presistence;

namespace OptiERP.Application.UserCommands.GetAllUsers;

public class GetAllUsersCommandHandler
    : IRequestHandler<
        GetAllUsersCommand,
        ErrorOr<List<GetAllUsersResult>>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersCommandHandler(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<List<GetAllUsersResult>>> Handle(
        GetAllUsersCommand request,
        CancellationToken cancellationToken)
    {
        return await _userRepository.GetAllUsersAsync(
            cancellationToken);
    }
}