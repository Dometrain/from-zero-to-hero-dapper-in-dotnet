using Microsoft.Data.SqlClient;
using Dapper;
using DapperConsole.Models;
using DapperConsole.Non_Query_Operations_Examples;


namespace DapperConsole;

class Program
{
    static async Task Main(string[] args)
    {
       //data insert
       var dataInsertManager = new DataInsertManager();
       var exampleProduct = new Product
       {
           ProductId = 1001,
           Name = "Mountain Bike",
           ProductNumber = "BK-M68B-42",
           Color = "Red",
           StandardCost = 500.00m,
           ListPrice = 749.99m,
           Size = "M",
           Weight = 15.5m,
           ProductCategoryId = 1,
           ProductModelId = 42,
           SellStartDate = DateTime.UtcNow,
           SellEndDate = DateTime.UtcNow.AddYears(2),
           DiscontinuedDate = null,
           ThumbNailPhoto = null, 
           ThumbnailPhotoFileName = "bike-thumb.jpg",
           rowguid = Guid.NewGuid(),
           ModifiedDate = DateTime.UtcNow
       };
       
       var productId = dataInsertManager.InsertProduct(exampleProduct);
       
       //delete data
       var dataRemovalManager = new DataRemovalManager();
       dataRemovalManager.DeleteProduct(productId);
       dataRemovalManager.DeleteProductsFromId(800);
    }
    


    
}
