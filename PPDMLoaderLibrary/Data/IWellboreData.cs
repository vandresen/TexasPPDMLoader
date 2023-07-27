using PPDMLoaderLibrary.Models;

namespace PPDMLoaderLibrary.Data
{
    public interface IWellboreData
    {
        Task SaveWellbores(List<Wellbore> wellbores, string connectionString);
        Task<List<Wellbore>> ReadWellbores(string connectionString);
        Task<List<WellHeaderData>> ReadWellInfo(string connectionString);
    }
}
