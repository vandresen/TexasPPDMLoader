using Microsoft.Data.SqlClient;
using PPDMLoaderLibrary.Models;
using TexasPPDMLoader;

Console.Write("Enter path: ");
string path = Console.ReadLine();
Console.Write("Enter county code (3 characters): ");
string countyCode = Console.ReadLine();
Console.Write("Enter sql server connection string (If blank then output to csv): ");
string connectionString = Console.ReadLine();

InputData input = new InputData()
{
    Path = path,
    CountyCode = countyCode,
    ConnectionString = connectionString
};

try
{
    if (!string.IsNullOrEmpty(connectionString))
    {
        SqlConnection sqlCn = new SqlConnection(connectionString);
        Console.WriteLine("Connection");
        sqlCn.Open();
        Console.WriteLine("Open");
        sqlCn.Close();
    }
    
    TexasWellData twd = new TexasWellData();
    List<Wellbore> wells = await twd.GetTexasWells(input);

    TexasDataStore tds = new TexasDataStore();
    await tds.Savewells(input, wells);
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}

Console.WriteLine("Loading complete");
Console.ReadLine();
