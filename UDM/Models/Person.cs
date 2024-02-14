namespace UDM.Models
{
	public class Person : EntityBase
	{
		public required string Surname { get; set; }

		public required string Name { get; set; }

		public string? Patronymic { get; set; }

		public DateTime? Birth_Date { get; set; }

		public Guid? AddressID { get; set; }

		public Guid? EmployeeID { get; set; }

		public Address? Address { get; set; }

		public Employee? Employee { get; set; }

		public List<Contact>? Contacts { get; set; }

	}
}
