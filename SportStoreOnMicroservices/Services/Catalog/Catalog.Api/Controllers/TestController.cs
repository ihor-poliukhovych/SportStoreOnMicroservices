using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [Route("api/catalog/test")]
    public class TestController : Controller
    {
        [Authorize]
        [Route("mylogin")]
        public IActionResult GetMyLogin()
        {
            return Ok($"Your login: {User.Identity.Name}");
        }
         
        [Authorize(Roles = "test")]
        [Route("myrole")]
        public IActionResult GetMyRole()
        {
            return Ok("Your role: test");
        }
    }
}