using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers
{
    [Route("api/catalog/[controller]")]
    public class TestController : Controller
    {
        /// <summary>
        /// Get authentificated user login
        /// </summary>
        /// <returns>Returns user login</returns>
        [Authorize]
        [HttpGet("[action]")]
        public IActionResult MyLogin()
        {
            return Ok($"Your login: {User.Identity.Name}");
        }

        /// <summary>
        /// Get authentificated user role
        /// </summary>
        /// <returns>Returns user role</returns>
        [Authorize(Roles = "test")]
        [HttpGet("[action]")]
        public IActionResult MyRole()
        {
            return Ok("Your role: test");
        }
    }
}