using System.Data.SqlClient;
using System.Data;
using UDM.Services.Interfaces;

namespace UDM.Services.Implementations
{
	public class DbConnectionProvider : IDbConnectionProvider
	{
		private readonly IConfiguration _config;

		public DbConnectionProvider(IConfiguration config)
		{
			_config = config;
		}

		public SqlConnection GetConnection()
		{
			return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
		}
	}
}
