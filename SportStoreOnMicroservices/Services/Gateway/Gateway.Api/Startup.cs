using Gateway.Api.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Gateway.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ProxyMiddleware>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMiddleware<ProxyMiddleware>();
        }
    }
}