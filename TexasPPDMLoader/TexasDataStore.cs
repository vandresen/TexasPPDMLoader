using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasPPDMLoader.Data;
using TexasPPDMLoader.DataAccess;
using TexasPPDMLoader.Models;

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
            string connectionstring = input.Path + @"\" + input.CountyCode + @".csv";
            _da = new CsvHelperDataAccess();
            _wb = new WellboreDataCsv(_da);
            await _wb.SaveWellbores(wells, connectionstring);
        }
    }
}
