using AppWorks.Utilities;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AppWorks.UI.Common
{
    /// <summary>
    /// Class for common settings.
    /// </summary>
    public sealed class CommonSettings
    {
        public static LogManager logger = new LogManager();

        public static string GetDateValue(string Textvalue, int Index)
        {
            int Year = DateTime.Now.Year;
            int Month = DateTime.Now.Month;
            int Day = DateTime.Now.Day;
            if (Index > 2 && Index < 5 && !Textvalue.Contains("/"))
            {
                Textvalue=Regex.Replace(Textvalue, ".{2}", "$0/");
            }
            string[] value = Textvalue.ToString().Split('/');
            string GetDate = string.Empty;

           
            if (Index <= 2)
            {
                if (Convert.ToInt32(value[0]) <= 31)
                {
                    GetDate = Month.ToString() + "/" + value[0] + "/" + Year.ToString();
                }

            }
            else if (Index >2  && Index<=5)
            {
                if (Convert.ToInt32(value[1]) <= 31 && Convert.ToInt32(value[0]) <= 12)
                {
                    GetDate = value[0] + "/" + value[1] + "/" + Year.ToString();
                }
                else
                {
                    GetDate = value[1] + "/" + value[0] + "/" + Year.ToString();
                }

            }
            else
            {
                GetDate = value[0] + "/" + value[1] + "/" + value[2];
            }
            return GetDate;
        }
    }

  
}
