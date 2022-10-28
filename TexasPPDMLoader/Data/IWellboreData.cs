using PPDMLoaderLibrary.Models;

namespace TexasPPDMLoader.Data
{
    public interface IWellboreData
    {
        Task SaveWellbores(List<Wellbore> wellbores, string connectionString);
        Task<List<Wellbore>> ReadWellbores(string connectionString);
    }
}
