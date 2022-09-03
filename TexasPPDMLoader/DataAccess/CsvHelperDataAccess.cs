using CsvHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasPPDMLoader.DataAccess
{
    public class CsvHelperDataAccess : IDataAccess
    {
        public Task<IEnumerable<T>> ReadData<T>(string connectionString)
        {
            throw new NotImplementedException();
        }

        public async Task SaveData<T>(string connectionString, T data)
        {
            using (var writer = new StreamWriter(connectionString))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords((IEnumerable)data);
            }
        }
    }
}
