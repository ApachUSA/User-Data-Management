using System.Data.SqlClient;
using System.Data;
using UDM.Models;
using UDM.Services.Interfaces;

namespace UDM.Services.Implementations
{
	public class SelectDataService : ISelectDataService
	{
		private readonly IDbConnectionProvider _connectionProvider;

		public SelectDataService(IDbConnectionProvider connectionProvider)
		{
			_connectionProvider = connectionProvider;
		}

		public async Task<List<City>> GetCitiesAsync()
		{
			DataTable dt = new();

			using SqlConnection con = _connectionProvider.GetConnection();
			await con.OpenAsync();

			using SqlCommand cmd = new("select * from Cities", con);
			using SqlDataAdapter adapter = new(cmd);

			adapter.Fill(dt);

			List<City> cities = [];
			foreach (DataRow dr in dt.Rows)
			{
				cities.Add(new City
				{
					ID = (Guid)dr["City_id"],
					City_Name = dr["City_name"].ToString()!,
				});
			}

			return cities;
		}

		public async Task<List<Company>> GetCompaniesAsync()
		{
			DataTable dt = new();

			using SqlConnection con = _connectionProvider.GetConnection();
			await con.OpenAsync();

			using SqlCommand cmd = new("select * from Companies", con);
			using SqlDataAdapter adapter = new(cmd);

			adapter.Fill(dt);

			List<Company> companies = [];
			foreach (DataRow dr in dt.Rows)
			{
				companies.Add(new Company
				{
					ID = (Guid)dr["Company_id"],
					Company_Name = dr["Company_name"].ToString()!,
					Company_Info = dr["Company_info"].ToString()!,
				});
			}

			return companies;
		}

		public async Task<List<Department>> GetDepartmentsAsync(Guid companyID)
		{

			DataTable dt = new();

			using SqlConnection con = _connectionProvider.GetConnection();
			await con.OpenAsync();

			using SqlCommand cmd = new($"select * from Departments where Company_id = '{companyID}'", con);
			using SqlDataAdapter adapter = new(cmd);

			adapter.Fill(dt);

			List<Department> departments = [];
			foreach (DataRow dr in dt.Rows)
			{
				departments.Add(new Department
				{
					ID = (Guid)dr["Department_id"],
					Department_Name = dr["Department_name"].ToString()!,
				});
			}

			return departments;
		}

		public async Task<List<Position>> GetPositionAsync(Guid departID)
		{

			DataTable dt = new();

			using SqlConnection con = _connectionProvider.GetConnection();
			await con.OpenAsync();

			using SqlCommand cmd = new($"select * from Positions where Department_id = '{departID}'", con);
			using SqlDataAdapter adapter = new(cmd);

			adapter.Fill(dt);

			List<Position> positions = [];
			foreach (DataRow dr in dt.Rows)
			{
				positions.Add(new Position
				{
					ID = (Guid)dr["Position_id"],
					Position_Name = dr["Position_name"].ToString()!,
				});
			}

			return positions;
		}
	}
}
