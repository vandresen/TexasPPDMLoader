using PPDMLoaderLibrary.DataAccess;
using PPDMLoaderLibrary.Models;

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
            await _da.SaveData<List<Wellbore>>(connectionString, wellbores, "");
        }
    }
}
