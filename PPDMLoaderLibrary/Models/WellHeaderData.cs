using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace PPDMLoaderLibrary.Models
{
    public class WellHeaderData
    {
        public string QUAD_NUM { get; set; }
        public string API_NUM { get; set; }
        public string SURVEY { get; set; }
        public string BLOCK { get; set; }
        public string SECTION { get; set; }
        public string ABSTRACT { get; set; }
        public string OPERATOR { get; set; }
        public double TOTAL_DEPTH { get; set; }
        public string WELL_NUMBER { get; set; }
        public string LEASE_NAME { get; set; }
        public string PERMIT_NUM { get; set; }
        public string GAS_RRCID { get; set; }
        public string FIELD_NAME { get; set; }
        public DateTime? COMPLETION_DATE { get; set; }
        public string PLUG_DATE { get; set; }
        public string REFER_TO_API { get; set; }
        public string ON_OFF_SCHEDULE { get; set; }
        public string OIL_GAS_CODE { get; set; }
    }
}
