using CompanyManager.Infrastructure.EntityFramework.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Infrastructure.EntityFramework
{
    internal class CompaniesDbContext : DbContext
    {
        public CompaniesDbContext(DbContextOptions<CompaniesDbContext> options) : base(options)
        {
        }
        
        public DbSet<CompanyDto> Companies { get; init; }
        
        public DbSet<DepartmentDto> Departments { get; init; }
        
        public DbSet<DivisionDto> Divisions { get; init; }
    }
}