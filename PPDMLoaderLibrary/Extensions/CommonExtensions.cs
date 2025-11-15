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

        public static decimal GetDecimalFromString(this string token)
        {
            decimal number = -99999.0m;
            if (!string.IsNullOrWhiteSpace(token))
            {
                decimal value;
                if (decimal.TryParse(token, out value)) number = value;
            }
            return number;
        }

        public static int GetIntlFromString(this string token)
        {
            int number = -99999;
            if (!string.IsNullOrWhiteSpace(token))
            {
                int value;
                if (int.TryParse(token, out value)) number = value;
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

        public static int ParseInt(this string s, int start, int length)
        {
            var part = SafeSubstring(s, start - 1, length).Trim();
            return int.TryParse(part, out var val) ? val : 0;
        }

        public static double ParseCasingDiameter(this string s, int inchStart, int inchLen, int fracNumStart, int fracNumLen, int fracDenStart, int fracDenLen)
        {
            var inch = ParseInt(s, inchStart, inchLen);
            var num = ParseInt(s, fracNumStart, fracNumLen);
            var den = ParseInt(s, fracDenStart, fracDenLen);
            return inch + (den > 0 ? (double)num / den : 0.0);
        }

        public static double ParseCasingWeight(this string s, int wholeStart, int wholeLen, int tenthsStart, int tenthsLen)
        {
            var whole = ParseInt(s, wholeStart, wholeLen);
            var tenths = ParseInt(s, tenthsStart, tenthsLen);
            return whole + tenths / 10.0;
        }

        private static string SafeSubstring(string s, int start, int length)
        {
            if (start >= s.Length) return string.Empty;
            return s.Substring(start, Math.Min(length, s.Length - start));
        }
    }
}
