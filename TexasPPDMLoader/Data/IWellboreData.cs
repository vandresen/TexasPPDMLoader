using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TexasPPDMLoader.Models;

namespace TexasPPDMLoader.Data
{
    public interface IWellboreData
    {
        Task SaveWellbores(List<Wellbore> wellbores, string connectionString);
        Task<List<Wellbore>> ReadWellbores(string connectionString);
    }
}
