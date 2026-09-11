using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using OptiERP.Application.Interfaces;

namespace OptiERP.Infrastructure.Authentication;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(
    IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor
            ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public Guid? UserId
    {
        get
        {
            var userId = _httpContextAccessor
                .HttpContext?
                .User
                .FindFirstValue(ClaimTypes.NameIdentifier)
                ?? _httpContextAccessor
                    .HttpContext?
                    .User
                    .FindFirstValue(JwtRegisteredClaimNames.Sub);

            return Guid.TryParse(userId, out var id)
                ? id
                : null;
        }
    }

    public string? Email =>
        _httpContextAccessor
            .HttpContext?
            .User
            .FindFirstValue(ClaimTypes.Email)
        ?? _httpContextAccessor
            .HttpContext?
            .User
            .FindFirstValue(JwtRegisteredClaimNames.Email);

    public string? Username =>
        _httpContextAccessor
            .HttpContext?
            .User
            .FindFirstValue(ClaimTypes.Name)
        ?? _httpContextAccessor
            .HttpContext?
            .User
            .FindFirstValue(JwtRegisteredClaimNames.Name);

    public bool IsAuthenticated =>
        _httpContextAccessor
            .HttpContext?
            .User
            .Identity?
            .IsAuthenticated ?? false;
}