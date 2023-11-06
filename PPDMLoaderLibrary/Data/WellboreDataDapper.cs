using PPDMLoaderLibrary.DataAccess;
using PPDMLoaderLibrary.Extensions;
using PPDMLoaderLibrary.Models;

namespace PPDMLoaderLibrary.Data
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
            wellbores.Where(c => string.IsNullOrEmpty(c.OPERATOR)).Select(c => { c.OPERATOR = "UNKNOWN"; return c; }).ToList();
            wellbores.Where(c => string.IsNullOrEmpty(c.ASSIGNED_FIELD)).Select(c => { c.ASSIGNED_FIELD = "UNKNOWN"; return c; }).ToList();
            wellbores.Where(c => string.IsNullOrEmpty(c.DEPTH_DATUM)).Select(c => { c.DEPTH_DATUM = "UNKNOWN"; return c; }).ToList();
            wellbores.Where(c => string.IsNullOrEmpty(c.CURRENT_STATUS)).Select(c => { c.CURRENT_STATUS = "UNKNOWN"; return c; }).ToList();
            await SaveWellboreRefData(wellbores, connectionString);
            string sql = "IF NOT EXISTS(SELECT 1 FROM WELL WHERE UWI = @UWI) " +
                "INSERT INTO WELL (UWI, SURFACE_LONGITUDE, SURFACE_LATITUDE, BOTTOM_HOLE_LATITUDE, BOTTOM_HOLE_LONGITUDE, " +
                "FINAL_TD, OPERATOR, ASSIGNED_FIELD, LEASE_NAME, WELL_NUM, COMPLETION_DATE, DEPTH_DATUM_ELEV, DEPTH_DATUM, " +
                "CURRENT_STATUS) " +
                "VALUES(@UWI, @SURFACE_LONGITUDE, @SURFACE_LATITUDE, @BOTTOM_HOLE_LATITUDE, @BOTTOM_HOLE_LONGITUDE, " +
                "@FINAL_TD, @OPERATOR, @ASSIGNED_FIELD, @LEASE_NAME, @WELL_NUM, @COMPLETION_DATE, " +
                "@DEPTH_DATUM_ELEV, @DEPTH_DATUM, @CURRENT_STATUS)";
            await _da.SaveData(connectionString, wellbores,sql);
        }

        public async Task SaveWellboreRefData(List<Wellbore> wellbores, string connectionString)
        {
            Dictionary<string, List<ReferenceData>> refDict = new Dictionary<string, List<ReferenceData>>();
            ReferenceTables tables = new ReferenceTables();

            List<ReferenceData> refs = wellbores.Select(x => x.OPERATOR).Distinct().ToList().CreateReferenceDataObject();
            refDict.Add(tables.RefTables[0].Table, refs);
            refs = wellbores.Select(x => x.ASSIGNED_FIELD).Distinct().ToList().CreateReferenceDataObject();
            refDict.Add(tables.RefTables[1].Table, refs);
            refs = wellbores.Select(x => x.DEPTH_DATUM).Distinct().ToList().CreateReferenceDataObject();
            refDict.Add(tables.RefTables[2].Table, refs);
            refs = wellbores.Select(x => x.CURRENT_STATUS).Distinct().ToList().CreateReferenceDataObject();
            refDict.Add(tables.RefTables[3].Table, refs);
            foreach (var table in tables.RefTables)
            {
                refs = refDict[table.Table];
                string sql = "";
                if (table.Table == "R_WELL_STATUS")
                {
                    sql = $"IF NOT EXISTS(SELECT 1 FROM {table.Table} WHERE {table.KeyAttribute} = @Reference) " +
                $"INSERT INTO {table.Table} " +
                $"(STATUS_TYPE, {table.KeyAttribute}, {table.ValueAttribute}) " +
                $"VALUES('STATUS', @Reference, @Reference)";
                }
                else
                {
                    sql = $"IF NOT EXISTS(SELECT 1 FROM {table.Table} WHERE {table.KeyAttribute} = @Reference) " +
                $"INSERT INTO {table.Table} " +
                $"({table.KeyAttribute}, {table.ValueAttribute}) " +
                $"VALUES(@Reference, @Reference)";
                }
                await _da.SaveData(connectionString, refs, sql);
            }
        }

        public Task<List<WellHeaderData>> ReadWellInfo(string connectionString)
        {
            throw new NotImplementedException();
        }

        public async Task SaveFormations(List<Formations> formations, string connectionString)
        {
            await SaveFormationsRefData(formations, connectionString);
            string sql = "IF NOT EXISTS" +
                "(SELECT 1 FROM STRAT_WELL_SECTION WHERE UWI = @UWI AND STRAT_NAME_SET_ID = 'UNKNOWN' AND STRAT_UNIT_ID = @STRAT_UNIT_ID) " +
                "INSERT INTO STRAT_WELL_SECTION (UWI, STRAT_NAME_SET_ID, STRAT_UNIT_ID, PICK_DEPTH, INTERP_ID) " +
                "VALUES(@UWI, 'UNKNOWN', @STRAT_UNIT_ID, @PICK_DEPTH, 'UNKNOWN')";
            await _da.SaveData(connectionString, formations, sql);
        }

        public async Task SaveFormationsRefData(List<Formations> wellbores, string connectionString)
        {
            List<ReferenceData> refs = wellbores.Select(x => x.STRAT_UNIT_ID).Distinct().ToList().CreateReferenceDataObject();

            string sql = $"IF NOT EXISTS(SELECT 1 FROM STRAT_NAME_SET WHERE STRAT_NAME_SET_ID = 'UNKNOWN') " +
                $"INSERT INTO STRAT_NAME_SET " +
                $"(STRAT_NAME_SET_ID, STRAT_NAME_SET_NAME) " +
                $"VALUES('UNKNOWN', 'UNKNOWN')";
            await _da.SaveData(connectionString, new { }, sql);

            sql = $"IF NOT EXISTS(SELECT 1 FROM STRAT_UNIT WHERE STRAT_UNIT_ID = @Reference) " +
                $"INSERT INTO STRAT_UNIT " +
                $"(STRAT_NAME_SET_ID, STRAT_UNIT_ID, LONG_NAME) " +
                $"VALUES('UNKNOWN', @Reference, @Reference)";
            await _da.SaveData(connectionString, refs, sql);
        }

        public async Task SavePerforations(List<Perforation> perfs, string connectionString)
        {
            string sql = "IF NOT EXISTS" +
                "(SELECT 1 FROM WELL_PERFORATION WHERE UWI = @UWI AND PERFORATION_OBS_NO = @PERFORATION_OBS_NO) " +
                "INSERT INTO WELL_PERFORATION (UWI, SOURCE, PERFORATION_OBS_NO, BASE_DEPTH, TOP_DEPTH) " +
                "VALUES(@UWI, 'UNKNOWN', @PERFORATION_OBS_NO, @BASE_DEPTH, @TOP_DEPTH)";
            await _da.SaveData(connectionString, perfs, sql);
        }
    }
}
