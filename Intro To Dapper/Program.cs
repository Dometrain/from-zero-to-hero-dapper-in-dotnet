using Microsoft.Data.SqlClient;
using Dapper;
using DapperConsole.Models;

namespace DapperConsole;

class Program
{
    static async Task Main(string[] args)
    {
        var customers = await GetCustomersWithDapperAsync();
    }

    public static async Task<List<Customer>> GetCustomersWithADONetAsync()
    {
        var customers = new List<Customer>();

        using (var connection = new SqlConnection(Config.ConnectionString))
        {
            await connection.OpenAsync();
            var commend = new SqlCommand("SELECT * FROM SalesLT.Customer", connection);
            var reader = await commend.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                customers.Add(new Customer()
                    {
                        CustomerID = reader.GetInt32(0),
                        NameStyle = reader.GetBoolean(1),
                        Title = reader.IsDBNull(2) ? null : reader.GetString(2),
                        FirstName = reader.GetString(3),
                        MiddleName = reader.IsDBNull(4) ? null : reader.GetString(4),
                        LastName = reader.GetString(5),
                        Suffix = reader.IsDBNull(6) ? null : reader.GetString(6),
                        CompanyName = reader.GetString(7),
                        SalesPerson = reader.GetString(8),
                        EmailAddress = reader.GetString(9),
                        Phone = reader.GetString(10),
                        PasswordHash = reader.GetString(11),
                        PasswordSalt = reader.GetString(12),
                        rowguid = reader.GetGuid(13),
                        ModifiedDate = reader.GetDateTime(14)
                    }
                );
            }
        }
        
        return customers;
    }
    
    public static async Task<IEnumerable<Customer>> GetCustomersWithDapperAsync()
    {
        using (var connection = new SqlConnection(Config.ConnectionString))
        {
            var customers = await connection.QueryAsync<Customer>("SELECT * FROM SalesLT.Customer");
            return customers;
        }
    }
}
