using PPDMLoaderLibrary.Data;
using PPDMLoaderLibrary.DataAccess;
using PPDMLoaderLibrary.Models;

namespace PPDMLoaderLibrary
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
            string connectionString = input.Path + @"\extract\well" + input.CountyCode;
            wellbores = await _wbl.ReadWellbores(connectionString);

            List<WellHeaderData> wellinfo = new List<WellHeaderData>();
            connectionString = input.Path + @"\api" + input.CountyCode;
            wellinfo = await _wbi.ReadWellInfo(connectionString);

            int dups = 0;
            int noMatches = 0;
            int noLocations = 0;
            foreach (var well in wellbores)
            {
                string UWI = well.UWI.Substring(2, 8);
                var info = wellinfo.Where(w => w.API_NUM == UWI).ToList();
                if (info == null || info.Count == 0)
                {
                    noMatches++;
                    //Console.WriteLine($"No match for {well.UWI}");
                }
                else if (info.Count > 0)
                {
                    if (info.Count > 1) 
                    { 
                        dups++;
                        //Console.WriteLine($"Duplicates for {well.UWI}");
                        //foreach (var item in info)
                        //{
                        //    Console.WriteLine($"        Well number {item.WELL_NUM}");
                        //}
                    }
                    well.OPERATOR = info[0].OPERATOR;
                    well.FINAL_TD = info[0].TOTAL_DEPTH;
                    well.LEASE_NAME = info[0].LEASE_NAME;
                    well.ASSIGNED_FIELD = info[0].FIELD_NAME;
                    well.COMPLETION_DATE = info[0].COMPLETION_DATE;
                    well.WELL_NUM = info[0].WELL_NUMBER;
                }
            }

            // Insert wells that don't have any locations
            foreach (var item in wellinfo)
            {
                string UWI = "42" + item.API_NUM + "00";
                var wells = wellbores.Where(w => w.UWI == UWI).ToList();
                if (wells.Count == 0)
                {
                    if (item.REFER_TO_API == "00000000")
                    {
                        Wellbore wellbore = new Wellbore()
                        {
                            UWI = UWI,
                            OPERATOR = item.OPERATOR,
                            FINAL_TD = item.TOTAL_DEPTH,
                            LEASE_NAME = item.LEASE_NAME,
                            ASSIGNED_FIELD = item.FIELD_NAME,
                            COMPLETION_DATE= item.COMPLETION_DATE,
                            WELL_NUM = item.WELL_NUMBER
                        };
                        wellbores.Add(wellbore);
                        noLocations++;
                        //Console.WriteLine($"Well {item.API_NUM} does not have a location");
                    } 
                }
            }

            Console.WriteLine($"Number of duplicate matches {dups}");
            Console.WriteLine($"Number of wells with no matches {noMatches}");
            Console.WriteLine($"Number of wells with no locations {noLocations}");
            return wellbores;
        }
    }
}
