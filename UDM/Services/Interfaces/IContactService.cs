using System.Data.SqlClient;
using UDM.Models;

namespace UDM.Services.Interfaces
{
	public interface IContactService
	{
			Task<List<Contact>> GetContactsByID(Guid? personID);

			Task InsertContact(Contact contact, Guid? personID, SqlCommand command);

			Task UpdateContact(Contact contact, Guid? personID, SqlCommand command);

			Task DeleteContact(Guid contactID, SqlCommand command);
	}
}
