using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using UDM.Models;
using UDM.Models.ViewModels;
using UDM.Services.Interfaces;

namespace UDM.Services.Implementations
{
    public class PersonService : IPersonService
    {
        private readonly IConfiguration _config;

        public PersonService(IConfiguration config)
        {
            _config = config;
        }

		/// <summary>
		/// Retrieves a person by their unique identifier asynchronously.
		/// </summary>
		/// <param name="personID">The unique identifier of the person.</param>
		/// <returns>An instance of the Person class representing the retrieved person.</returns>
		public async Task<Person> GetPersonByID(Guid? personID)
        {
            DataTable dt = new();

            using SqlConnection con = new(_config.GetConnectionString("DefaultConnection"));
            await con.OpenAsync();

            using SqlCommand cmd = new($"select * from Persons where Person_id = '{personID}'", con);
            using SqlDataAdapter adapter = new(cmd);

            adapter.Fill(dt);

            DataRow row = dt.Rows[0];

            var person = new Person
            {
                ID = row.Field<Guid>("Person_id"),
                Surname = row.Field<string>("Surname"),
                Name = row.Field<string>("Name_"),
                Patronymic = row.Field<string>("Patronymic"),
                Birth_Date = row.Field<DateTime?>("Birth_Date"),
                AddressID = row.Field<Guid?>("Address_id"),
                EmployeeID = row.Field<Guid?>("Employee_id"),
            };

            return person;
        }

		/// <summary>
		/// Inserts a new person into the database asynchronously.
		/// </summary>
		/// <param name="person">The person object to be inserted.</param>
		/// <param name="command">The SqlCommand object associated with an open SqlConnection.</param>
		/// <returns>The unique identifier of the newly inserted person.</returns>
		public async Task<Guid> InsertPerson(Person person, SqlCommand command)
        {
            command.Parameters.Clear();

            command.CommandText = "InsertPerson";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@Surname", SqlDbType.NVarChar, 30).Value = person.Surname;
            command.Parameters.Add("@Name", SqlDbType.NVarChar, 20).Value = person.Name;
            command.Parameters.Add("@Patronymic", SqlDbType.NVarChar, 30).Value = person.Patronymic;
            command.Parameters.Add("@BDate", SqlDbType.Date).Value = person.Birth_Date;
            command.Parameters.Add("@AddressID", SqlDbType.UniqueIdentifier).Value = person.AddressID;
            command.Parameters.Add("@EmployeesID", SqlDbType.UniqueIdentifier).Value = person.EmployeeID;

            SqlParameter outputParameter = command.Parameters.Add("@PersonID", SqlDbType.UniqueIdentifier);
            outputParameter.Direction = ParameterDirection.Output;

            await command.ExecuteNonQueryAsync();

            return (Guid)outputParameter.Value;
        }

		/// <summary>
		/// Updates an existing person asynchronously.
		/// </summary>
		/// <param name="person">The updated person object.</param>
		/// <param name="command">The SqlCommand object associated with an open SqlConnection.</param>
		public async Task UpdatePerson(Person person, SqlCommand command)
        {
			command.Parameters.Clear();

			command.CommandText = "UpdatePerson";
			command.CommandType = CommandType.StoredProcedure;

			command.Parameters.Add("@PersonID", SqlDbType.UniqueIdentifier).Value = person.ID;
			command.Parameters.Add("@Surname", SqlDbType.NVarChar, 30).Value = person.Surname;
			command.Parameters.Add("@Name", SqlDbType.NVarChar, 20).Value = person.Name;
			command.Parameters.Add("@Patronymic", SqlDbType.NVarChar, 30).Value = person.Patronymic;
			command.Parameters.Add("@BDate", SqlDbType.Date).Value = person.Birth_Date;
			command.Parameters.Add("@AddressID", SqlDbType.UniqueIdentifier).Value = person.AddressID;
			command.Parameters.Add("@EmployeeID", SqlDbType.UniqueIdentifier).Value = person.EmployeeID;

			await command.ExecuteNonQueryAsync();
		}

		/// <summary>
		/// Deletes a person from the database asynchronously.
		/// </summary>
		/// <param name="personID">The unique identifier of the person to be deleted.</param>
		/// <param name="command">The SqlCommand object associated with an open SqlConnection.</param>
		public async Task DeletePerson(Guid? personID, SqlCommand command)
		{
			command.Parameters.Clear();

			command.CommandText = $"delete from Persons where Person_id = '{personID}'";

			await command.ExecuteNonQueryAsync();
		}
	}
}
