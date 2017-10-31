using Identity.Api.DbContext;
using Identity.Api.Entities;
using Identity.Api.Extentions;
using Identity.Api.Providers;
using Identity.Api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Identity.Api
{    
    public class Startup
    {
        private const string SwaggerPageName = "Identity Api";
        
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
            services.AddTransient<IDatabaseInitializer, DatabaseInitializer>();
            
            services.AddSwagger(SwaggerPageName);
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(_configuration.GetSection("Logging"));
            loggerFactory.AddDebug();
            
            app.UseSwaggerUI(SwaggerPageName);
            app.UseMvc();
        }
    }
}