using Microsoft.AspNetCore.Mvc;
using CompanyManager.Domain.Models;
using CompanyManager.Domain.Repositories;
using CompanyManager.UI.Models;
using CompanyManager.UI.Extensions;

namespace CompanyManager.UI.Controllers
{
    [ApiController]
    [Route("api/companies/{companyId:guid}/departments")]
    public sealed class DepartmentsApiController(
        ICompaniesRepository companiesRepository,
        ILogger<DepartmentsApiController> logger) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateDepartmentAsync(Guid companyId, CancellationToken cancellationToken)
        {
            try
            {
                var company = await companiesRepository.GetByIdAsync(companyId, cancellationToken);
                if (company == null)
                {
                    return BadRequest();
                }

                var department = new Department("Департамент", []);
                company.Append(department);
                await companiesRepository.UpdateAsync(company, cancellationToken);
                return CreatedAtAction("GetDepartmentById",
                    new { companyId = company.Id, departmentId = department.Id }, department.MapToViewModel());
            }
            catch (Exception e)
            {
                logger.LogError(e, "Не удалось создать департамент");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{departmentId:guid}")]
        public async Task<ActionResult<DepartmentViewModel>> GetDepartmentByIdAsync(Guid companyId, Guid departmentId,
            CancellationToken cancellationToken)
        {
            try
            {
                var company = await companiesRepository.GetByIdAsync(companyId, cancellationToken);
                if (company == null)
                {
                    return NotFound();
                }

                var department = company.GetDepartmentById(departmentId);
                if (department == null)
                {
                    return NotFound();
                }

                return Ok(department.MapToViewModel());
            }
            catch (Exception e)
            {
                logger.LogError(e, "Возникла ошибка при получении департамента по id {DepartmentId}", departmentId);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{departmentId:guid}")]
        public async Task<ActionResult> DeleteDepartmentAsync(Guid companyId, Guid departmentId,
            CancellationToken cancellationToken)
        {
            try
            {
                var company = await companiesRepository.GetByIdAsync(companyId, cancellationToken);
                if (company == null)
                {
                    return BadRequest();
                }

                var department = company.GetDepartmentById(departmentId);
                if (department == null)
                {
                    return NoContent();
                }

                company.Remove(department);
                await companiesRepository.UpdateAsync(company, cancellationToken);
                return NoContent();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Возникла ошибка при удалении департамента по id {DepartmentId}", departmentId);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPatch("{departmentId:guid}/name")]
        public async Task<ActionResult> RenameDepartmentAsync(Guid companyId, Guid departmentId, string name,
            CancellationToken cancellationToken)
        {
            try
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
                return Ok();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Возникла ошибка при переименовании компании по id {CompanyId}", companyId);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPatch("{departmentId:guid}/position")]
        public async Task<ActionResult> MoveDepartmentAsync(Guid companyId, Guid departmentId, MoveDirection direction,
            CancellationToken cancellationToken)
        {
            try
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
                return Ok();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Возникла ошибка при перемещении департамента c id {DepartmentId}", departmentId);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}