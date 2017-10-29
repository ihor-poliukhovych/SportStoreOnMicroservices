using Gateway.Api.Entities;

namespace Identity.Api.Providers
{
    public interface IAuthOptionProvider
    {
        AuthOptions GetUserAuthOptions();
        AuthOptions GetSystemAuthOptions();
    }
}