using CompanyManager.Domain.Models;
using CompanyManager.Domain.Repositories;
using CompanyManager.UI.Extensions;
using CompanyManager.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanyManager.UI.Controllers
{
    [ApiController]
    [Route("api/companies/{companyId:guid}/departments/{departmentId:guid}")]
    public sealed class DivisionsApiController(
        ICompaniesRepository companiesRepository,
        ILogger<DepartmentsApiController> logger) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateDivisionAsync(Guid companyId, Guid departmentId,
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

                var division = new Division("Отделение");
                department.Append(division);
                await companiesRepository.UpdateAsync(company, cancellationToken);

                return CreatedAtAction("GetDivisionById",
                    new
                    {
                        companyId = company.Id,
                        departmentId = department.Id,
                        divisionId = division.Id
                    },
                    division.MapToViewModel());
            }
            catch (Exception e)
            {
                logger.LogError(e, "Не удалось создать отделение");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{divisionId:guid}")]
        public async Task<ActionResult<DepartmentViewModel>> GetDivisionByIdAsync(Guid companyId, Guid departmentId,
            Guid divisionId,
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

                var division = department.GetDivisionById(divisionId);
                if (division == null)
                {
                    return NotFound();
                }

                return Ok(division.MapToViewModel());
            }
            catch (Exception e)
            {
                logger.LogError(e, "Возникла ошибка при получении отделения по id {DivisionId}", divisionId);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{divisionId:guid}")]
        public async Task<ActionResult> DeleteDivisionAsync(Guid companyId, Guid departmentId, Guid divisionId,
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

                var division = department.GetDivisionById(divisionId);
                if (division == null)
                {
                    return NoContent();
                }

                department.Remove(division);
                await companiesRepository.UpdateAsync(company, cancellationToken);
                return NoContent();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Возникла ошибка при удалении отделения по id {DivisionId}", divisionId);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPatch("{divisionId:guid}/name")]
        public async Task<ActionResult> RenameDivisionAsync(Guid companyId, Guid departmentId, Guid divisionId,
            string name,
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

                var division = department.GetDivisionById(divisionId);
                if (division == null)
                {
                    return BadRequest();
                }

                division.Name = name;
                await companiesRepository.UpdateAsync(company, cancellationToken);
                return Ok();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Возникла ошибка при переименовании отделения по id {DivisionId}", divisionId);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPatch("{divisionId:guid}/position")]
        public async Task<ActionResult> MoveDivisionAsync(Guid companyId, Guid departmentId, Guid divisionId,
            MoveDirection direction,
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
                return Ok();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Возникла ошибка при перемещении отделения c id {DivisionId}", divisionId);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}