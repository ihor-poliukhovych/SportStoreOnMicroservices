using System.Collections.Generic;
using System.Security.Claims;

namespace Gateway.Api.Services
{
    public interface IJwtTokenService
    {
        string GenerateUserToken(IEnumerable<Claim> claims);
        string GenerateSystemToken(IEnumerable<Claim> claims);
    }
}