using System;
using System.Data.SqlClient;
using UDM.Models;
using UDM.Models.ViewModels;
using UDM.Services.Interfaces;

namespace UDM.Services.Implementations
{
	public class PersonTransactionService : IPersonTransactionService
	{
		private readonly IConfiguration _configuration;
		private readonly IPersonService _personService;
		private readonly IEmployeeService _employeeService;
		private readonly IAddressService _addressService;
		private readonly IContactService _contactService;

		public PersonTransactionService(IConfiguration configuration, IPersonService personService, IEmployeeService employeeService, IAddressService addressService, IContactService contactService)
		{
			_configuration = configuration;
			_personService = personService;
			_employeeService = employeeService;
			_addressService = addressService;
			_contactService = contactService;
		}

		/// <summary>
		/// Retrieves a person asynchronously based on their ID.
		/// </summary>
		/// <param name="personID">The ID of the person to retrieve.</param>
		/// <returns>The retrieved person if found; otherwise, null.</returns>
		public async Task<Person?> GetPersonAsync(Guid personID)
		{
			var person = await _personService.GetPersonByID(personID);
			if (person != null)
			{
				if (person.AddressID != null) person.Address = await _addressService.GetAddressByID(person.AddressID);
				if (person.EmployeeID != null) person.Employee = await _employeeService.GetEmployeeByID(person.EmployeeID);
				person.Contacts = await _contactService.GetContactsByID(personID);

				return person;
			}

			return null;
		}

		/// <summary>
		/// Creates a new person asynchronously with transactional support.
		/// </summary>
		/// <param name="person">The person to create.</param>
		/// <returns>A service response indicating the success of the operation.</returns>
		public async Task<ServiceResponseVM> CreatePersonAsync(Person person)
		{
			using SqlConnection con = new(_configuration.GetConnectionString("DefaultConnection"));
			await con.OpenAsync();

			SqlTransaction transaction = con.BeginTransaction();

			SqlCommand command = con.CreateCommand();
			command.Transaction = transaction;
			try
			{

				if (person.Address != null)
					person.AddressID = await _addressService.InsertAddress(person.Address, command);

				if (person.Employee != null)
					person.EmployeeID = await _employeeService.InsertEmployee(person.Employee, command);

				person.ID = await _personService.InsertPerson(person, command);

				if (person.Contacts != null && person.Contacts.Count > 0)
					await _contactService.InsertContact(person.Contacts.First(), person.ID, command);

				await transaction.CommitAsync();

				return new ServiceResponseVM() { Success = true, Message = "Person was successfully created" };
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				return new ServiceResponseVM() { Success = false, Message = $"Rollback transaction. \n {ex.Message}" };
			}
		}

		/// <summary>
		/// Updates a person asynchronously with transactional support.
		/// </summary>
		/// <param name="person">The person to update.</param>
		/// <returns>A service response indicating the success of the operation.</returns>
		public async Task<ServiceResponseVM> UpdatePersonAsync(Person person)
		{
			using SqlConnection con = new(_configuration.GetConnectionString("DefaultConnection"));
			await con.OpenAsync();

			SqlTransaction transaction = con.BeginTransaction();

			SqlCommand command = con.CreateCommand();
			command.Transaction = transaction;
			try
			{
				await _personService.UpdatePerson(person, command);

				if (person.Address != null)
					await _addressService.UpdateAddress(person.Address, person.ID, command);

				if (person.Employee != null)
					await _employeeService.UpdateEmployee(person.Employee, person.ID, command);

				if (person.Contacts != null && person.Contacts.Count > 0)
				{
					foreach (var contact in person.Contacts)
					{
						await _contactService.UpdateContact(contact, person.ID, command);
					}
				}

				await transaction.CommitAsync();

				return new ServiceResponseVM() { Success = true, Message = "Person was successfully created" };
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				return new ServiceResponseVM() { Success = false, Message = $"Rollback transaction. \n {ex.Message}" };
			}
		}

		/// <summary>
		/// Deletes a person asynchronously with transactional support.
		/// </summary>
		/// <param name="personID">The ID of the person to delete.</param>
		/// <param name="addressID">The ID of the address associated with the person.</param>
		/// <param name="employeeID">The ID of the employee associated with the person.</param>
		/// <returns>A service response indicating the success of the operation.</returns>
		public async Task<ServiceResponseVM> DeletePersonAsync(Guid personID, Guid? addressID, Guid? employeeID)
		{
			using SqlConnection con = new(_configuration.GetConnectionString("DefaultConnection"));
			await con.OpenAsync();

			SqlTransaction transaction = con.BeginTransaction();

			SqlCommand command = con.CreateCommand();
			command.Transaction = transaction;
			try
			{
				if (addressID != null)
					await _addressService.DeleteAddress(addressID, command);

				if (employeeID != null)
					await _employeeService.DeleteEmployee(employeeID, command);

				await _personService.DeletePerson(personID, command);

				await transaction.CommitAsync();

				return new ServiceResponseVM() { Success = true, Message = "Person was successfully deleted" };
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				return new ServiceResponseVM() { Success = false, Message = $"Rollback transaction. \n {ex.Message}" };
			}
		}

		/// <summary>
		/// Deletes a contact asynchronously with transactional support.
		/// </summary>
		/// <param name="contactID">The ID of the contact to delete.</param>
		/// <returns>A service response indicating the success of the operation.</returns>
		public async Task<ServiceResponseVM> DeleteContactAsync(Guid contactID)
		{
			using SqlConnection con = new(_configuration.GetConnectionString("DefaultConnection"));
			await con.OpenAsync();

			SqlTransaction transaction = con.BeginTransaction();

			SqlCommand command = con.CreateCommand();
			command.Transaction = transaction;

			try
			{
				await _contactService.DeleteContact(contactID, command);

				await transaction.CommitAsync();

				return new ServiceResponseVM() { Success = true, Message = "Contact was successfully deleted" };
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				return new ServiceResponseVM() { Success = false, Message = $"Rollback transaction. \n {ex.Message}" };
			}
		}

		/// <summary>
		/// Deletes an address asynchronously with transactional support.
		/// </summary>
		/// <param name="addressID">The ID of the address to delete.</param>
		/// <returns>A service response indicating the success of the operation.</returns>
		public async Task<ServiceResponseVM> DeleteAddressAsync(Guid addressID)
		{
			using SqlConnection con = new(_configuration.GetConnectionString("DefaultConnection"));
			await con.OpenAsync();

			SqlTransaction transaction = con.BeginTransaction();

			SqlCommand command = con.CreateCommand();
			command.Transaction = transaction;

			try
			{
				await _addressService.DeleteAddress(addressID, command);

				await transaction.CommitAsync();

				return new ServiceResponseVM() { Success = true, Message = "Address was successfully deleted" };
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				return new ServiceResponseVM() { Success = false, Message = $"Rollback transaction. \n {ex.Message}" };
			}
		}

		/// <summary>
		/// Deletes an employee asynchronously with transactional support.
		/// </summary>
		/// <param name="employeeID">The ID of the employee to delete.</param>
		/// <returns>A service response indicating the success of the operation.</returns>
		public async Task<ServiceResponseVM> DeleteEmployeeAsync(Guid employeeID)
		{
			using SqlConnection con = new(_configuration.GetConnectionString("DefaultConnection"));
			await con.OpenAsync();

			SqlTransaction transaction = con.BeginTransaction();

			SqlCommand command = con.CreateCommand();
			command.Transaction = transaction;

			try
			{
				await _employeeService.DeleteEmployee(employeeID, command);

				await transaction.CommitAsync();

				return new ServiceResponseVM() { Success = true, Message = "Employee was successfully deleted" };
			}
			catch (Exception ex)
			{
				await transaction.RollbackAsync();
				return new ServiceResponseVM() { Success = false, Message = $"Rollback transaction. \n {ex.Message}" };
			}
		}
	}
}
