using Microsoft.Data.SqlClient;
using PPDMLoaderLibrary;
using PPDMLoaderLibrary.Extensions;
using PPDMLoaderLibrary.Models;

namespace TexasPPDMLoader
{
    public class App
    {
        public static async Task<int> RunLoader(InputData input)
        {
            try
            {
                var dl = new DownloadDataFromWeb(input.Path);
                dl.DownloadWells(input.CountyCode);
                dl.DownloadApiData(input.CountyCode);
                dl.DownloadFullWellboreData();

                if (!string.IsNullOrWhiteSpace(input.ConnectionString))
                {
                    try
                    {
                        await using var sql = new SqlConnection(input.ConnectionString);
                        Console.WriteLine("Connecting...");
                        await sql.OpenAsync();
                        Console.WriteLine("Connected!");
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("SQL CONNECTION FAILED:");
                        Console.ResetColor();
                        Console.WriteLine(ex.Message);
                        return 2;
                    }
                    
                }

                var twd = new TexasWellData();
                var wells = await twd.GetTexasWells(input);

                var tfd = new TexasFullWellboreData();
                var full = await tfd.GetTexasFullWellboreData(input);

                var formations = tfd.GetTexasFormationData(input);
                var perfs = tfd.GetTexasPerforationData(input);
                var casings = tfd.GetTexasCasingData(input);

                wells = wells.MergeWellboreObjects(full);

                var tds = new TexasDataStore();
                await tds.Savewells(input, wells);
                await tds.SaveFormations(input, formations);
                await tds.SavePerforations(input, perfs);
                await tds.SaveCasings(input, casings);

                Console.WriteLine("Loading complete");
                return 0;
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("UNEXPECTED ERROR:");
                Console.ResetColor();
                Console.WriteLine(ex);
                return 1;
            }
        }
    }
}
