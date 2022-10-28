using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPDMLoaderLibrary.Models
{
    public class SurfaceData
    {
        public int SurfaceId { get; set; }
        public int SymbolNumber { get; set; }
        public string Api { get; set; }
        public string Reliable { get; set; }
        public double Lon27 { get; set; }
        public double Lat27 { get; set; }
        public double Lon83 { get; set; }
        public double Lat83 { get; set; }
        public string WellId { get; set; }
    }
}
