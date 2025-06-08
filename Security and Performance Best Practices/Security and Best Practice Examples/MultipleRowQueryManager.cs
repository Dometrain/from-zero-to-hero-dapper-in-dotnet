using Microsoft.Data.SqlClient;
using Dapper;
using DapperConsole.Models;

namespace DapperConsole.Security_And_Performance_Best_Practices;

public class MultipleRowQueryManager
{
    //Unbuffered Query
    public IEnumerable<Product> GetProductByColourUnbuffered(string colour)
    {
        using var connection = new SqlConnection(Config.ConnectionString);
        var sql = "SELECT * FROM Sales.LT.Product WHERE Color = @colour";
        var result = connection.Query<Product>(sql, 
            new { Color = @colour },
            buffered: false);
        return result;
    }
    
    //Create a temp table
    public async Task<IEnumerable<Product>> ProductTempTableExample()
    {
        using var connection = new SqlConnection(Config.ConnectionString);
        var sql = @"
                    CREATE TABLE #ProductIds (Id INT);
                    INSERT INTO #ProductIds (Id) VALUES (@Id1), (@Id2), (@Id3);
                    SELECT p.* FROM SalesLT.Product P 
                    JOIN #ProductIds pid ON p.ProductId = pid.Id;
                  ";
        var products = connection.Query<Product>(sql, new { Id1 = 721, Id2 = 722, Id3 = 723 });
        return products;
    }
    
    public async Task<IEnumerable<Product>> GetProductByColourAsync(string colour)
    {
        using var connection = new SqlConnection(Config.ConnectionString);
        var sql = "SELECT * FROM Sales.LT.Product WHERE Color = @colour";
        var result = await connection.QueryAsync<Product>(sql, new { Color = @colour });
        return result;
    }
    
    public async Task<IEnumerable<ProductPartial>> GetPartialProductByColourAsync(string colour)
    {
        using var connection = new SqlConnection(Config.ConnectionString);
        var sql = "SELECT * FROM Sales.LT.Product WHERE Color = @colour";
        var result = await connection.QueryAsync<ProductPartial>(sql, new { Color = @colour });
        return result;
    }
    
    //synchronous version
    public IEnumerable<Product> GetProductByColour(string colour)
    {
        using var connection = new SqlConnection(Config.ConnectionString);
        var sql = "SELECT * FROM Sales.LT.Product WHERE Color = @colour";
        var result = connection.Query<Product>(sql, new { Color = @colour });
        return result;
    }
}
