using Catalog.Api.Extentions;
using Catalog.Api.Providers;
using Identity.Api.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Catalog.Api
{
    public class Startup
    {
        private const string SwaggerPageName = "Catalog Api";
        
        private readonly IConfiguration _configuration;
        
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IAuthOptionProvider, AuthOptionProvider>();    
            
            var serviceProvider = services.BuildServiceProvider();
            var authOptionProvider = serviceProvider.GetService<IAuthOptionProvider>();
     
            services.AddSportStoreSystemAuthentication(authOptionProvider.GetSystemAuthOptions());             
            services.AddSwagger(SwaggerPageName);
            services.AddMvc();    
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(_configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseAuthentication();
            app.UseSwaggerUI(SwaggerPageName);
            app.UseMvc();
        }
    }
}