using Microsoft.Data.SqlClient;
using Dapper;
using DapperConsole.Models;

namespace DapperConsole.Relationship_Examples;

public class RelationalQueryManager
{
    public async Task<IEnumerable<SalesOrder>> SalesOrderMinAmountDue(int minimumAmount)
    {
        using (var connection = new SqlConnection(Config.ConnectionString))
        {
            await connection.OpenAsync();
            var sql = @"SELECT
                soh.SalesOrderID, soh.OrderDate, soh.TotalDue,
                sd.SalesOrderDetailID, sd.ProductID, sd.OrderQty, sd.LineTotal
                FROM SalesLT.SalesOrderHeader soh
                JOIN SalesLT.SalesOrderDetail sd ON soh.SalesOrderID = sd.SalesOrderID
                WHERE soh.TotalDue > @minAmount";
            var orders = await connection.QueryAsync<SalesOrder, SalesOrderDetail, SalesOrder>(
                sql,
                (order, detail) =>
                {
                    order.OrderDetails = order.OrderDetails ?? new List<SalesOrderDetail>();
                    order.OrderDetails.Add(detail);
                    return order;
                }, new { minAmount = 1000 },
                splitOn: "SalesOrderDetailID" // Split results at SalesOrderDetailID to map details to child object.
            );

            return orders;
        }
    }

    public async Task<IEnumerable<Product>> ProductsWithCategories()
    {
        using (var connection = new SqlConnection(Config.ConnectionString))
        {
            await connection.OpenAsync();
            var sql = @"
            SELECT 
                p.ProductID, p.Name, p.ProductNumber,
                c.CategoryID, c.Name AS CategoryName
            FROM SalesLT.Product p
            JOIN SalesLT.ProductCategoryLink pcl ON p.ProductID = pcl.ProductID
            JOIN SalesLT.Category c ON pcl.CategoryID = c.CategoryID
            ORDER BY p.ProductID";

            var productDict = new Dictionary<int, Product>();

            var products = await connection.QueryAsync<Product, Category, Product>(
                sql,
                (product, category) =>
                {
                    if (!productDict.TryGetValue(product.ProductId, out var existingProduct))
                    {
                        existingProduct = product;
                        existingProduct.Categories = new List<Category>();
                        productDict.Add(existingProduct.ProductId, existingProduct);
                    }

                    existingProduct.Categories.Add(category);
                    return existingProduct;
                },
                splitOn: "CategoryID"
            );
            return productDict.Values;
        }
    }
}
