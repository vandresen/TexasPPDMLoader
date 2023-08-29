using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPDMLoaderLibrary.Models
{
    public class Formations
    {
        public string UWI { get; set; }
        public string STRAT_UNIT_ID { get; set; }
        public double? PICK_DEPTH { get; set; }

        public override int GetHashCode()
        {
            return Tuple.Create(UWI, STRAT_UNIT_ID, PICK_DEPTH).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is Formations other)
            {
                return UWI.Equals(other.UWI) &&
                       STRAT_UNIT_ID.Equals(other.STRAT_UNIT_ID) &&
                       PICK_DEPTH.Equals(other.PICK_DEPTH);
            }
            return false;
        }
    }
}
