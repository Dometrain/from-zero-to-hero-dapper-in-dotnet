using Microsoft.Data.SqlClient;
using Dapper;
using Z.Dapper.Plus;
using DapperConsole.Models;

using DapperConsole.Models;

namespace DapperConsole.BulkOperationExamples;

public class BulkUpdateManager
{
    public async Task BulkUpdateProductsAsync(List<Product> products)
    {
        using var connection = new SqlConnection(Config.ConnectionString);
        await connection.BulkUpdateAsync(products);
    }
}
