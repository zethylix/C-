using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.ADO;

public class Database(IConfiguration configuration)
{
    private readonly IConfiguration _configuration = configuration;

    public SqlConnection GetConnection()
    {
        return new SqlConnection(_configuration.GetConnectionString("TodoDb"));
    }
}