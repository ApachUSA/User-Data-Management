namespace UDM.Models
{
	public class Address : EntityBase
	{
		public required string Location { get; set; }

		public Guid? CityID { get; set; }

		public City? City { get; set; }
	}
}
