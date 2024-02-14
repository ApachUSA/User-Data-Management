using System.Data.SqlClient;
using UDM.Models;
using UDM.Models.ViewModels;

namespace UDM.Services.Interfaces
{
	public interface IPersonService
	{
		Task<Person> GetPersonByID(Guid? personID);

        Task<Guid> InsertPerson(Person person, SqlCommand command);

		Task UpdatePerson(Person person, SqlCommand command);

		Task DeletePerson(Guid? personID, SqlCommand command);

	}
}
