using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasPPDMLoader.DataAccess;
using TexasPPDMLoader.Models;

namespace TexasPPDMLoader.Data
{
    public class WellboreDataDapper : IWellboreData
    {
        private readonly IDataAccess _da;

        public WellboreDataDapper(IDataAccess da)
        {
            _da = da;
        }

        public Task<List<Wellbore>> ReadWellbores(string connectionString)
        {
            throw new NotImplementedException();
        }

        public async Task SaveWellbores(List<Wellbore> wellbores, string connectionString)
        {
            string sql = "IF NOT EXISTS(SELECT 1 FROM WELL WHERE UWI = @UWI) " +
                "INSERT INTO WELL (UWI, SURFACE_LONGITUDE, SURFACE_LATITUDE, BOTTOM_HOLE_LATITUDE, BOTTOM_HOLE_LONGITUDE) " +
                "VALUES(@UWI, @SURFACE_LONGITUDE, @SURFACE_LATITUDE, @BOTTOM_HOLE_LATITUDE, @BOTTOM_HOLE_LONGITUDE)";
            await _da.SaveData(connectionString, wellbores,sql);
        }
    }
}
