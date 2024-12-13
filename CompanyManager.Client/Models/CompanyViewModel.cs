namespace CompanyManager.Client.Models
{
    public record CompanyViewModel(Guid Id, string Name, IEnumerable<DepartmentViewModel> Departments);
}