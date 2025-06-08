using Microsoft.Data.SqlClient;
using Dapper;
using DapperConsole.Models;
using DapperConsole.QueryingDataExamples;

namespace DapperConsole;

class Program
{
    static async Task Main(string[] args)
    {
        //single value queries
        var singleValueQueryManager = new SingleValueQueryManager();
        var totalCustomers = await singleValueQueryManager.GetTotalCustomersAsync();
        var lastName = await singleValueQueryManager.GetCustomerLastNameAsync(22);
        
        //single row queries
        var singleRowQueryManager = new SingleRowQueryManager();
        var customer = await singleRowQueryManager.GetCUstomerByIdAsync(22);
        
        //multiple row queries
        var multipleRowQueryManager = new MultipleRowQueryManager();
        var redProducts = await multipleRowQueryManager.GetProductByColourAsync("Red");
        var blueProducts = await multipleRowQueryManager.GetPartialProductByColourAsync("Blue");
        
    }

    
}
