namespace UDM.Models
{
	public class Department : EntityBase
	{
		public string? Department_Name { get; set; }

		public Guid? CompanyID { get; set; }

		public Company? Company { get; set; }
	}
}
