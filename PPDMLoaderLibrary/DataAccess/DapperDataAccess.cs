﻿using Dapper;
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

        public async Task SaveData<T>(string connectionString, T parameters, string sql)
        {
            using IDbConnection cnn = new SqlConnection(connectionString);
            await cnn.ExecuteAsync(sql, parameters);
        }
    }
}
