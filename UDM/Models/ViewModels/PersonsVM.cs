namespace UDM.Models.ViewModels
{
	public class PersonsVM
	{
		public Guid Id { get; set; }

		public string? FullName { get; set; }

		public Guid? City { get; set; }

		public Guid? Position { get; set; }

		public Guid? Department { get; set; }

		public Guid? Company { get; set; }		
	}
}
