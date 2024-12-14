using System.Text;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc;
using CompanyManager.Domain.Models;
using CompanyManager.Domain.Repositories;
using CompanyManager.Infrastructure.Services.CompaniesSerializer;
using CompanyManager.Infrastructure.Services.CompaniesSerializer.Abstractions;
using CompanyManager.UI.Models;
using CompanyManager.UI.Extensions;

namespace CompanyManager.UI.Controllers
{
    [ApiController]
    [Route("api/companies")]
    public sealed class CompaniesApiController(
        ICompaniesRepository companiesRepository,
        [FromKeyedServices(SerializationType.Xml)] ICompaniesSerializer companiesSerializer,
        ILogger<CompaniesApiController> logger) : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateCompanyAsync(CancellationToken cancellationToken)
        {
            try
            {
                var company = new Company("Компания", []);
                await companiesRepository.AddAsync(company, cancellationToken);
                return CreatedAtAction("GetCompanyById", new { companyId = company.Id }, company.MapToViewModel());
            }
            catch (Exception e)
            {
                logger.LogError(e, "Не удалось создать компанию");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{companyId:guid}")]
        public async Task<ActionResult<CompanyViewModel>> GetCompanyByIdAsync(Guid companyId,
            CancellationToken cancellationToken)
        {
            try
            {
                var company = await companiesRepository.GetByIdAsync(companyId, cancellationToken);
                if (company == null)
                {
                    return NotFound();
                }

                return Ok(company.MapToViewModel());
            }
            catch (Exception e)
            {
                logger.LogError(e, "Возникла ошибка при получении компании по id {CompanyId}", companyId);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{companyId:guid}")]
        public async Task<ActionResult> DeleteCompanyAsync(Guid companyId, CancellationToken cancellationToken)
        {
            try
            {
                await companiesRepository.RemoveByIdAsync(companyId, cancellationToken);
                return NoContent();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Возникла ошибка при удалении компании по id {CompanyId}", companyId);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPatch("{companyId:guid}/name")]
        public async Task<ActionResult> RenameCompanyAsync(Guid companyId, string name,
            CancellationToken cancellationToken)
        {
            try
            {
                var company = await companiesRepository.GetByIdAsync(companyId, cancellationToken);
                if (company == null)
                {
                    return BadRequest();
                }

                company.Name = name;
                await companiesRepository.UpdateAsync(company, cancellationToken);
                return Ok();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Возникла ошибка при переименовании компании по id {CompanyId}", companyId);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("xml")]
        public async Task<ActionResult> ExportToXmlAsync(CancellationToken cancellationToken)
        {
            try
            {
                var companies = new List<Company>();
                await foreach (var company in companiesRepository.GetAllAsync(cancellationToken))
                {
                    companies.Add(company);
                }
                
                var serializedCompanies = await companiesSerializer.SerializeAsync(companies, cancellationToken);
                return File(serializedCompanies, "application/xml", "companies.xml");
            }
            catch (Exception e)
            {
                logger.LogError(e, "Возникла ошибка при экспорте компаний");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
        
        [HttpPost("xml")]
        public async Task<ActionResult> ImportFromXmlAsync(IFormFile file, CancellationToken cancellationToken)
        {
            try
            {
                var fileContent = await file.ReadContentAsBytesAsync(cancellationToken);
                var companies = await companiesSerializer.DeserializeAsync(fileContent, cancellationToken);
                await companiesRepository.AddManyAsync(companies, cancellationToken);
                
                return Ok();
            }
            catch (Exception e)
            {
                logger.LogError(e, "Возникла ошибка при импорте компаний");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}