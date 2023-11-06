using Microsoft.Data.SqlClient;
using PPDMLoaderLibrary;
using PPDMLoaderLibrary.Extensions;
using PPDMLoaderLibrary.Models;

Console.Write(@"Enter path (Default = C:\temp): ");
string path = Console.ReadLine();
if (string.IsNullOrEmpty(path)) path = @"C:\temp";
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
    DownloadDataFromWeb dl = new DownloadDataFromWeb(path);
    dl.DownloadWells(countyCode);
    dl.DownloadApiData(countyCode);
    dl.DownloadFullWellboreData();
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

    TexasFullWellboreData tpd = new TexasFullWellboreData();
    List<Wellbore> fullWelbores = tpd.GetTexasFullWellboreData(input);
    List<Formations> formations = tpd.GetTexasFormationData(input);
    List<Perforation> perfs = tpd.GetTexasPerforationData(input);

    wells = wells.MergeWellboreObjects(fullWelbores);

    TexasDataStore tds = new TexasDataStore();
    await tds.Savewells(input, wells);
    await tds.SaveFormations(input, formations);
    await tds.SavePerforations(input, perfs);
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}

Console.WriteLine("Loading complete");
Console.ReadLine();
