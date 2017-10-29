using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Identity.Api.Providers;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Api.Services
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IAuthOptionProvider _authOptionProvider;

        public JwtTokenService(IAuthOptionProvider authOptionProvider)
        {
            _authOptionProvider = authOptionProvider;
        }
        
        public string GenerateUserToken(IEnumerable<Claim> claims)
        {
            if (claims == null)
                throw new ArgumentNullException(nameof(claims));

            var authOptons = _authOptionProvider.GetUserAuthOptions();
            if (authOptons == null)
                throw new InvalidOperationException("Auth options required");
            
            var now = DateTime.UtcNow;
            
            var jwt = new JwtSecurityToken
            (
                issuer: authOptons.Issuer,
                audience: authOptons.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(authOptons.Lifetime)),
                signingCredentials: new SigningCredentials
                (
                    authOptons.SecurityKey,
                    SecurityAlgorithms.HmacSha256
                )
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}