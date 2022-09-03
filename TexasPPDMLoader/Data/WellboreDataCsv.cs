using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasPPDMLoader.DataAccess;
using TexasPPDMLoader.Models;

namespace TexasPPDMLoader.Data
{
    public class WellboreDataCsv: IWellboreData
    {
        private readonly IDataAccess _da;

        public WellboreDataCsv(IDataAccess da)
        {
            _da = da;
        }

        public Task<List<Wellbore>> ReadWellbores(string connectionString)
        {
            throw new NotImplementedException();
        }

        public async Task SaveWellbores(List<Wellbore> wellbores, string connectionString)
        {
            await _da.SaveData<List<Wellbore>>(connectionString, wellbores);
        }
    }
}
