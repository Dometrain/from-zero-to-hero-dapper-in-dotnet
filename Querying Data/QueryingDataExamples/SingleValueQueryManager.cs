using Microsoft.Data.SqlClient;
using Dapper;

namespace DapperConsole.QueryingDataExamples;

public class SingleValueQueryManager
{
    public async Task<int> GetTotalCustomersAsync()
    {
        using var connection = new SqlConnection(Config.ConnectionString);
        var sql = "SELECT COUNT(*) FROM SalesLT.Customer";
        var count = await connection.QuerySingleAsync<int>(sql);
        return count;
    }

    public async Task<string> GetCustomerLastNameAsync(int customerId)
    {
        using var connection = new SqlConnection(Config.ConnectionString);
        var sql = "SELECT LastName FROM SalesLT.Customer WHERE CustomerID = @customerId";
        var lastName = await connection.QuerySingleAsync<string>(sql, new { @customerId });
        return lastName;
    }
}
