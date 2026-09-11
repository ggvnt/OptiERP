using ErrorOr;
using MediatR;
using OptiERP.Application.Interfaces;

namespace OptiERP.Application.UserCommands.GetCurrentUser;

public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, ErrorOr<GetCurrentUserResult>>
{
    private readonly ICurrentUserService _currentUserService;

    public GetCurrentUserQueryHandler(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public Task<ErrorOr<GetCurrentUserResult>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        if( !_currentUserService.IsAuthenticated || _currentUserService.UserId is null)
        {
            return Task.FromResult<ErrorOr<GetCurrentUserResult>>(Error.Failure("User is not authenticated"));
        }

        if (_currentUserService.UserId is null ||
            _currentUserService.Email is null ||
            _currentUserService.Username is null)
        {
            return Task.FromResult<ErrorOr<GetCurrentUserResult>>(
                Error.Unauthorized(
                    "User.InvalidClaims",
                    "User claims are missing or invalid."));
        }

        var result = new GetCurrentUserResult(
            _currentUserService.UserId.Value,
            _currentUserService.Email,
            _currentUserService.Username
            );

        return Task.FromResult<ErrorOr<GetCurrentUserResult>>(result);
    }
}