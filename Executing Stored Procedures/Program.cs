using Microsoft.Data.SqlClient;
using Dapper;
using DapperConsole.Models;
using DapperConsole.Stored_Procedure_Examples;

namespace DapperConsole;

class Program
{
    static async Task Main(string[] args)
    {
        var storedProcedureManager = new StoredProcedureManager();
        var customerOrders = await storedProcedureManager.GetOrdersByCustomerIdAsync(22);
    }
    


    
}
