using System;

namespace Cofradinn.Modules.Utilities
{
    public static class StringBuilder
    {
        /// <summary>
        /// Return format: DDMMYYYYHHMMSS_FileName
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="fileName"></param>
        /// <returns>format: DDMMYYYYHHMMSS_FileName </returns>
        public static string BuildFileName(DateTime dateTime, string fileName)
        {
            string date = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
            string time = DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            string name = date + time + "_" + fileName;

            return name;
        }
    }
}