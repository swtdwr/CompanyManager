using CompanyManager.Domain.Repositories;
using CompanyManager.Infrastructure.EntityFramework.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CompanyManager.Infrastructure.EntityFramework
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEntityFrameworkDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CompaniesDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("CompaniesDb")));
            services.AddScoped<ICompaniesRepository, EfCompaniesRepository>();
            
            return services;
        }
    }
}