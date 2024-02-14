using UDM.Models;
using UDM.Models.ViewModels;

namespace UDM.Services.Interfaces
{
	public interface IPersonTransactionService
	{
		Task<Person?> GetPersonAsync(Guid personID);

		Task<ServiceResponseVM> CreatePersonAsync(Person person);

		Task<ServiceResponseVM> UpdatePersonAsync(Person person);

		Task<ServiceResponseVM> DeletePersonAsync(Guid personID, Guid? addressID, Guid? employeeID);

		Task<ServiceResponseVM> DeleteContactAsync(Guid contactID);

		Task<ServiceResponseVM> DeleteAddressAsync(Guid addressID);

		Task<ServiceResponseVM> DeleteEmployeeAsync(Guid employeeID);
	}
}
