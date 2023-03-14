using DbfDataReader;
using PPDMLoaderLibrary.DataAccess;
using PPDMLoaderLibrary.Models;
using System.Text;

namespace TexasPPDMLoader.Data
{
    public class WellboreDataLocationsDbase : IWellboreData
    {
        private readonly IDataAccess _da;

        public WellboreDataLocationsDbase(IDataAccess da)
        {
            _da = da;
        }

        public async Task<List<Wellbore>> ReadWellbores(string connectionString)
        {
            List<Wellbore> wellbores = new List<Wellbore>();

            string file = connectionString + "l.dbf";
            List<SurfaceBottomLines> surfaceBottomLines = getLineData(file);
            Console.WriteLine($"Number of surface bottom lines found is {surfaceBottomLines.Count}");

            file = connectionString + "s.dbf";
            List<SurfaceData> surfaceData = getSurfaceData(file);
            Console.WriteLine($"Number of surface locations found is {surfaceData.Count}");

            file = connectionString + "b.dbf";
            List<BottomData> bottomWells = getBottomData(file);
            Console.WriteLine($"Number of bottom locations found is {bottomWells.Count}");

            foreach (var well in bottomWells)
            {
                var surf = surfaceData.Where(w => w.SurfaceId == well.SurfaceId).ToList();
                double surfLat = 0;
                double surfLon = 0;
                if (surf.Count == 0)
                {
                    surfLat = well.Lat27;
                    surfLon = well.Long27;
                }
                else
                {
                    surfLat = surf[0].Lat27;
                    surfLon = surf[0].Lon27;
                }
                int uwiLength = well.Api.Length;
                if(!String.IsNullOrEmpty(well.Sidetrack))
                {
                    //Console.WriteLine($"Sidetrack is {well.Sidetrack} and well number is {well.WellNumber}");
                }
                Wellbore wellbore = new Wellbore()
                {
                    SURFACE_LATITUDE = surfLat,
                    SURFACE_LONGITUDE = surfLon,
                    BOTTOM_HOLE_LATITUDE = well.Lat27,
                    BOTTOM_HOLE_LONGITUDE = well.Long27,
                    WELL_NUM = well.WellNumber
                };
                if (uwiLength == 8)
                {
                    wellbore.UWI = "42" + well.Api + "00";
                }
                else
                {
                    wellbore.UWI = "U42" + well.Api + "-" + well.BottomId.ToString();
                }
                wellbores.Add(wellbore);
            }

            foreach (var well in surfaceData)
            {
                var bott = bottomWells.Where(w => w.SurfaceId == well.SurfaceId).ToList();
                if(bott.Count == 0) 
                {
                    //Console.WriteLine($"No bottom for {well.SurfaceId}");
                    double surfLat = well.Lat27;
                    double surfLon = well.Lon27;
                    Wellbore wellbore = new Wellbore()
                    {
                        SURFACE_LATITUDE = surfLat,
                        SURFACE_LONGITUDE = surfLon,
                        BOTTOM_HOLE_LATITUDE = well.Lat27,
                        BOTTOM_HOLE_LONGITUDE = well.Lon27
                    };
                    int uwiLength = well.Api.Length;
                    if (uwiLength == 8)
                    {
                        wellbore.UWI = "42" + well.Api + "00";
                    }
                    else
                    {
                        wellbore.UWI = "U" + well.Api + "-" + well.SurfaceId.ToString();
                    }
                    wellbores.Add(wellbore);
                }
            }
            
            return wellbores;
        }

        public Task SaveWellbores(List<Wellbore> wellbores, string connectionString)
        {
            throw new NotImplementedException();
        }

        private List<BottomData> getBottomData(string bottomFile)
        {
            if (!File.Exists(bottomFile))
            {
                Exception error = new Exception($"The bottom file does not exists.");
                throw error;
            }
            var skipDeleted = true;
            List<BottomData> bottomWells = new List<BottomData>();
            using (var dbfTable = new DbfTable(bottomFile, Encoding.UTF8))
            {
                var dbfRecord = new DbfRecord(dbfTable);
                while (dbfTable.Read(dbfRecord))
                {
                    if (skipDeleted && dbfRecord.IsDeleted)
                    {
                        continue;
                    }
                    int bottom = (int)(long)dbfRecord.GetValue(0);
                    int surface = (int)(long)dbfRecord.GetValue(1);
                    int symNum = (int)dbfRecord.GetValue(2);
                    string apiNumber = (string)dbfRecord.GetValue(3);
                    string reliable = (string)dbfRecord.GetValue(4);
                    string api10 = (string)dbfRecord.GetValue(5);
                    string api = (string)dbfRecord.GetValue(6);
                    double long27 = (double)(decimal)dbfRecord.GetValue(7);
                    double lat27 = (double)(decimal)dbfRecord.GetValue(8);
                    double long83 = (double)(decimal)dbfRecord.GetValue(9);
                    double lat83 = (double)(decimal)dbfRecord.GetValue(10);
                    string outFips = (string)dbfRecord.GetValue(11);
                    string wellNumber = (string)dbfRecord.GetValue(12);
                    string radioactive = (string)dbfRecord.GetValue(13);
                    string wellId = (string)dbfRecord.GetValue(14);
                    string sidetrack = (string)dbfRecord.GetValue(15);
                    BottomData bot = new BottomData()
                    {
                        BottomId = bottom,
                        SurfaceId = surface,
                        SymbolNumber = symNum,
                        ApiNumber = apiNumber,
                        Reliable = reliable,
                        Api10 = api10,
                        Api = api,
                        Long27 = long27,
                        Lat27 = lat27,
                        Lon83 = long83,
                        Lat83 = lat83,
                        OutFips = outFips,
                        WellNumber = wellNumber,
                        Radioactive = radioactive,
                        WellId = wellId,
                        Sidetrack = sidetrack
                    };
                    bottomWells.Add(bot);
                }
            }
            return bottomWells;
        }

        private List<SurfaceData> getSurfaceData(string file)
        {
            if (!File.Exists(file))
            {
                Exception error = new Exception($"The surface file does not exists.");
                throw error;
            }
            var skipDeleted = true;
            List<SurfaceData> surfaceWells = new List<SurfaceData>();
            using (var dbfTable = new DbfTable(file, Encoding.UTF8))
            {
                var dbfRecord = new DbfRecord(dbfTable);
                while (dbfTable.Read(dbfRecord))
                {
                    if (skipDeleted && dbfRecord.IsDeleted)
                    {
                        continue;
                    }
                    int surface = (int)(long)dbfRecord.GetValue(0);
                    int symNum = (int)dbfRecord.GetValue(1);
                    string api = (string)dbfRecord.GetValue(2);
                    string reliable = (string)dbfRecord.GetValue(3);
                    double lon27 = (double)(decimal)dbfRecord.GetValue(4);
                    double lat27 = (double)(decimal)dbfRecord.GetValue(5);
                    double lon83 = (double)(decimal)dbfRecord.GetValue(6);
                    double lat83 = (double)(decimal)dbfRecord.GetValue(7);
                    string wellId = (string)dbfRecord.GetValue(8);
                    SurfaceData sur = new SurfaceData()
                    {
                        SurfaceId = surface,
                        SymbolNumber = symNum,
                        Api = api,
                        Reliable = reliable,
                        Lon27 = lon27,
                        Lat27 = lat27,
                        Lon83 = lon83,
                        Lat83 = lat83,
                        WellId = wellId
                    };
                    surfaceWells.Add(sur);
                }
            }
            return surfaceWells;
        }

        private List<SurfaceBottomLines> getLineData(string file)
        {
            List<SurfaceBottomLines> lines = new List<SurfaceBottomLines>();
            if (!File.Exists(file))
            {
                return lines;
            }
            var skipDeleted = true;
            
            using (var dbfTable = new DbfTable(file, Encoding.UTF8))
            {
                var dbfRecord = new DbfRecord(dbfTable);
                while (dbfTable.Read(dbfRecord))
                {
                    if (skipDeleted && dbfRecord.IsDeleted)
                    {
                        continue;
                    }
                    int bottom = (int)(long)dbfRecord.GetValue(0);
                    int surface = (int)(long)dbfRecord.GetValue(1);
                    string api10 = (string)dbfRecord.GetValue(2);
                    string api = (string)dbfRecord.GetValue(3);
                    string sideTrack = (string)dbfRecord.GetValue(4);
                    SurfaceBottomLines sbl = new SurfaceBottomLines()
                    {
                        BottomId = bottom,
                        SurfaceId = surface,
                        Api10 = api10,
                        Api = api,
                        SideTrack = sideTrack,
                    };
                    lines.Add(sbl);
                }
            }
            return lines;
        }

        public Task<List<WellHeaderData>> ReadWellInfo(string connectionString)
        {
            throw new NotImplementedException();
        }
    }
}
