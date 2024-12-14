using CompanyManager.Domain.Models;
using CompanyManager.Infrastructure.EntityFramework.DTOs;

namespace CompanyManager.Infrastructure.EntityFramework.Extensions
{
    internal static class MappingExtensions
    {
        public static Company MapToEntity(this CompanyDto dto)
        {
            var departments = dto.Departments.Select(MapToEntity);
            return new Company(dto.Id, dto.Name, departments);
        }
        
        public static CompanyDto MapToDto(this Company company)
        {
            return new CompanyDto
            {
                Id = company.Id,
                Name = company.Name,
                Departments = company.GetDepartments().Select(x => MapToDto(company, x)).ToArray()
            };
        }
        
        private static Department MapToEntity(this DepartmentDto dto)
        {
            var divisions = dto.Divisions.Select(MapToEntity);
            return new Department(dto.Id, dto.Name, divisions);
        }
        
        private static DepartmentDto MapToDto(Company company, Department department)
        {
            return new DepartmentDto
            {
                Id = department.Id,
                Name = department.Name,
                CompanyId = company.Id,
                Divisions = department.GetDivisions().Select(x => MapToDto(department, x)).ToArray()
            };
        }
        
        private static Division MapToEntity(DivisionDto dto)
        {
            return new Division(dto.Id, dto.Name);
        }
        
        private static DivisionDto MapToDto(Department department, Division division)
        {
            return new DivisionDto
            {
                Id = division.Id,
                Name = division.Name,
                DepartmentId = department.Id,
            };
        }
    }
}