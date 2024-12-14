namespace CompanyManager.Infrastructure.EntityFramework.DTOs
{
    internal class DepartmentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CompanyId { get; set; }
        public CompanyDto Company { get; set; }
        public ICollection<DivisionDto> Divisions { get; set; } = [];
    }
}