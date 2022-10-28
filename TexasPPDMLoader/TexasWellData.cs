using PPDMLoaderLibrary.Models;
using TexasPPDMLoader.Data;
using TexasPPDMLoader.DataAccess;

namespace TexasPPDMLoader
{
    public class TexasWellData
    {
        private readonly IWellboreData _wbl;
        private readonly IWellboreData _wbi;
        private readonly IDataAccess _da;

        public TexasWellData()
        {
            _da = new DbaseDataAccess();
            _wbl = new WellboreDataLocationsDbase(_da);
            _wbi = new WellboreDataInfoDbase(_da);
        }

        public async Task<List<Wellbore>> GetTexasWells(InputData input)
        {
            List<Wellbore> wellbores = new List<Wellbore>();
            string connectionString = input.Path + @"\well" + input.CountyCode;
            wellbores = await _wbl.ReadWellbores(connectionString);

            List<Wellbore> wellinfo = new List<Wellbore>();
            connectionString = input.Path + @"\api" + input.CountyCode;
            wellinfo = await _wbi.ReadWellbores(connectionString);

            int dups = 0;
            int noMatches = 0;
            foreach (var well in wellbores)
            {
                var info = wellinfo.Where(w => w.UWI == well.UWI).ToList();
                if (info == null || info.Count == 0)
                {
                    noMatches++;
                    //Console.WriteLine($"No match for {well.UWI}");
                }
                else if (info.Count > 0)
                {
                    if (info.Count > 1) dups++;
                    well.OPERATOR = info[0].OPERATOR;
                    well.FINAL_TD = info[0].FINAL_TD;
                    well.LEASE_NAME = info[0].LEASE_NAME;
                    well.ASSIGNED_FIELD = info[0].ASSIGNED_FIELD;
                }
            }
            Console.WriteLine($"Number of duplicate matches {dups}");
            Console.WriteLine($"Number of wells with no matches {noMatches}");
            return wellbores;
        }
    }
}
