using Microsoft.Data.SqlClient;
using DapperConsole.Models;
using Z.Dapper.Plus;

namespace DapperConsole.BulkOperationExamples;

public class BulkUpsertManager
{

    public async Task BulkUpsertProductsAsync(IEnumerable<Product> products)
    {
        using var connection = new SqlConnection(Config.ConnectionString);
        await connection.BulkMergeAsync(products);
    }
}
