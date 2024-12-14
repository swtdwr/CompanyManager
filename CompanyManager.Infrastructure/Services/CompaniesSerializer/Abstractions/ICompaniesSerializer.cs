using CompanyManager.Domain.Models;

namespace CompanyManager.Infrastructure.Services.CompaniesSerializer.Abstractions
{
    public interface ICompaniesSerializer
    {
        Task<byte[]> SerializeAsync(IEnumerable<Company> companies, CancellationToken cancellationToken = default);
    
        Task<IEnumerable<Company>> DeserializeAsync(byte[] serializedCompanies, CancellationToken cancellationToken = default);
    }
}