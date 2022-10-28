using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPDMLoaderLibrary.Models
{
    public class BottomData
    {
        public int BottomId { get; set; }
        public int SurfaceId { get; set; }
        public int SymbolNumber { get; set; }
        public string ApiNumber { get; set; }
        public string Reliable { get; set; }
        public string Api10 { get; set; }
        public string Api { get; set; }
        public double Long27 { get; set; }
        public double Lat27 { get; set; }
        public double Lon83 { get; set; }
        public double Lat83 { get; set; }
        public string OutFips { get; set; }
        public string WellNumber { get; set; }
        public string Radioactive { get; set; }
        public string WellId { get; set; }
        public string Sidetrack { get; set; }
    }
}
