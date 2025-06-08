using Microsoft.Data.SqlClient;
using Dapper;
using DapperConsole.Models;
using DapperConsole.Reader_Examples;


namespace DapperConsole;

class Program
{
    static async Task Main(string[] args)
    {
        var readerManager = new ReaderManager();
        await readerManager.PrintOrdersOverMinAmount(1000);
    }

    
}
