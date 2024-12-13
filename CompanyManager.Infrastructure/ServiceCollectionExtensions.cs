﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CompanyManager.Infrastructure.EntityFramework;

namespace CompanyManager.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkDataAccess(configuration);
            return services;
        }
    }
}