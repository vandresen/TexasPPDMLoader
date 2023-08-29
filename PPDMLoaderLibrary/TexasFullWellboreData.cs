using PPDMLoaderLibrary.Extensions;
using PPDMLoaderLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPDMLoaderLibrary
{
    public class TexasFullWellboreData
    {
        private int[] rootWidths;
        private int[] locationWidths;
        private int[] rootPos = {1, 3, 6, 11, 13, 15, 17, 20, 21, 23, 25, 27, 29, 34, 39, 41, 43, 45, 47, 49, 51,
            53, 55, 56, 57, 65, 73, 81, 87, 88, 89, 90, 91, 92, 100, 101, 102, 103, 105, 106, 108, 110, 112, 114,
            116, 118, 120, 122, 124, 126, 128, 130, 132, 133, 139, 146, 152, 158, 159, 160, 161, 168 };
        int[] locationPos = { 1, 3, 6, 12, 67, 77, 85, 89, 95, 101, 114, 120, 133, 143, 153, 158, 160, 170, 178, 179 };
        private string uwi = "";
        private Formations formation;
        private HashSet<Formations> formations = new HashSet<Formations>();

        public TexasFullWellboreData()
        {
            rootWidths = new int[rootPos.Length - 1];
            locationWidths = new int[locationPos.Length - 1];
        }

        public List<Wellbore> GetTexasFullWellboreData(InputData input)
        {
            List<Wellbore> result = new List<Wellbore>();
            string textFile = input.Path + @"\dbf900.txt";
            if (File.Exists(textFile))
            {
                List<Wellbore> wellbores = new List<Wellbore>();
                using (StreamReader file = new StreamReader(textFile))
                {
                    int counter = 0;
                    string ln;

                    for (int i = 1; i < rootPos.Length; i++)
                    {
                        rootWidths[i - 1] = rootPos[i] - rootPos[i - 1];
                    }

                    for (int i = 1; i < locationPos.Length; i++)
                    {
                        locationWidths[i - 1] = locationPos[i] - locationPos[i - 1];
                    }
                    Wellbore wb = new Wellbore();
                    while ((ln = file.ReadLine()) != null)
                    {

                        string recordKey = ln.Substring(0, 2);
                        //string uwi = "";
                        string completionDate = "";
                        string totalDepth = "";

                        // WELL-BORE-API-ROOT.
                        if (recordKey == "01")
                        {

                            string[] ret = ln.ParseString(rootWidths);
                            uwi = "42" + ret[1] + ret[2] + "00";
                            completionDate = ret[8] + ret[9] + ret[10] + ret[11];
                            totalDepth = ret[12];
                            if (wb.UWI != null)
                            {
                                wellbores.Add(wb);
                            }
                            wb = new Wellbore();
                            wb.UWI = uwi;
                            wb.FINAL_TD = totalDepth.GetDoubleFromString();
                            wb.COMPLETION_DATE = completionDate.GetDateFromString();
                        }
                        //WELL-BORE-COMPLETION-SEG
                        else if (recordKey == "02")
                        {
                            //Console.WriteLine(ln);
                        }
                        // WELL-BORE-FILE-DATE
                        else if (recordKey == "03")
                        {
                            string elevation = ln.Substring(85, 4);
                            string elevCode = ln.Substring(89, 2);
                            if (elevCode.Trim().Length == 0) elevCode = "UNKNOWN";
                            if (elevation != "0000")
                            {
                                wb.DEPTH_DATUM_ELEV = elevation.GetDoubleFromString();
                                wb.DEPTH_DATUM = elevCode;
                            }
                        }
                        //WELL-BORE-COMPL-RMKS-SEG.
                        else if (recordKey == "04")
                        {
                            //Console.WriteLine(ln);
                        }
                        // WELL-BORE-TUBING-SEG
                        else if (recordKey == "05")
                        {
                            //Console.WriteLine(ln);
                        }
                        // WELL-BORE-CASING-SEG.
                        else if (recordKey == "06")
                        {
                            //Console.WriteLine(ln);
                        }
                        // WELL-BORE-PERF-SEG.
                        else if (recordKey == "07")
                        {
                            //Console.WriteLine(ln);
                        }
                        // WELL-BORE-LINER-SEG.
                        else if (recordKey == "08")
                        {
                            //Console.WriteLine(ln);
                        }
                        // WELL-FORMATION-DATA-SEG.
                        else if (recordKey == "09")
                        {
                            //Console.WriteLine(ln);
                            string formationName = ln.Substring(5, 32);
                            string strDepth = ln.Substring(37, 5);
                            if (strDepth != "00000")
                            {
                                Formations formation = new Formations();
                                formation.PICK_DEPTH = strDepth.GetDoubleFromString();
                                formation.UWI = uwi;
                                formation.STRAT_UNIT_ID = formationName;
                                formations.Add(formation);
                            }
                        }
                        // WELL-SQUEEZE-DATA-SEG
                        else if (recordKey == "10")
                        {
                            //Console.WriteLine(ln);
                        }
                        // WELL-BORE-TWDB-SEG.
                        else if (recordKey == "11")
                        {
                            //Console.WriteLine(ln);
                        }
                        // WELL-BORE-OLD-LOCATION-SEG.
                        else if (recordKey == "12")
                        {
                            //Console.WriteLine(ln);
                        }
                        // WELL-BORE-NEW-LOC-SEG.
                        else if (recordKey == "13")
                        {
                            //string[] ret = Utilities.ParseString(locationWidths, ln);
                            //string county = ret[2];
                            //string survey = ret[3];
                            //string blockNumber = ret[4];
                            //string section = ret[5];
                            //Console.WriteLine($"{county}, {survey}, {blockNumber}, {section}, {ret[13]}, {ret[14]}, {ret[15]}, {ret[16]}, {ret[18]}");
                            //Console.WriteLine(ln);
                        }
                        // WELL-BORE-PLUGGING-SEG.
                        else if (recordKey == "14")
                        {
                            //Console.WriteLine(ln);
                        }
                        // WELL-BORE-PLUG-RMKS-SEG.
                        else if (recordKey == "15")
                        {
                            //Console.WriteLine(ln);
                        }
                        // WELL-BORE-PLUG-REC-SEG.
                        else if (recordKey == "16")
                        {
                            //Console.WriteLine(ln);
                        }
                        // WELL-BORE-PLUG-CAS-SEG.
                        else if (recordKey == "17")
                        {
                            //Console.WriteLine(ln);
                        }
                        // WELL-BORE-PLUG-RECORD.
                        else if (recordKey == "18")
                        {
                            //Console.WriteLine(ln);
                        }
                        // WELL-BORE-PLUG-NOMEN-SEG.
                        else if (recordKey == "19")
                        {
                            //Console.WriteLine(ln);
                        }
                        // WELL-BORE-DRLG-PMT-SEG
                        else if (recordKey == "20")
                        {
                            //Console.WriteLine(ln);
                        }
                        // WELL-BORE-WELL-ID-SEG.
                        else if (recordKey == "21")
                        {
                            //Console.WriteLine(ln);
                        }
                        else
                        {
                            //Console.WriteLine(ln);
                        }

                        counter++;
                    }
                    file.Close();
                    Console.WriteLine($"Full wellbore file has {counter} lines.");
                    result = wellbores.Where(x => x.UWI.Substring(2, 3) == input.CountyCode).ToList();
                }
            }
            else
            {
                Console.WriteLine($"Full wellbore file {textFile} does not exist");
            }
            return result;
        }

        public List<Formations> GetTexasFormationData(InputData input)
        {
            List<Formations> result = formations.Where(x => x.UWI.Substring(2, 3) == input.CountyCode).ToList();
            return result;
        }
    }
}
