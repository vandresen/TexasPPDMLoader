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
    public class TexasWellData
    {
        private readonly IWellboreData _wb;
        private readonly IDataAccess _da;

        public TexasWellData()
        {
            _da = new DbaseDataAccess();
            _wb = new WellboreDataDbase(_da);
        }

        public async Task<List<Wellbore>> GetTexasWells(InputData input)
        {
            List<Wellbore> wellbores = new List<Wellbore>();

            string connectionString = input.Path + @"\well" + input.CountyCode;
            wellbores = await _wb.ReadWellbores(connectionString);

            return wellbores;
        }
    }
}
