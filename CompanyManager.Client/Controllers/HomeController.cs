using Microsoft.AspNetCore.Mvc;
using CompanyManager.Client.Extensions;
using CompanyManager.Client.Models;
using CompanyManager.Domain.Models;
using CompanyManager.Domain.Repositories;

namespace CompanyManager.Client.Controllers
{
    public class HomeController(ICompaniesRepository companiesRepository) : Controller
    {
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var companyViewModels = new List<CompanyViewModel>();

            await foreach (var company in companiesRepository.GetAllAsync(cancellationToken))
            {
                var companyViewModel = company.MapToViewModel();
                companyViewModels.Add(companyViewModel);
            }

            return View(companyViewModels);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartmentAsync(Guid companyId, CancellationToken cancellationToken)
        {
            var company = await companiesRepository.GetByIdAsync(companyId, cancellationToken);
            if (company == null)
            {
                return BadRequest();
            }

            company.Append(new Department("Департамент", []));
            await companiesRepository.UpdateAsync(company, cancellationToken);

            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDepartmentAsync(Guid companyId, Guid departmentId,
            CancellationToken cancellationToken)
        {
            var company = await companiesRepository.GetByIdAsync(companyId, cancellationToken);
            if (company == null)
            {
                return BadRequest();
            }

            var department = company.GetDepartmentById(departmentId);
            if (department == null)
            {
                return BadRequest();
            }

            company.Remove(department);
            await companiesRepository.UpdateAsync(company, cancellationToken);

            return RedirectToAction(nameof(Index));
        }

        [HttpPatch]
        public async Task<IActionResult> RenameDepartmentAsync(Guid companyId, Guid departmentId, string name,
            CancellationToken cancellationToken)
        {
            var company = await companiesRepository.GetByIdAsync(companyId, cancellationToken);
            if (company == null)
            {
                return BadRequest();
            }

            var department = company.GetDepartmentById(departmentId);
            if (department == null)
            {
                return BadRequest();
            }

            department.Name = name;
            await companiesRepository.UpdateAsync(company, cancellationToken);

            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> MoveDepartmentAsync(Guid companyId, Guid departmentId, MoveDirection direction,
            CancellationToken cancellationToken)
        {
            var company = await companiesRepository.GetByIdAsync(companyId, cancellationToken);
            if (company == null)
            {
                return BadRequest();
            }

            var department = company.GetDepartmentById(departmentId);
            if (department == null)
            {
                return BadRequest();
            }

            if (direction == MoveDirection.Down)
            {
                company.MoveDown(department);
            }
            else
            {
                company.MoveUp(department);
            }

            await companiesRepository.UpdateAsync(company, cancellationToken);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> CreateDivisionAsync(Guid companyId, Guid departmentId,
            CancellationToken cancellationToken)
        {
            var company = await companiesRepository.GetByIdAsync(companyId, cancellationToken);
            if (company == null)
            {
                return BadRequest();
            }

            var department = company.GetDepartmentById(departmentId);
            if (department == null)
            {
                return BadRequest();
            }

            department.Append(new Division("Отдел"));
            await companiesRepository.UpdateAsync(company, cancellationToken);

            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDivisionAsync(Guid companyId, Guid departmentId, Guid divisionId,
            CancellationToken cancellationToken)
        {
            var company = await companiesRepository.GetByIdAsync(companyId, cancellationToken);
            if (company == null)
            {
                return BadRequest();
            }

            var department = company.GetDepartmentById(departmentId);
            if (department == null)
            {
                return BadRequest();
            }

            var division = department.GetDivisionById(divisionId);
            if (division == null)
            {
                return BadRequest();
            }

            department.Remove(division);
            await companiesRepository.UpdateAsync(company, cancellationToken);

            return RedirectToAction(nameof(Index));
        }

        [HttpPatch]
        public async Task<IActionResult> RenameDivisionAsync(Guid companyId, Guid departmentId, Guid divisionId,
            string name, CancellationToken cancellationToken)
        {
            var company = await companiesRepository.GetByIdAsync(companyId, cancellationToken);
            if (company == null)
            {
                return BadRequest();
            }

            var department = company.GetDepartmentById(departmentId);
            if (department == null)
            {
                return BadRequest();
            }

            var division = department.GetDivisionById(divisionId);
            if (division == null)
            {
                return BadRequest();
            }

            division.Name = name;
            await companiesRepository.UpdateAsync(company, cancellationToken);

            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> MoveDivisionAsync(Guid companyId, Guid departmentId, Guid divisionId,
            MoveDirection direction, CancellationToken cancellationToken)
        {
            var company = await companiesRepository.GetByIdAsync(companyId, cancellationToken);
            if (company == null)
            {
                return BadRequest();
            }

            var department = company.GetDepartmentById(departmentId);
            if (department == null)
            {
                return BadRequest();
            }

            var division = department.GetDivisionById(divisionId);
            if (division == null)
            {
                return BadRequest();
            }

            if (direction == MoveDirection.Down)
            {
                department.MoveDown(division);
            }
            else
            {
                department.MoveUp(division);
            }

            await companiesRepository.UpdateAsync(company, cancellationToken);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult ExportToXml()
        {
            throw new NotImplementedException();
        }

        public IActionResult ImportFromXml()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompanyAsync(CancellationToken cancellationToken)
        {
            var company = new Company("Компания", []);
            await companiesRepository.AddAsync(company, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCompanyAsync(Guid companyId, CancellationToken cancellationToken)
        {
            await companiesRepository.RemoveByIdAsync(companyId, cancellationToken);
            return RedirectToAction(nameof(Index));
        }

        [HttpPatch]
        public async Task<IActionResult> RenameCompanyAsync(Guid companyId, string name,
            CancellationToken cancellationToken)
        {
            var company = await companiesRepository.GetByIdAsync(companyId, cancellationToken);
            if (company == null)
            {
                return BadRequest();
            }

            company.Name = name;
            await companiesRepository.UpdateAsync(company, cancellationToken);
            return RedirectToAction(nameof(Index));
        }
    }
}