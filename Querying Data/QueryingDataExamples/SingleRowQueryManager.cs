using Microsoft.Data.SqlClient;
using Dapper;
using DapperConsole.Models;

namespace DapperConsole.QueryingDataExamples;

public class SingleRowQueryManager
{
    public async Task<Customer?> GetCUstomerByIdAsync(int customerId)
    {
        using var connection = new SqlConnection(Config.ConnectionString);
        var sql = "SELECT * FROM SalesLT.Customer WHERE CustomerId = @customerId";
        var customer = await connection.QuerySingleOrDefaultAsync<Customer>(sql, new { @customerId });
        return customer;
    }
}
