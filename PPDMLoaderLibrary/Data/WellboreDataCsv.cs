using PPDMLoaderLibrary.DataAccess;
using PPDMLoaderLibrary.Models;

namespace PPDMLoaderLibrary.Data
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

        public Task<List<WellHeaderData>> ReadWellInfo(string connectionString)
        {
            throw new NotImplementedException();
        }

        public async Task SaveFormations(List<Formations> formations, string connectionString)
        {
            await _da.SaveData<List<Formations>>(connectionString, formations, "");
        }

        public async Task SavePerforations(List<Perforation> perfs, string connectionString)
        {
            await _da.SaveData<List<Perforation>>(connectionString, perfs, "");
        }

        public async Task SaveWellbores(List<Wellbore> wellbores, string connectionString)
        {
            await _da.SaveData<List<Wellbore>>(connectionString, wellbores, "");
        }
    }
}
