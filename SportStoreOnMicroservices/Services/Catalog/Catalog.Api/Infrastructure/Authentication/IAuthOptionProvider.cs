namespace Catalog.Api.Infrastructure.Authentication
{
    public interface IAuthOptionProvider
    {
        AuthOptions GetSystemAuthOptions();
    }
}