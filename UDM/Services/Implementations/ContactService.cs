using System.Data;
using System.Data.SqlClient;
using System.Net;
using UDM.Models;
using UDM.Models.ViewModels;
using UDM.Services.Interfaces;

namespace UDM.Services.Implementations
{
	public class ContactService : IContactService
	{
		private readonly IConfiguration _config;

		public ContactService(IConfiguration config)
		{
			_config = config;
		}

		/// <summary>
		/// Retrieves contacts associated with a person by their unique identifier asynchronously.
		/// </summary>
		/// <param name="personID">The unique identifier of the person.</param>
		/// <returns>A list of Contact objects associated with the person.</returns>
		public async Task<List<Contact>> GetContactsByID(Guid? personID)
		{
			DataTable dt = new();

			using SqlConnection con = new(_config.GetConnectionString("DefaultConnection"));
			await con.OpenAsync();

			using SqlCommand cmd = new($"select * from Contacts where Person_id = '{personID}'", con);
			using SqlDataAdapter adapter = new(cmd);

			adapter.Fill(dt);

			List<Contact> contacts = [];
			foreach (DataRow dr in dt.Rows)
			{
				contacts.Add(new Contact
				{
					ID = dr.Field<Guid>("Contacts_id"),
					Phone_Number = dr.Field<string>("Phone_number"),
					PersonID = dr.Field<Guid>("Person_id"),
				});
			}


			return contacts;
		}

		/// <summary>
		/// Inserts a new contact into the database asynchronously.
		/// </summary>
		/// <param name="contact">The contact object to be inserted.</param>
		/// <param name="personID">The unique identifier of the person associated with the contact.</param>
		/// <param name="command">The SqlCommand object associated with an open SqlConnection.</param>
		public async Task InsertContact(Contact contact, Guid? personID, SqlCommand command)
		{
			command.Parameters.Clear();

			command.CommandText = "InsertContacts";
			command.CommandType = CommandType.StoredProcedure;

			command.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 30).Value = contact.Phone_Number;
			command.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = personID;

			await command.ExecuteNonQueryAsync();
		}

		/// <summary>
		/// Updates an existing contact asynchronously.
		/// </summary>
		/// <param name="contact">The updated contact object.</param>
		/// <param name="personID">The unique identifier of the person associated with the contact.</param>
		/// <param name="command">The SqlCommand object associated with an open SqlConnection.</param>
		public async Task UpdateContact(Contact contact, Guid? personID, SqlCommand command)
		{
			command.Parameters.Clear();

			command.CommandText = "UpdateContacts";
			command.CommandType = CommandType.StoredProcedure;

			command.Parameters.Add("@ContactID", SqlDbType.UniqueIdentifier).Value = contact.ID;
			command.Parameters.Add("@PhoneNumber", SqlDbType.NVarChar, 30).Value = contact.Phone_Number;
			command.Parameters.Add("@PersonID", SqlDbType.UniqueIdentifier).Value = personID;

			await command.ExecuteNonQueryAsync();
		}

		/// <summary>
		/// Deletes a contact from the database asynchronously.
		/// </summary>
		/// <param name="contactID">The unique identifier of the contact to be deleted.</param>
		/// <param name="command">The SqlCommand object associated with an open SqlConnection.</param>
		public async Task DeleteContact(Guid contactID, SqlCommand command)
		{
			command.Parameters.Clear();

			command.CommandText = $"delete from Contacts where Contacts_id = '{contactID}'";

			await command.ExecuteNonQueryAsync();
		}
	}
}
