using Catalog.Api.Entities;

namespace Catalog.Api.Providers
{
    public interface IAuthOptionProvider
    {
        AuthOptions GetSystemAuthOptions();
    }
}