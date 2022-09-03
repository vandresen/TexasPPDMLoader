using TexasPPDMLoader;
using TexasPPDMLoader.Models;

string path = @"C:\Users\vidar\Downloads\well001";
string countyCode = "001";

InputData input = new InputData()
{
    Path = path,
    CountyCode = countyCode,
    ConnectionString = ""
};

try
{
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
