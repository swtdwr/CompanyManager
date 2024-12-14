using Microsoft.AspNetCore.Mvc;
using CompanyManager.Domain.Repositories;
using CompanyManager.UI.Models;
using CompanyManager.UI.Extensions;

namespace CompanyManager.UI.Controllers
{
    public sealed class HomeController(ICompaniesRepository companiesRepository) : Controller
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
    }
}