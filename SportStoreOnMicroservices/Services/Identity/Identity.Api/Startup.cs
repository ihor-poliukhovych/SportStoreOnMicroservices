using Identity.Api.DbContext;
using Identity.Api.Entities;
using Identity.Api.Providers;
using Identity.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Api
{    
    public class Startup
    {
        private readonly IConfiguration _configuration;
        
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SportStoreDbContext>(x =>
                x.UseSqlServer(_configuration.GetConnectionString("IdentityDbConnection")));

            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<SportStoreDbContext>();

            services.AddTransient<IJwtTokenService, JwtTokenService>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IAuthOptionProvider, AuthOptionProvider>();
            
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
        }
    }
}