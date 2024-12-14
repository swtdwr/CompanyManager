namespace CompanyManager.UI.Models
{
    public record CompanyViewModel(Guid Id, string Name, IEnumerable<DepartmentViewModel> Departments);
}