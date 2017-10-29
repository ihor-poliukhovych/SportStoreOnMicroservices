using Identity.Api.Entities;

namespace Identity.Api.Providers
{
    public class AuthOptionProvider : IAuthOptionProvider
    {   
        //TODO: Remove constants and move all settings to Consul
        private const int _lifetime = 10;
        private const string _issuer = "Ihor-Poliukhovych";
        private const string _audience = "SportStore-On-Microservices";
        private const string _key = "My_User_SecretKey!123";

        public AuthOptions GetUserAuthOptions()
        {
            return new AuthOptions(_issuer, _audience, _lifetime, _key);
        }
    }
}