using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPDMLoaderLibrary.DataAccess
{
    public interface IFileStorageService
    {
        Task<string> ReadFile(string fileShare, string fileName);
    }
}
