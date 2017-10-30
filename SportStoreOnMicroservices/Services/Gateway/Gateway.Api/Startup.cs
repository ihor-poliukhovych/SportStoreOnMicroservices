using Gateway.Api.Extentions;
using Gateway.Api.Middleware;
using Gateway.Api.Services;
using Identity.Api.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Gateway.Api
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
            services.AddTransient<IJwtTokenService, JwtTokenService>();     
            services.AddTransient<IAuthOptionProvider, AuthOptionProvider>();                   
            services.AddTransient<ProxyMiddleware>();

            var serviceProvider = services.BuildServiceProvider();
            var authOptionProvider = serviceProvider.GetService<IAuthOptionProvider>();
     
            services.AddSportStoreUserAuthentication(authOptionProvider.GetUserAuthOptions());
        }
        
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(_configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            
            app.UseAuthentication();      
            app.UseMiddleware<ProxyMiddleware>();
        }
    }
}