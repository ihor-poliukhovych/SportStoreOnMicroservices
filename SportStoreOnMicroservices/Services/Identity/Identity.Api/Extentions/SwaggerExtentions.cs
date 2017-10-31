using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Identity.Api.Extentions
{
    public static class SwaggerExtentions
    {
        public static void AddSwagger(this IServiceCollection services, string name, int version = 1)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc($"v{version}", new Info {Title = name, Version = $"v{version}"});
            });
        }
        
        public static void UseSwaggerUI(this IApplicationBuilder app, string name, int version = 1)
        {
            app.UseSwagger().UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/v{version}/swagger.json", $"{name} v{version}");
            });
        }
    }
}