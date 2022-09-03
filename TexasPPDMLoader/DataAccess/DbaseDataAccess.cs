using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasPPDMLoader.DataAccess
{
    public class DbaseDataAccess : IDataAccess
    {
        public DbaseDataAccess()
        {
        }

        public Task<IEnumerable<T>> ReadData<T>(string connectionString)
        {
            throw new NotImplementedException();
        }

        public Task SaveData<T>(string connectionString, T data)
        {
            throw new NotImplementedException();
        }
    }
}
