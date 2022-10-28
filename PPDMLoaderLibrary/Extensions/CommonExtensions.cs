using PPDMLoaderLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPDMLoaderLibrary.Extensions
{
    public static class CommonExtensions
    {
        public static double GetDoubleFromString(this string token)
        {
            double number = -99999.0;
            if (!string.IsNullOrWhiteSpace(token))
            {
                double value;
                if (double.TryParse(token, out value)) number = value;
            }
            return number;
        }

        public static List<ReferenceData> CreateReferenceDataObject(this List<string> refValues)
        {
            List<ReferenceData> refs = new List<ReferenceData>();
            foreach (var value in refValues)
            {
                ReferenceData refData = new ReferenceData() { Reference = value };
                refs.Add(refData);
            }
            return refs;
        }
    }
}
