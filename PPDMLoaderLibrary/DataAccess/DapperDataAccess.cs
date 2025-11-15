using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPDMLoaderLibrary.DataAccess
{
    public class DapperDataAccess : IDataAccess
    {
        public DapperDataAccess()
        {

        }

        public Task<IEnumerable<T>> ReadData<T>(string connectionString)
        {
            throw new NotImplementedException();
        }

        public async Task SaveData<T>(string connectionString, string sql, T? parameters = default)
        {
            using IDbConnection cnn = new SqlConnection(connectionString);
            object param = parameters is null ? new { } : parameters!;
            await cnn.ExecuteAsync(sql, param);
        }
    }
}
