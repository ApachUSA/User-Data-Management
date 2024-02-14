using System.Data.SqlClient;
using UDM.Models;

namespace UDM.Services.Interfaces
{
	public interface IAddressService
	{
        Task<Address> GetAddressByID(Guid? addressID);

        Task<Guid> InsertAddress(Address address, SqlCommand command);

		Task UpdateAddress(Address address, Guid? personID, SqlCommand command);

		Task DeleteAddress(Guid? addressID, SqlCommand command);
	}
}
