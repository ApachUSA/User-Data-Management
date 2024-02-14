using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UDM.Models;
using UDM.Services.Interfaces;

namespace UDM.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISelectDataService _selectDataService;

		public HomeController(ISelectDataService selectDataService)
		{
			_selectDataService = selectDataService;
		}

		public async Task<IActionResult> Index()
        {
            var company = await _selectDataService.GetCompaniesAsync();

			return View(company.Where(x => x.Company_Name == "”крпошта").FirstOrDefault());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
