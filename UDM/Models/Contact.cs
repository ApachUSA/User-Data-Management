namespace UDM.Models
{
	public class Contact : EntityBase
	{
		public required string Phone_Number { get; set; }

		public Guid? PersonID { get; set; }

		public Person? Person { get; set; }
	}
}
