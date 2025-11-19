using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPDMLoaderLibrary.Models
{
    public class LoaderOptions
    {
        public string Path { get; set; } = @"C:\temp";
        public string CountyCode { get; set; }
        public string? ConnectionString { get; set; } = "";
    }
}
