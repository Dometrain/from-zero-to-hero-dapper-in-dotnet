using Microsoft.Data.SqlClient;
using Dapper;
using DapperConsole.Models;


namespace DapperConsole.Security_And_Performance_Best_Practices;

public class DataRemovalManager
{
    //Transaction Example
    public async Task DeleteProductFullyAsync(int productId)
    {
        using var connection = new SqlConnection(Config.ConnectionString);
        await connection.OpenAsync();
        using var transaction = connection.BeginTransaction();
        
        var deleteLinkSql = "DELETE FROM SalesLT.ProductCategoryLink WHERE ProductId = @ProductId";
        var sql = "DELETE FROM SalesLT.Product WHERE ProductId = @ProductId";

        try
        {
            await connection.ExecuteAsync(deleteLinkSql, new { ProductId = productId });
            await connection.ExecuteAsync(sql, new { ProductId = productId });
            await transaction.CommitAsync();
        }
        catch
        {
            transaction.Rollback();
        }
        
       
    }

    public void DeleteProductsFromId(int fromId)
    {
        using var connection = new SqlConnection(Config.ConnectionString);
        var sql = "DELETE FROM SalesLT.Product WHERE ProductId > @FromId";
        connection.Execute(sql, new { FromId = fromId });
    }
}
