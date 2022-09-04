﻿using TexasPPDMLoader;
using TexasPPDMLoader.Models;

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
