using Identity.Api.Entities;

namespace Identity.Api.Providers
{
    public interface IAuthOptionProvider
    {
        AuthOptions GetUserAuthOptions();
    }
}