using Microsoft.Data.SqlClient;
using Dapper;
using DapperConsole.Models;

namespace DapperConsole.Security_And_Performance_Best_Practices;

public class SingleRowQueryManager
{
   //SQL Injection Bad Practice
   public SalesOrderDetail GetSalesOrderDetailBadExample(string uniqueId)
   {
      using var connection = new SqlConnection(Config.ConnectionString);
      var sql = "SELECT * FROM SalesLT.SalesOrderDetail WHERE rowguid = " + uniqueId;
      var result = connection.QuerySingleOrDefault<SalesOrderDetail>(sql);
      return result;
   }
   
   //SQL Injection Good Practice
   public SalesOrderDetail GetSalesOrderDetail(string uniqueId)
   {
      using var connection = new SqlConnection(Config.ConnectionString);
      var sql = "SELECT * FROM SalesLT.SalesOrderDetail WHERE rowguid = CAST(@rowGuid as UNIQUEIDENTIFIER)";
      var result = connection.QuerySingleOrDefault<SalesOrderDetail>(sql, new { rowGuid = uniqueId });
      return result;
   }
   
}
