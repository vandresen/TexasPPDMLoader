using PPDMLoaderLibrary.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        public static DateTime? GetDateFromString(this string token)
        {
            CultureInfo provider = new CultureInfo("en-US");
            DateTime? dateTime = null;
            if (!string.IsNullOrWhiteSpace(token))
            {
                DateTime value;
                if (DateTime.TryParseExact(token, "yyyyMMdd", provider, DateTimeStyles.None, out value))
                {
                    dateTime = value;
                } 
            }
            return dateTime;
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
