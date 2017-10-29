using Catalog.Api.Extentions;
using Catalog.Api.Providers;
using Identity.Api.Providers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            var authOptionProvider = serviceProvider.GetService<IAuthOptionProvider>();
     
            services.AddSportStoreSystemAuthentication(authOptionProvider.GetSystemAuthOptions());
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}