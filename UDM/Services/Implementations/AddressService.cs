using System.Data.SqlClient;
using System.Data;
using UDM.Models;
using UDM.Models.ViewModels;
using UDM.Services.Interfaces;
using System;

namespace UDM.Services.Implementations
{
	public class AddressService : IAddressService
	{
        private readonly IConfiguration _config;

        public AddressService(IConfiguration config)
        {
            _config = config;
        }

		/// <summary>
		/// Retrieves an address by its unique identifier asynchronously.
		/// </summary>
		/// <param name="addressID">The unique identifier of the address.</param>
		/// <returns>An instance of the Address class representing the retrieved address.</returns>
		public async Task<Address> GetAddressByID(Guid? addressID)
        {
            DataTable dt = new();

            using SqlConnection con = new(_config.GetConnectionString("DefaultConnection"));
            await con.OpenAsync();

            using SqlCommand cmd = new($"select * from Addresses where Address_id = '{addressID}'", con);
            using SqlDataAdapter adapter = new(cmd);

            adapter.Fill(dt);

            DataRow row = dt.Rows[0];

            var address = new Address
            {
                ID = row.Field<Guid>("Address_id"),
                Location = row.Field<string>("Location_"),
                CityID = row.Field<Guid?>("City_id"),
            };

            return address;
        }

		/// <summary>
		/// Inserts a new address into the database asynchronously.
		/// </summary>
		/// <param name="address">The address object to be inserted.</param>
		/// <param name="command">The SqlCommand object associated with an open SqlConnection.</param>
		/// <returns>The unique identifier of the newly inserted address.</returns>
		public async Task<Guid> InsertAddress(Address address, SqlCommand command)
		{
            command.Parameters.Clear();

            command.CommandText = "InsertAddress";
			command.CommandType = CommandType.StoredProcedure;

			command.Parameters.Add("@Location", SqlDbType.VarChar, 100).Value = address.Location;
			command.Parameters.Add("@CityID", SqlDbType.UniqueIdentifier, 20).Value = address.CityID;

			SqlParameter outputParameter = command.Parameters.Add("@AddressID", SqlDbType.UniqueIdentifier);
			outputParameter.Direction = ParameterDirection.Output;

			await command.ExecuteNonQueryAsync();

			return (Guid)outputParameter.Value;
		}

		/// <summary>
		/// Updates an existing address asynchronously.
		/// </summary>
		/// <param name="address">The updated address object.</param>
		/// <param name="personID">The unique identifier of the person associated with the address.</param>
		/// <param name="command">The SqlCommand object associated with an open SqlConnection.</param>
		public async Task UpdateAddress(Address address, Guid? personID, SqlCommand command)
		{
			command.Parameters.Clear();

			command.CommandText = "UpdateAddress";
			command.CommandType = CommandType.StoredProcedure;

			command.Parameters.Add("@AddressID", SqlDbType.UniqueIdentifier).Value = address.ID;
			command.Parameters.Add("@Location", SqlDbType.VarChar, 100).Value = address.Location;
			command.Parameters.Add("@CityID", SqlDbType.UniqueIdentifier, 20).Value = address.CityID;
			command.Parameters.Add("@PersonID", SqlDbType.UniqueIdentifier).Value = personID;

			await command.ExecuteNonQueryAsync();
		}

		/// <summary>
		/// Deletes an address from the database asynchronously.
		/// </summary>
		/// <param name="addressID">The unique identifier of the address to be deleted.</param>
		/// <param name="command">The SqlCommand object associated with an open SqlConnection.</param>
		public async Task DeleteAddress(Guid? addressID, SqlCommand command)
		{
			command.Parameters.Clear();

			command.CommandText = $"delete from Addresses where Address_id = '{addressID}'";

			await command.ExecuteNonQueryAsync();
		}
	}
}
