namespace CompanyManager.Infrastructure.EntityFramework.DTOs
{
    public class CompanyDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<DepartmentDto> Departments { get; set; } = [];
    }
}