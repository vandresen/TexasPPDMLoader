using PPDMLoaderLibrary.Models;
using System.CommandLine;
using TexasPPDMLoader;

var pathOption = new Option<string>("--path") { Description = "Directory to store downloaded files (Default = C:\temp)" };
var countyOption = new Option<string>("--county")
{
    Description = "3-character county code",
    Required = true
};
var connectionOption = new Option<string?>("--connection") { Description = "Optional SQL Server connection string (Default = csv file)" };

var root = new RootCommand("Texas PPDMLoader CLI")
{
    pathOption,
    countyOption,
    connectionOption
};

root.SetAction(async parseResult =>
{
    InputData input = new()
    {
        Path = parseResult.GetValue(pathOption) ?? @"C:\temp",
        CountyCode = parseResult.GetValue(countyOption),
        ConnectionString = parseResult.GetValue(connectionOption) ?? ""
    };

    if (input.CountyCode.Length != 3)
    {
        Console.Error.WriteLine("Error: --county must be exactly 3 characters.");
        return 1;
    }

    try
    {
        return await App.RunLoader(input);
    }
    catch (Exception ex)
    {
        Console.Error.WriteLine("Unhandled error:");
        Console.Error.WriteLine(ex.ToString());
        return 3;
    }
});

ParseResult parseResult = root.Parse(args);
return parseResult.Invoke();
