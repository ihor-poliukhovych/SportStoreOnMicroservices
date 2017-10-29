using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Identity.Api.Entities;
using Identity.Api.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.Api.Services
{    
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        
        public IdentityService(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;   
        }
        
        public async Task<ClaimsIdentity> GetIdentity(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Username);
            if (user == null)
                return null;
            
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
                return null;

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role)
            };

            return new ClaimsIdentity
            (
                claims, "Token",
                nameType: ClaimsIdentity.DefaultNameClaimType,
                roleType: ClaimsIdentity.DefaultRoleClaimType
            );
        }
    }
}