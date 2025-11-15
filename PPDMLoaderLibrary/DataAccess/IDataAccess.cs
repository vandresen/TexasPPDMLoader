using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPDMLoaderLibrary.DataAccess
{
    public interface IDataAccess
    {
        Task SaveData<T>(string connectionString, string sql, T? data = default);
        Task<IEnumerable<T>> ReadData<T>(string connectionString);
    }
}
