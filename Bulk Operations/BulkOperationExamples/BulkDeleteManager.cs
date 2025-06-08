using Microsoft.Data.SqlClient;
using DapperConsole.Models;
using Z.Dapper.Plus;

namespace DapperConsole.BulkOperationExamples;

public class BulkDeleteManager
{
    public async Task BulkDeleteProductsAsync(IEnumerable<Product> products)
    {
        using var connection = new SqlConnection(Config.ConnectionString);
        await connection.BulkDeleteAsync(products);
    }
}
