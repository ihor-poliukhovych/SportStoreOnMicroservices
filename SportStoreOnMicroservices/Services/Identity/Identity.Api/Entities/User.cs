using Microsoft.AspNetCore.Identity;

namespace Identity.Api.Entities
{
    public class User : IdentityUser
    {
        public string Role { get; set; }
    }
}