using Microsoft.AspNetCore.Mvc;
using UDM.Models.ViewModels;
using UDM.Services.Implementations;
using UDM.Services.Interfaces;

namespace UDM.Controllers
{
	public class StaffController : Controller
	{
		private readonly IStaffService _staffService;
		private readonly ISelectDataService _selectDataService;

        public StaffController(ISelectDataService selectDataService, IStaffService staffService)
        {
            _selectDataService = selectDataService;
            _staffService = staffService;
        }

        [HttpGet]
		public IActionResult Index() => View();

		[HttpPost]
		public async Task<IActionResult> GetPersonTable(PersonsVM personsVM)
		{
			var persons = await _staffService.GetPersonsWithFilters(personsVM);
			return PartialView("PersonTablePartial", persons);
		}

		[HttpGet]
		public IActionResult SalaryIndex() => View();

		[HttpPost]
		public async Task<IActionResult> GetSalaryTable(PersonsVM personsVM)
		{
			var persons = await _staffService.GetSalaryWithFilters(personsVM);
			return PartialView("SalaryTablePartial", persons);
		}

		[HttpPost]
		public async Task<IActionResult> DownloadSalaryTable(List<TableSalaryVM> tableSalaryVM)
		{
			return File(await _staffService.SaveSalaryTable(tableSalaryVM), "text/txt", "SalaryReport.txt");
		}

		[HttpGet]
		public async Task<IActionResult> GetCitiesData()
		{
			return Ok(await _selectDataService.GetCitiesAsync());
		}

		[HttpGet]
		public async Task<IActionResult> GetCompaniesData()
		{
			return Ok(await _selectDataService.GetCompaniesAsync());
		}

		[HttpGet]
		public async Task<IActionResult> GetDepartmentData(Guid companyID)
		{
			return Ok(await _selectDataService.GetDepartmentsAsync(companyID));
		}

		[HttpGet]
		public async Task<IActionResult> GetPositionsData(Guid departID)
		{
			return Ok(await _selectDataService.GetPositionAsync(departID));
		}
	}
}
