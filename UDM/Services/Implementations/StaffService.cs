using System.Data.SqlClient;
using System.Data;
using UDM.Models.ViewModels;
using UDM.Services.Interfaces;
using UDM.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;

namespace UDM.Services.Implementations
{
    public class StaffService : IStaffService
    {
        private readonly IConfiguration _config;

        public StaffService(IConfiguration config)
        {
            _config = config;
        }

		/// <summary>
		/// Retrieves a list of persons with applied filters asynchronously.
		/// </summary>
		/// <param name="personsVM">The filter parameters.</param>
		/// <returns>A list of persons matching the specified filters.</returns>
		public async Task<List<TableStaffVM>> GetPersonsWithFilters(PersonsVM personsVM)
        {
            DataTable dt = new();

            using SqlConnection con = new(_config.GetConnectionString("DefaultConnection"));
            await con.OpenAsync();

            SqlCommand command = con.CreateCommand();

            command.CommandText = "GetPersonsWithFilters";
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add("@cityID", SqlDbType.UniqueIdentifier).Value = personsVM.City;
            command.Parameters.Add("@positionID", SqlDbType.UniqueIdentifier).Value = personsVM.Position;
            command.Parameters.Add("@departID", SqlDbType.UniqueIdentifier).Value = personsVM.Department;
            command.Parameters.Add("@companyID", SqlDbType.UniqueIdentifier).Value = personsVM.Company;
            command.Parameters.Add("@pib", SqlDbType.NVarChar, 100).Value = personsVM.FullName;

            using SqlDataAdapter adapter = new(command);

            adapter.Fill(dt);

            List<TableStaffVM> persons = [];
            foreach (DataRow dr in dt.Rows)
            {
                persons.Add(new TableStaffVM
				{
                    Id = dr.Field<Guid>("Person_id"),
                    FullName = dr.Field<string>("Surname") + " " + dr.Field<string>("Name_") + " " + dr.Field<string>("Patronymic"),
                    City = dr.Field<string>("City_name"),
                    Position = dr.Field<string>("Position_name"),
                    Department = dr.Field<string>("Department_name"),
                    Company = dr.Field<string>("Company_name")
                });
            }
            return persons.OrderBy(x => x.FullName).ToList();
        }

		/// <summary>
		/// Retrieves a list of salaries with applied filters asynchronously.
		/// </summary>
		/// <param name="personsVM">The filter parameters.</param>
		/// <returns>A list of salaries matching the specified filters.</returns>
		public async Task<List<TableSalaryVM>> GetSalaryWithFilters(PersonsVM personsVM)
        {
			DataTable dt = new();

			using SqlConnection con = new(_config.GetConnectionString("DefaultConnection"));
			await con.OpenAsync();

			SqlCommand command = con.CreateCommand();

			command.CommandText = "GetSalaryReportWithFilters";
			command.CommandType = CommandType.StoredProcedure;

			command.Parameters.Add("@positionID", SqlDbType.UniqueIdentifier).Value = personsVM.Position;
			command.Parameters.Add("@departID", SqlDbType.UniqueIdentifier).Value = personsVM.Department;
			command.Parameters.Add("@companyID", SqlDbType.UniqueIdentifier).Value = personsVM.Company;

			using SqlDataAdapter adapter = new(command);

			adapter.Fill(dt);

			List<TableSalaryVM> persons = [];
			foreach (DataRow dr in dt.Rows)
			{
				persons.Add(new TableSalaryVM
				{
					FullName = dr.Field<string>("Surname") + " " + dr.Field<string>("Name_"),
					Position = dr.Field<string>("Position_name"),
					Department = dr.Field<string>("Department_name"),
					Company = dr.Field<string>("Company_name"),
					Salary = dr.Field<decimal>("Salary")
				});
			}
			return persons.OrderBy(x => x.FullName).ToList();
		}

		/// <summary>
		/// Generates a text representation of a salary table and returns it as a byte array asynchronously.
		/// </summary>
		/// <param name="salaryTable">The list of salary items to include in the table.</param>
		/// <returns>A byte array representing the salary table.</returns>
		public async Task<byte[]> SaveSalaryTable(List<TableSalaryVM> salaryTable)
		{
			StringBuilder txt = new();
			int padR = 30;

			txt.AppendLine($"{"Full name".PadRight(padR)}|{"Position".PadRight(padR)}|{"Department".PadRight(padR)}|{"Company".PadRight(padR)}|{ "Salary".PadRight(padR)}|");
			txt.AppendLine($"{new string('-',padR)}|{new string('-', padR)}|{new string('-', padR)}|{new string('-', padR)}|{new string('-', padR)}|");

			foreach (var item in salaryTable)
			{
				txt.AppendLine($"{item?.FullName?.PadRight(padR)}|{item?.Position?.PadRight(padR)}|{item?.Department?.PadRight(padR)}|{item?.Company?.PadRight(padR)}|{item?.Salary?.ToString().PadRight(padR)}|");
			}

			txt.AppendLine($"{new string('-', padR)}|{new string('-', padR)}|{new string('-', padR)}|{new string('-', padR)}|{new string('-', padR)}|");

			txt.AppendLine($"{"Total".PadRight(padR*4+3)}|{salaryTable?.Sum(x => x.Salary)?.ToString().PadRight(padR)}|");

			return Encoding.UTF8.GetBytes(txt.ToString());

		}
	}
}
