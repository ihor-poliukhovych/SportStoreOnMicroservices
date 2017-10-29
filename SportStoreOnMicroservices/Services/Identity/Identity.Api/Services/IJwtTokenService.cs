using System.Collections.Generic;
using System.Security.Claims;

namespace Identity.Api.Services
{
    public interface IJwtTokenService
    {
        string GenerateUserToken(IEnumerable<Claim> claims);
    }
}