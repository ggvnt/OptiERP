using OptiERP.Domain.Entities;

namespace OptiERP.Application.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}