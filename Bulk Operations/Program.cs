using Microsoft.Data.SqlClient;
using Dapper;
using DapperConsole.BulkOperationExamples;
using DapperConsole.Models;
using Z.Dapper.Plus;


namespace DapperConsole;

class Program
{
    static async Task Main(string[] args)
    {
        //Map Product table
        DapperPlusManager.Entity<Product>().Table("SalesLT.Product");
        
        //Bulk Insert
        var bulkInsertManager = new BulkInsertManager();
        await bulkInsertManager.BulkInsertProductsAsync(Config.dummyProducts);
        
        //Bulk Update
        var bulkUpdateManager = new BulkUpdateManager();
        var productsToUpdate = Config.dummyProducts.Take(2).ToList();
        productsToUpdate[0].ProductId = 1028;
        productsToUpdate[1].ProductId = 1029;
        productsToUpdate[0].Name = "XBOX";
        productsToUpdate[1].Name = "Playstation 5";
        await bulkUpdateManager.BulkUpdateProductsAsync(productsToUpdate);
        
        //Bulk Merge
        
        
        List<Product> products = new List<Product>
        {
            new Product
            {
                //This will be updated as it was created from Config.DummyProducts
                ProductId = 1,
                Name = "Mountain Bike",
                ProductNumber = "BK-M68B-42",
                Color = "Blue", //changed from red to blue
                StandardCost = 500.00m,
                ListPrice = 749.99m,
                Size = "M",
                Weight = 15.5m,
                ProductCategoryId = 1,
                ProductModelId = 101,
                SellStartDate = DateTime.UtcNow,
                SellEndDate = DateTime.UtcNow.AddYears(2),
                DiscontinuedDate = null,
                ThumbNailPhoto = null,
                ThumbnailPhotoFileName = "mountainbike.jpg",
                rowguid = Guid.NewGuid(),
                ModifiedDate = DateTime.UtcNow
            },
            new Product
            {
                //This will be created as a new record
                Name = "Some new record",
                ProductNumber = "BK-R79Y-44",
                Color = "Blue",
                StandardCost = 450.00m,
                ListPrice = 699.99m,
                Size = "L",
                Weight = 14.2m,
                ProductCategoryId = 2,
                ProductModelId = 102,
                SellStartDate = DateTime.UtcNow,
                SellEndDate = DateTime.UtcNow.AddYears(2),
                DiscontinuedDate = null,
                ThumbNailPhoto = null,
                ThumbnailPhotoFileName = "roadbike.jpg",
                rowguid = Guid.NewGuid(),
                ModifiedDate = DateTime.UtcNow
            }
        };
        
        await bulkInsertManager.BulkInsertProductsAsync(products);
        
        
        var productsToDelete = Config.dummyProducts.Take(2).ToList();
        productsToDelete[0].ProductId = 1028; //change productID as needed
        productsToDelete[1].ProductId = 1029;
        var bulkDeleteManager = new BulkDeleteManager();
        await bulkDeleteManager.BulkDeleteProductsAsync(productsToDelete);


    }
  
}
