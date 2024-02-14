using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Data.SqlClient;
using UDM.Models;
using UDM.Services.Implementations;
using UDM.Services.Interfaces;

namespace UDM.Controllers
{
	public class PersonController : Controller
	{
		private readonly IPersonTransactionService _personTransactionService;

		public PersonController(IPersonTransactionService personTransactionService)
		{
			_personTransactionService = personTransactionService;
		}

		[HttpGet]
		public IActionResult CreatePerson() => View("PersonCard");
		
		
		[HttpPost]
		public async Task<IActionResult> CreatePerson(Person person)
		{
			var response = await _personTransactionService.CreatePersonAsync(person);
			if(response.Success)
			{
				return Ok(response.Message);
			}
			return BadRequest(response.Message);
		}

        [HttpGet]
        public async Task<IActionResult> PersonDetails(Guid personID)
        {	
            return View("PersonCard", await _personTransactionService.GetPersonAsync(personID));
        }

		[HttpPost]
		public async Task<IActionResult> UpdatePerson(Person person)
		{
			var response = await _personTransactionService.UpdatePersonAsync(person);
			if (response.Success)
			{
				return Ok(response.Message);
			}
			return BadRequest(response.Message);
		}

		[HttpPost]
		public async Task<IActionResult> DeletePerson(Guid personID,Guid addressID, Guid employeeID)
		{
			var response = await _personTransactionService.DeletePersonAsync(personID,addressID,employeeID);
			if (response.Success)
			{
				return Ok(response.Message);
			}
			return BadRequest(response.Message);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteAddress(Guid addressID)
		{
			var response = await _personTransactionService.DeleteAddressAsync(addressID);
			if (response.Success)
			{
				return Ok(response.Message);
			}
			return BadRequest(response.Message);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteEmployee(Guid employeeID)
		{
			var response = await _personTransactionService.DeleteEmployeeAsync(employeeID);
			if (response.Success)
			{
				return Ok(response.Message);
			}
			return BadRequest(response.Message);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteContact(Guid contactID)
		{
			var response = await _personTransactionService.DeleteContactAsync(contactID);
			if (response.Success)
			{
				return Ok(response.Message);
			}
			return BadRequest(response.Message);
		}
	}
}
