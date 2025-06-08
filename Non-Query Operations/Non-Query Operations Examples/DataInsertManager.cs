using Microsoft.Data.SqlClient;
using Dapper;
using DapperConsole.Models;


namespace DapperConsole.Non_Query_Operations_Examples;

public class DataInsertManager
{
    public int InsertProduct(Product product)
    {
        using var connection = new SqlConnection(Config.ConnectionString);
        var sql = @"INSERT INTO SalesLT.Product(
                            Name, 
                            ProductNumber, 
                            Color,
                            StandardCost,
                            ListPrice,
                            Size,
                            Weight,
                            ProductCategoryID,
                            ProductModelID,
                            SellStartDate,
                            SellEndDate,
                            DiscontinuedDate,
                            ThumbnailPhoto,
                            ThumbnailPhotoFileName,
                            rowguid,
                            ModifiedDate
                        )
                        VALUES()
                            @Name, 
                            @ProductNumber, 
                            @Color,
                            @StandardCost,
                            @ListPrice,
                            @Size,
                            @Weight,
                            @ProductCategoryID,
                            @ProductModelID,
                            @SellStartDate,
                            @SellEndDate,
                            @DiscontinuedDate,
                            @ThumbnailPhoto,
                            @ThumbnailPhotoFileName,
                            @rowguid,
                            @ModifiedDate
                         );
                         SELECT CAST(SCOPE_IDENTITY() AS INT);";
        
        var productId = connection.ExecuteScalar<int>(sql, product);
        return productId;
    }
}
