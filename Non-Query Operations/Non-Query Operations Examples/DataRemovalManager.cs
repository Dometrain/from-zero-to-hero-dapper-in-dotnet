using Microsoft.Data.SqlClient;
using Dapper;
using DapperConsole.Models;


namespace DapperConsole.Non_Query_Operations_Examples;

public class DataRemovalManager
{
    public void DeleteProduct(int productId)
    {
        using var connection = new SqlConnection(Config.ConnectionString);
        var sql = "DELETE FROM SalesLT.Product WHERE ProductId = @ProductId";
        connection.Execute(sql, new { ProductId = productId });
    }

    public void DeleteProductsFromId(int fromId)
    {
        using var connection = new SqlConnection(Config.ConnectionString);
        var sql = "DELETE FROM SalesLT.Product WHERE ProductId > @FromId";
        connection.Execute(sql, new { FromId = fromId });
    }
}
