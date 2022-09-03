using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TexasPPDMLoader.Models
{
    public class Wellbore
    {
        public string UWI { get; set; }
        public double SURFACE_LONGITUDE { get; set; }
        public double SURFACE_LATITUDE { get; set; }
        public double BOTTOM_HOLE_LATITUDE { get; set; }
        public double BOTTOM_HOLE_LONGITUDE { get; set; }
    }
}
