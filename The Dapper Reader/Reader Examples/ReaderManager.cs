using Microsoft.Data.SqlClient;
using Dapper;

namespace DapperConsole.Reader_Examples;

public class ReaderManager
{
    public async Task PrintOrdersOverMinAmount(int minimumAmount)
    {
        using (var connection = new SqlConnection(Config.ConnectionString))
        {
            var sql = @"
                        SELECT SalesOrderID, OrderDate, TotalDue, Comment
                        FROM SalesLT.SalesOrderHeader
                        WHERE TotalDue > @minAmount";

            using (var reader = await connection.ExecuteReaderAsync(sql, new { minAmount = minimumAmount }))
            {
                while (await reader.ReadAsync())
                {
                    var orderId = reader.GetInt32(0);
                    var orderDate = reader.GetDateTime(1);
                    var totalDue = reader.GetDecimal(2);
                    string? comment = !reader.IsDBNull(3) ? reader.GetString(3) : null;
                    
                    Console.WriteLine($"Order {orderId} on {orderDate:d} - " +
                                      $"Total: {totalDue:C}, Comment: {comment ?? "[none]"}");
                }
            }
                                      
        }
    }
}
