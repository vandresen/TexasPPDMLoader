using DbfDataReader;
using PPDMLoaderLibrary.DataAccess;
using PPDMLoaderLibrary.Extensions;
using PPDMLoaderLibrary.Models;
using System.Text;

namespace PPDMLoaderLibrary.Data
{
    public class WellboreDataInfoDbase : IWellboreData
    {
        private readonly IDataAccess _da;

        public WellboreDataInfoDbase(IDataAccess da)
        {
            _da = da;
        }

        public async Task<List<Wellbore>> ReadWellbores(string connectionString)
        {
            List<Wellbore> wellbores = new List<Wellbore>();

            string file = connectionString + ".dbf";
            List<WellHeaderData> wellHeaders = getHeaderData(file);
            Console.WriteLine($"Number of well info found is {wellHeaders.Count}");
            int refWells = 0;

            foreach (var well in wellHeaders)
            {
                int uwiLength = well.API_NUM.Length;
                //if(well.REFER_TO_API != "00000000")
                //{
                //    refWells++;
                //    //Console.WriteLine($"{well.API_NUM} is refered to {well.REFER_TO_API}");
                //}
                if (uwiLength == 8)
                {
                    Wellbore wellbore = new Wellbore()
                    {
                        UWI = "42" + well.API_NUM + "00",
                        LEASE_NAME = well.LEASE_NAME,
                        OPERATOR = well.OPERATOR,
                        ASSIGNED_FIELD = well.FIELD_NAME,
                        FINAL_TD = well.TOTAL_DEPTH,
                        WELL_NUM = well.WELL_NUMBER,
                        COMPLETION_DATE = well.COMPLETION_DATE,
                    };
                    wellbores.Add(wellbore);
                }
                else
                {
                    Console.WriteLine($"Bad api number {well.API_NUM}");
                }
            }
            Console.WriteLine($"Number of reference wells that has been ignored: {refWells}");
            return wellbores;
        }

        public async Task<List<WellHeaderData>> ReadWellInfo(string connectionString)
        {
            string file = connectionString + ".dbf";
            List<WellHeaderData> wellHeaders = getHeaderData(file);
            return wellHeaders;
        }

        public Task SaveFormations(List<Formations> formations, string connectionString)
        {
            throw new NotImplementedException();
        }

        public Task SaveWellbores(List<Wellbore> wellbores, string connectionString)
        {
            throw new NotImplementedException();
        }

        private List<WellHeaderData> getHeaderData(string file)
        {
            if (!File.Exists(file))
            {
                Exception error = new Exception($"The surface bottom file does not exists.");
                throw error;
            }
            var skipDeleted = true;
            List<WellHeaderData> headers = new List<WellHeaderData>();
            using (var dbfTable = new DbfTable(file, Encoding.UTF8))
            {
                var dbfRecord = new DbfRecord(dbfTable);
                while (dbfTable.Read(dbfRecord))
                {
                    if (skipDeleted && dbfRecord.IsDeleted)
                    {
                        continue;
                    }
                    string abstractName = (string)dbfRecord.GetValue(0);
                    string apiNum = (string)dbfRecord.GetValue(1);
                    string block = (string)dbfRecord.GetValue(2);
                    string completion = (string)dbfRecord.GetValue(3);
                    DateTime? completionDate = completion.GetDateFromString();
                    string fieldName = (string)dbfRecord.GetValue(4);
                    string leaseName = (string)dbfRecord.GetValue(5);
                    string gasRrcid = (string)dbfRecord.GetValue(6);
                    string oilGasCode = (string)dbfRecord.GetValue(7);
                    string onOffSch = (string)dbfRecord.GetValue(8);
                    string operatorName = (string)dbfRecord.GetValue(9);
                    string permitNum = (string)dbfRecord.GetValue(10);
                    string plugDate = (string)dbfRecord.GetValue(11);
                    string referToApi = (string)dbfRecord.GetValue(12);
                    string section = (string)dbfRecord.GetValue(13);
                    string survey = (string)dbfRecord.GetValue(14);
                    string strTotalDepth = (string)dbfRecord.GetValue(15);
                    double totalDept = strTotalDepth.GetDoubleFromString();
                    string wellid = (string)dbfRecord.GetValue(16);
                    string quadNum = (string)(dbfRecord.GetValue(17));   
                    WellHeaderData head = new WellHeaderData()
                    {
                        ABSTRACT = abstractName,
                        API_NUM = apiNum,
                        BLOCK = block,
                        COMPLETION_DATE = completionDate,
                        FIELD_NAME = fieldName,
                        LEASE_NAME = leaseName,
                        GAS_RRCID = gasRrcid,
                        OIL_GAS_CODE = oilGasCode,
                        OPERATOR = operatorName,
                        PERMIT_NUM = permitNum,
                        PLUG_DATE = plugDate,
                        REFER_TO_API = referToApi,
                        SECTION = section,
                        SURVEY = survey,
                        TOTAL_DEPTH = totalDept,
                        WELL_NUMBER = wellid,
                        QUAD_NUM = quadNum,
                        
                    };
                    headers.Add(head);
                }
            }
            return headers;
        }
    }
}
