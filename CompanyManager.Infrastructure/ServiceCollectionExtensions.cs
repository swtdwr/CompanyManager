using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CompanyManager.Infrastructure.EntityFramework;
using CompanyManager.Infrastructure.Services.CompaniesSerializer;
using CompanyManager.Infrastructure.Services.CompaniesSerializer.Abstractions;

namespace CompanyManager.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkDataAccess(configuration);
            services.AddKeyedSingleton<ICompaniesSerializer, XmlCompaniesSerializer>(SerializationType.Xml);
            return services;
        }
    }
}