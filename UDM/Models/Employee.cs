namespace UDM.Models
{
	public class Employee : EntityBase
	{
		public DateTime? Date_of_hire {get; set;}

		public decimal? Salary { get; set;}

		public Guid? PositionID { get; set;}

		public Position? Position { get; set;}
	}
}
