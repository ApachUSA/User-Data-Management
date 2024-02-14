using System.Data;
using System.Data.SqlClient;

namespace UDM.Services.Interfaces
{
	public interface IDbConnectionProvider
	{
		SqlConnection GetConnection();
	}
}
