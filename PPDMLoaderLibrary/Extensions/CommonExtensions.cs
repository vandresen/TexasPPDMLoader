using PPDMLoaderLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPDMLoaderLibrary.Extensions
{
    public static class CommonExtensions
    {
        public static List<Wellbore> MergeWellboreObjects(this List<Wellbore> wells1, List<Wellbore> wells2)
        {
            List<Wellbore> result = new List<Wellbore>();
            foreach (var wellbore in wells1)
            {
                Wellbore well = wells2.FirstOrDefault(s => s.UWI == wellbore.UWI);
                if(well != null) 
                {
                    wellbore.COMPLETION_DATE = well.COMPLETION_DATE;
                    wellbore.DEPTH_DATUM = well.DEPTH_DATUM;
                    wellbore.DEPTH_DATUM_ELEV = well.DEPTH_DATUM_ELEV;
                    if (wellbore.FINAL_TD < 1) wellbore.FINAL_TD = well.FINAL_TD;
                }
                result.Add(wellbore);
            }

            foreach (var wellbore in wells2)
            {
                Wellbore well = wells1.FirstOrDefault(s => s.UWI == wellbore.UWI);
                if (well == null) 
                { 
                    result.Add(wellbore);
                }
            }
            return result;
        }
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

        public static string[] ParseString(this string ln, int[] widths)
        {
            string[] ret = new string[widths.Length];
            char[] c = ln.ToCharArray();
            int startPos = 0;
            for (int i = 0; i < widths.Length; i++)
            {
                int width = widths[i];
                ret[i] = new string(c.Skip(startPos).Take(width).ToArray<char>());
                startPos += width;
            }
            return ret;
        }

        public static string Truncate(this string value, int maxLength)
        {
            return value?.Substring(0, Math.Min(value.Length, maxLength));
        }
    }
}
