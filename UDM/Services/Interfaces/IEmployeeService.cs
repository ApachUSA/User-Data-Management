using System.Data.SqlClient;
using UDM.Models;

namespace UDM.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<Employee> GetEmployeeByID(Guid? employeeID);

        Task<Guid> InsertEmployee(Employee employee, SqlCommand command);

        Task UpdateEmployee(Employee employee, Guid? personID, SqlCommand command);

		Task DeleteEmployee(Guid? employeeID, SqlCommand command);
	}
}
