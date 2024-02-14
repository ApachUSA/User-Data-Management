namespace UDM.Models
{
	public class Position : EntityBase
	{
		public string? Position_Name { get; set; }

		public Guid? DepartmentID { get; set; }

		public Department? Department { get; set; }
	}
}
