namespace CompanyManager.Infrastructure.EntityFramework.DTOs
{
    public class DivisionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid DepartmentId { get; set; }
        public DepartmentDto Department { get; set; }
    }
}