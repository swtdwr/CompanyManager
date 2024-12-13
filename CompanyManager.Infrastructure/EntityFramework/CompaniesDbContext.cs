using CompanyManager.Infrastructure.EntityFramework.DTOs;
using Microsoft.EntityFrameworkCore;

namespace CompanyManager.Infrastructure.EntityFramework.Repositories
{
    public class CompaniesDbContext : DbContext
    {
        public CompaniesDbContext(DbContextOptions<CompaniesDbContext> options) : base(options)
        {
        }
        
        public DbSet<CompanyDto> Companies { get; set; }
        
        public DbSet<DepartmentDto> Departments { get; set; }
        
        public DbSet<DivisionDto> Divisions { get; set; }
    }
}