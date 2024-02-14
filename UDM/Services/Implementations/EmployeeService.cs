using System.Data;
using System.Data.SqlClient;
using System.Net;
using UDM.Models;
using UDM.Services.Interfaces;

namespace UDM.Services.Implementations
{
	public class EmployeeService : IEmployeeService
	{
        private readonly IConfiguration _config;

        public EmployeeService(IConfiguration config)
        {
            _config = config;
        }

		/// <summary>
		/// Retrieves an employee by their unique identifier asynchronously.
		/// </summary>
		/// <param name="employeeID">The unique identifier of the employee.</param>
		/// <returns>An instance of the Employee class representing the retrieved employee.</returns>
		public async Task<Employee> GetEmployeeByID(Guid? employeeID)
        {
            DataTable dt = new();

            using SqlConnection con = new(_config.GetConnectionString("DefaultConnection"));
            await con.OpenAsync();

            using SqlCommand cmd = new($"select * from Employees " +
                $" LEFT JOIN Positions on Employees.Position_id = Positions.Position_id " +
                $" LEFT JOIN Departments on Positions.Department_id = Departments.Department_id" +
                $" LEFT JOIN Companies on Departments.Company_id = Companies.Company_id" +
                $" where Employee_id =  '{employeeID}'", con);
            using SqlDataAdapter adapter = new(cmd);

            adapter.Fill(dt);

            DataRow row = dt.Rows[0];

            var employee = new Employee
            {
                ID = row.Field<Guid>("Employee_id"),
                Date_of_hire = row.Field<DateTime?>("Date_of_hire"),
                Salary = row.Field<decimal>("Salary"),
                PositionID = row.Field<Guid?>("Position_id"),
                Position = new()
                {
                    ID = row.Field<Guid?>("Position_id"),
                    Department = new()
                    {
                        ID = row.Field<Guid?>("Department_id"),
                        Company = new() { ID = row.Field<Guid?>("Company_id") }
                    }
                },
                
            };

            return employee;
        }

		/// <summary>
		/// Inserts a new employee into the database asynchronously.
		/// </summary>
		/// <param name="employee">The employee object to be inserted.</param>
		/// <param name="command">The SqlCommand object associated with an open SqlConnection.</param>
		/// <returns>The unique identifier of the newly inserted employee.</returns>
		public async Task<Guid> InsertEmployee(Employee employee, SqlCommand command)
		{
			command.Parameters.Clear();

			command.CommandText = "InsertEmployees";
			command.CommandType = CommandType.StoredProcedure;

			command.Parameters.Add("@Date_of_Hire", SqlDbType.Date).Value = employee.Date_of_hire;
			command.Parameters.Add("@Salary", SqlDbType.Decimal).Value = employee.Salary;
			command.Parameters.Add("@PositionID", SqlDbType.UniqueIdentifier, 20).Value = employee.PositionID;

			SqlParameter outputParameter = command.Parameters.Add("@EmployeesID", SqlDbType.UniqueIdentifier);
			outputParameter.Direction = ParameterDirection.Output;

			await command.ExecuteNonQueryAsync();

			return (Guid)outputParameter.Value;
		}

		/// <summary>
		/// Updates an existing employee asynchronously.
		/// </summary>
		/// <param name="employee">The updated employee object.</param>
		/// <param name="personID">The unique identifier of the person associated with the employee.</param>
		/// <param name="command">The SqlCommand object associated with an open SqlConnection.</param>
		public async Task UpdateEmployee(Employee employee, Guid? personID, SqlCommand command)
		{
			command.Parameters.Clear();

			command.CommandText = "UpdateEmployee";
			command.CommandType = CommandType.StoredProcedure;

			command.Parameters.Add("@EmployeeID", SqlDbType.UniqueIdentifier).Value = employee.ID;
			command.Parameters.Add("@Date_of_Hire", SqlDbType.Date).Value = employee.Date_of_hire;
			command.Parameters.Add("@Salary", SqlDbType.Decimal).Value = employee.Salary;
			command.Parameters.Add("@PositionID", SqlDbType.UniqueIdentifier).Value = employee.PositionID;
			command.Parameters.Add("@PersonID", SqlDbType.UniqueIdentifier).Value = personID;

			await command.ExecuteNonQueryAsync();
		}

		/// <summary>
		/// Deletes an employee from the database asynchronously.
		/// </summary>
		/// <param name="employeeID">The unique identifier of the employee to be deleted.</param>
		/// <param name="command">The SqlCommand object associated with an open SqlConnection.</param>
		public async Task DeleteEmployee(Guid? employeeID, SqlCommand command)
		{
			command.Parameters.Clear();

			command.CommandText = $"delete from Employees where Employee_id = '{employeeID}'";

			await command.ExecuteNonQueryAsync();
		}

	}
}
