using Microsoft.Data.SqlClient;
using Dapper;
using DapperConsole.Models;
using DapperConsole.Security_And_Performance_Best_Practices;
using Z.Dapper.Plus;


namespace DapperConsole;

class Program
{
    static async Task Main(string[] args)
    {
        var singleRowQueryManager = new SingleRowQueryManager();
        
        //SQL Injection!!!! DON'T DO THIS!!!
        var result = singleRowQueryManager
            .GetSalesOrderDetail(
                "12706FAB-F3A2-48C6-B7C7-1CCDE4081F18; UPDATE SalesLT.SalesOrderDetail SET UnitPriceDiscount = 1000;");
    }
  
}
