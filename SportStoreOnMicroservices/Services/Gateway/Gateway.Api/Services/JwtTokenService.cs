using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Gateway.Api.Entities;
using Identity.Api.Providers;
using Microsoft.IdentityModel.Tokens;

namespace Gateway.Api.Services
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
            var authOptions = _authOptionProvider.GetUserAuthOptions();
            
            return GenerateToken(claims, authOptions);
        }
        
        public string GenerateSystemToken(IEnumerable<Claim> claims)
        {
            var authOptions = _authOptionProvider.GetSystemAuthOptions();
            
            return GenerateToken(claims, authOptions);
        }
        
        private string GenerateToken(IEnumerable<Claim> claims, AuthOptions authOptions)
        {
            if (claims == null)
                throw new ArgumentNullException(nameof(claims));

            if (authOptions == null)
                throw new ArgumentNullException(nameof(authOptions));

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken
            (
                issuer: authOptions.Issuer,
                audience: authOptions.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(authOptions.Lifetime)),
                signingCredentials: new SigningCredentials
                (
                    authOptions.SecurityKey,
                    SecurityAlgorithms.HmacSha256
                )
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}