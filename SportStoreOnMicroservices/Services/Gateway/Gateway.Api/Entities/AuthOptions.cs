using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Gateway.Api.Entities
{
    public class AuthOptions
    {
        public int Lifetime { get; }
        public string Issuer { get; }
        public string Audience { get; }
        public SymmetricSecurityKey SecurityKey { get; }

        public AuthOptions(string issuer, string audience, int lifetime, string key)
        {
            Issuer = issuer;
            Audience = audience;
            Lifetime = lifetime;
            SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
        }
    }
}