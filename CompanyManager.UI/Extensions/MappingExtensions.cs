using CompanyManager.Domain.Models;
using CompanyManager.UI.Models;

namespace CompanyManager.UI.Extensions
{
    public static class MappingExtensions
    {
        public static CompanyViewModel MapToViewModel(this Company company)
        {
            var departmentsViewModels = company.GetDepartments().Select(MapToViewModel);
            return new CompanyViewModel(company.Id, company.Name, departmentsViewModels);
        }

        public static DepartmentViewModel MapToViewModel(this Department department)
        {
            var divisionsViewModels = department.GetDivisions().Select(MapToViewModel);
            return new DepartmentViewModel(department.Id, department.Name, divisionsViewModels);
        }

        public static DivisionViewModel MapToViewModel(this Division division)
        {
            return new DivisionViewModel(division.Id, division.Name);
        }
    }
}