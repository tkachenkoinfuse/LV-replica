using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ServiceWithHangfire
{
    internal class ServiceResolver
    {
        private IServiceCollection services;
        private IConfiguration configuration;
        private static IServiceProvider _serviceProvider;

        public ServiceResolver(IServiceCollection services, IConfiguration configuration)
        {
            this.services = services;
            this.configuration = configuration;
        }
        public IServiceProvider GetServiceProvider()
        {
            return _serviceProvider;
        }
    }
}