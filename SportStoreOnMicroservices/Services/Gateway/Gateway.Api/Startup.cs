using Gateway.Api.Extentions;
using Gateway.Api.Middleware;
using Gateway.Api.Services;
using Identity.Api.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gateway.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IJwtTokenService, JwtTokenService>();     
            services.AddSingleton<IAuthOptionProvider, AuthOptionProvider>();                   
            services.AddTransient<ProxyMiddleware>();

            var serviceProvider = services.BuildServiceProvider();
            var authOptionProvider = serviceProvider.GetService<IAuthOptionProvider>();
     
            services.AddSportStoreUserAuthentication(authOptionProvider.GetUserAuthOptions());
        }
        
        public void Configure(IApplicationBuilder app)
        {
            app.UseAuthentication();      
            app.UseMiddleware<ProxyMiddleware>();
        }
    }
}