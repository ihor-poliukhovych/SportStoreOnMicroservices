using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Gateway.Api.Models;
using Gateway.Api.Services;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Http;

namespace Gateway.Api.Middleware
{ 
    public class ProxyMiddleware
    {
        private readonly IJwtTokenService _jwtTokenService;

        //TODO: Move configuration to Consul
        private readonly Dictionary<string, ServiceConfig> _configs = new Dictionary<string, ServiceConfig>
        {
            {
                "catalog", new ServiceConfig("http", "localhost", "5002")
            }
        };

        public ProxyMiddleware(RequestDelegate next, IJwtTokenService jwtTokenService)
        {
            _jwtTokenService = jwtTokenService;
        }

        private static readonly HttpClient HttpClient = new HttpClient(new HttpClientHandler {UseCookies = false});
            
        public async Task Invoke(HttpContext context)
        {
            if (context.User == null)
            {
                context.Response.StatusCode = 401;
                return;
            }
            
            var config = GetServiceConfig(context);   
            
            if (config == null)
            {
                context.Response.StatusCode = (int) HttpStatusCode.BadGateway;
                return;
            }
            
            var request = GetRequest(context, config);
            
            await SendAsync(context, request);
        }

        private ServiceConfig GetServiceConfig(HttpContext context)
        {
            var url = context.Request.GetUri(); 
            
            if (url.Segments.Length < 3)
                return null;
                          
            var serviceIdentifier = url.Segments[2].TrimEnd('/');
            
            _configs.TryGetValue(serviceIdentifier, out var config);
                
            return config;
        }
        
        private HttpRequestMessage GetRequest(HttpContext context, ServiceConfig config)
        {   
            var request = new HttpRequestMessage
            {
                Method = new HttpMethod(context.Request.Method)
            };
                
            if (!StringComparer.OrdinalIgnoreCase.Equals(context.Request.Method, "GET") &&
                !StringComparer.OrdinalIgnoreCase.Equals(context.Request.Method, "HEAD") &&
                !StringComparer.OrdinalIgnoreCase.Equals(context.Request.Method, "DELETE") &&
                !StringComparer.OrdinalIgnoreCase.Equals(context.Request.Method, "TRACE"))
            {
                request.Content = new StreamContent(context.Request.Body);
            }
            
            foreach (var header in context.Request.Headers)
            {
                if (request.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray()))
                    continue;

                request.Content.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
            }

            request.RequestUri = config.GetRequestUri(context.Request.Path, context.Request.QueryString.ToString());
            request.Headers.Host = config.GetHost();
            
            var systemToken = _jwtTokenService.GenerateSystemToken(context.User.Claims);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", systemToken);

            return request;
        }

        private async Task SendAsync(HttpContext context, HttpRequestMessage request)
        {
            using (var responseMessage = await HttpClient.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead, context.RequestAborted))
            {
                if (responseMessage.StatusCode == HttpStatusCode.ServiceUnavailable)
                    responseMessage.EnsureSuccessStatusCode();

                context.Response.StatusCode = (int) responseMessage.StatusCode;

                foreach (var header in responseMessage.Headers)
                    context.Response.Headers.TryAdd(header.Key, header.Value.ToArray());

                foreach (var header in responseMessage.Content.Headers)
                    context.Response.Headers.TryAdd(header.Key, header.Value.ToArray());

                context.Response.Headers.Remove("transfer-encoding");

                await responseMessage.Content.CopyToAsync(context.Response.Body);
            }
        }
    }
}