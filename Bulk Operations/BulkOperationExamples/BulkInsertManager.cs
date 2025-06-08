using Microsoft.Data.SqlClient;
using Dapper;
using Z.Dapper.Plus;
using DapperConsole.Models;

namespace DapperConsole.BulkOperationExamples;

public class BulkInsertManager
{
    public async Task BulkInsertProductsAsync(List<Product> products)
    {
        using var connection = new SqlConnection(Config.ConnectionString);
        await connection.BulkInsertAsync(products);
    }
}
