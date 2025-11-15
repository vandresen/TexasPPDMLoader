using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPDMLoaderLibrary.Models
{
    public class Casing
    {
        public string UWI { get; set; } = "";
        public string SOURCE { get; set; } = "RRC";
        public string TUBING_TYPE { get; set; } = "CASING";
        public int TUBING_OBS_NO { get; set; }
        public double OUTSIDE_DIAMETER { get; set; }
        public string OUTSIDE_DIAMETER_OUOM { get; set; } = "in";
        public double TUBING_WEIGHT { get; set; }
        public string TUBING_WEIGHT_OUOM { get; set; } = "lb/ft";
        public double SHOE_DEPTH { get; set; }
        public string SHOE_DEPTH_OUOM { get; set; } = "ft";
        public double HOLE_SIZE { get; set; }
        public string HOLE_SIZE_OUOM { get; set; } = "in";
        public double LEFT_IN_HOLE_LENGTH { get; set; }
        public string LEFT_IN_HOLE_LENGTH_OUOM { get; set; } = "ft";
        public string REMARK { get; set; } = "";
        public string ACTIVE_IND { get; set; } = "Y";
    }
}
