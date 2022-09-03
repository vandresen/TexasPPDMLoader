using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasPPDMLoader.DataAccess
{
    public interface IDataAccess
    {
        Task SaveData<T>(string connectionString, T data);
        Task<IEnumerable<T>> ReadData<T>(string connectionString);
    }
}
