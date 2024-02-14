using UDM.Models;

namespace UDM.Services.Interfaces
{
	public interface ISelectDataService
	{
		Task<List<City>> GetCitiesAsync();

		Task<List<Company>> GetCompaniesAsync();

		Task<List<Department>> GetDepartmentsAsync(Guid companyID);

		Task<List<Position>> GetPositionAsync(Guid departID);
	}
}
