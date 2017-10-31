using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [Route("api/catalog/test")]
    public class TestController : Controller
    {
        /// <summary>
        /// Get authentificated user login
        /// </summary>
        /// <returns>Returns user login</returns>
        [Authorize]
        [HttpGet("mylogin")]
        public IActionResult GetMyLogin()
        {
            return Ok($"Your login: {User.Identity.Name}");
        }

        /// <summary>
        /// Get authentificated user role
        /// </summary>
        /// <returns>Returns user role</returns>
        [Authorize(Roles = "test")]
        [HttpGet("myrole")]
        public IActionResult GetMyRole()
        {
            return Ok("Your role: test");
        }
    }
}