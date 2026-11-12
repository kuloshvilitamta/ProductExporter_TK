using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
namespace DataBaseConnector;

public static class DBConnector
{
    public static SqlConnection GetSqlConnection ()=>new(BuildingConfiguration());

    public static string? BuildingConfiguration()
    {
        var configuration = new ConfigurationBuilder()
               .SetBasePath(AppContext.BaseDirectory)
               .AddJsonFile("dbsettings.json", optional: false)
               .Build();

        return configuration.GetConnectionString("DefaultConnection");
    }
}
