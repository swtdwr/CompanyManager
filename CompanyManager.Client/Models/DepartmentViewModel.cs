namespace CompanyManager.Client.Models
{
    public record DepartmentViewModel(Guid Id, string Name, IEnumerable<DivisionViewModel> Divisions);
}