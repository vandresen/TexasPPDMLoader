using PPDMLoaderLibrary.DataAccess;
using PPDMLoaderLibrary.Models;
using TexasPPDMLoader.Data;

namespace TexasPPDMLoader
{
    public class TexasDataStore
    {
        private IWellboreData _wb;
        private IDataAccess _da;

        public TexasDataStore()
        {
            
        }

        public async Task Savewells(InputData input, List<Wellbore> wells)
        {
            string connectionString = "";
            if (string.IsNullOrEmpty(input.ConnectionString))
            {
                connectionString = input.Path + @"\" + input.CountyCode + @".csv";
                _da = new CsvHelperDataAccess();
                _wb = new WellboreDataCsv(_da);
            }
            else
            {
                connectionString = input.ConnectionString;
                _da = new DapperDataAccess();
                _wb = new WellboreDataDapper(_da);
            }
            
            await _wb.SaveWellbores(wells, connectionString);
        }
    }
}
