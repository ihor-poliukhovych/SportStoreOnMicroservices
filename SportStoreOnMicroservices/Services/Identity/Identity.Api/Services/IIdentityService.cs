using System.Security.Claims;
using System.Threading.Tasks;
using Identity.Api.Models;

namespace Identity.Api.Services
{
    public interface IIdentityService
    {
        Task<ClaimsIdentity> GetIdentity(LoginModel model);
    }
}