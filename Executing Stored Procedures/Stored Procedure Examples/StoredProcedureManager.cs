
using System.Data;
using Microsoft.Data.SqlClient;
using DapperConsole.Models;
using Dapper;

namespace DapperConsole.Stored_Procedure_Examples;

public class StoredProcedureManager
{
    public async Task<IEnumerable<SalesOrder>> GetOrdersByCustomerIdAsync(int customerId)
    {
        using var connection = new SqlConnection(Config.ConnectionString);
        var orders =
            await connection.QueryAsync<SalesOrder>("SalesLT.GetOrdersByCustomer", 
                new { CustomerId = customerId }, 
                commandType: CommandType.StoredProcedure);

        return orders;
    }
}
