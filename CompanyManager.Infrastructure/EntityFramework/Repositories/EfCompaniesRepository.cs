using CompanyManager.Domain.Models;
using CompanyManager.Domain.Repositories;
using CompanyManager.Infrastructure.EntityFramework.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Infrastructure.EntityFramework.Repositories
{
    public sealed class EfCompaniesRepository(CompaniesDbContext context) : ICompaniesRepository
    {
        public async Task AddAsync(Company company, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(company);
            await context.Companies.AddAsync(company.MapToDto(), cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Company company, CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(company);
            context.Companies.Update(company.MapToDto());
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task RemoveByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var companies = context.Companies;
            var company = await companies.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
            if (company == null)
            {
                throw new InvalidOperationException();
            }
            companies.Remove(company);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Company?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var dto = await context.Companies.Include(x => x.Departments)
                                             .ThenInclude(x => x.Divisions)
                                             .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
            return dto?.MapToEntity();
        }

        public IAsyncEnumerable<Company> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return context.Companies.Include(x => x.Departments)
                                    .ThenInclude(x => x.Divisions)
                                    .Select(x => x.MapToEntity())
                                    .AsAsyncEnumerable();
        }
    }
}