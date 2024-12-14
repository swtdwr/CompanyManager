using System.Xml.Serialization;
using CompanyManager.Domain.Models;
using CompanyManager.Infrastructure.Services.CompaniesSerializer.Abstractions;

namespace CompanyManager.Infrastructure.Services.CompaniesSerializer
{
    public class CompanyDto
    {
        [XmlElement("Id")]
        public Guid Id { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlArray("Departments")]
        [XmlArrayItem("Department")]
        public List<DepartmentDto> Departments { get; set; }
    }

    public class DepartmentDto
    {
        [XmlElement("Id")]
        public Guid Id { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlArray("Divisions")]
        [XmlArrayItem("Division")]
        public List<DivisionDto> Divisions { get; set; }
    }

    public class DivisionDto
    {
        [XmlElement("Id")]
        public Guid Id { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }
    }
    
    internal sealed class XmlCompaniesSerializer : ICompaniesSerializer
    {
        private readonly XmlSerializer _xmlSerializer = new(typeof(CompanyDto[]));

        public Task<byte[]> SerializeAsync(IEnumerable<Company> companies, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
        
            var companiesDto = companies.Select(ConvertToXmlDto).ToArray();
            using var memoryStream = new MemoryStream();
            _xmlSerializer.Serialize(memoryStream, companiesDto);
            return Task.FromResult(memoryStream.ToArray());
        }

        public Task<IEnumerable<Company>> DeserializeAsync(byte[] serializedCompanies, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            using var memoryStream = new MemoryStream(serializedCompanies);
            var deserializedCompanies = _xmlSerializer.Deserialize(memoryStream) as CompanyDto[];
            return Task.FromResult(deserializedCompanies?.Select(ConvertToEntity) ?? Array.Empty<Company>());
        }

        private static CompanyDto ConvertToXmlDto(Company company)
        {
            return new CompanyDto
            {
                Id = company.Id,
                Name = company.Name,
                Departments = company.GetDepartments().Select(ConvertToXmlDto).ToList()
            };
        }

        private static DepartmentDto ConvertToXmlDto(Department department)
        {
            return new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name,
                Divisions = department.GetDivisions().Select(ConvertToXmlDto).ToList()
            };
        }

        private static DivisionDto ConvertToXmlDto(Division division)
        {
            return new DivisionDto
            {
                Id = division.Id,
                Name = division.Name
            };
        }

        private static Company ConvertToEntity(CompanyDto companyDto)
        {
            var departments = companyDto.Departments.Select(ConvertToEntity);
            return new Company(companyDto.Id, companyDto.Name, departments);
        }

        private static Department ConvertToEntity(DepartmentDto departmentDto)
        {
            var divisions = departmentDto.Divisions.Select(ConvertToEntity);
            return new Department(departmentDto.Id, departmentDto.Name, divisions);
        }

        private static Division ConvertToEntity(DivisionDto divisionDto)
        {
            return new Division(divisionDto.Id, divisionDto.Name);
        }
    }
}