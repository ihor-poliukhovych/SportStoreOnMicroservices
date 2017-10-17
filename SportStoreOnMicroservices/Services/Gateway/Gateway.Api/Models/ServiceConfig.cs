using System;

namespace Gateway.Api.Models
{
    public class ServiceConfig
    {
        public string Scheme { get; }
        public string Host { get; }
        public string Port { get; }
        
        public ServiceConfig(string scheme, string host, string port)
        {
            Scheme = scheme;
            Host = host;
            Port = port;
        }

        public string GetHost()
        {
            return $"{Host}:{Port}";
        }

        public Uri GetRequestUri(string path, string queryString)
        {
            return new Uri($"{Scheme}://{Host}:{Port}{path}{queryString}", UriKind.Absolute);
        }
    }
}