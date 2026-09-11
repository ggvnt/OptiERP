using ErrorOr;
using MediatR;
using OptiERP.Application.UserCommands.Interfaces.Presistence;


namespace OptiERP.Application.UserCommands.GetUserById;

public class GetUserByIdCommandHandler : IRequestHandler<GetUserByIdCommand, ErrorOr<GetUserByIdResult>>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<ErrorOr<GetUserByIdResult>> Handle(
        GetUserByIdCommand request,
        CancellationToken cancellationToken)
    {
        return await _userRepository.GetUserByIdAsync(
            request.UserId,
            cancellationToken);
    }
}