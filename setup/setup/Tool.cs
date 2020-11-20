using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Xml;
using System.IO;
using System.Collections.Specialized;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Data.OleDb;

namespace setup
{
    class Tool
    {
        #region 定义区
        // private static string strCnn = @"Data Source=YZT2;User ID=tdly;Password=123456";
        #endregion


        #region 日期转换
        private static string getLink(string str, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return str + "=to_date('" + value + "','YYYY-MM-DD HH24:MI:SS')";
            }
            return "";
        }
        #endregion




        #region 写log

        public static void WriteLogToFile1(string msg)
        {
            string strDic = System.AppDomain.CurrentDomain.BaseDirectory;
            IniConfig readIdent = new IniConfig();
            readIdent.LoadFromFile(strDic + "service.ini");
            string logFile = readIdent.ReadIdent("configSet", "selectLogFilePath", "");
            System.DateTime currentTime = System.DateTime.Now;

            if (!Directory.Exists(logFile+ "\\logFiles\\" + DateTime.Now.ToString("yyyy-MM-dd")))  //不存在则创建
            {
                Directory.CreateDirectory(logFile + "\\logFiles\\" + DateTime.Now.ToString("yyyy-MM-dd"));
            }
            string logPath = logFile + "\\logFiles\\"+ DateTime.Now.ToString("yyyy-MM-dd")+"\\IMPORT_DTJC_LOG_" + currentTime.ToLongDateString() + ".txt";
            try
            {
                using (StreamWriter sw = File.AppendText(logPath))
                {
                    sw.WriteLine("["+ currentTime.ToString()+ "]"+ msg);
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                }
            }
            catch (IOException e)
            {
                using (StreamWriter sw = File.AppendText(logPath))
                {
                    sw.WriteLine("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")+"]"+ e.Message);
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                }
            }
        }
        #endregion



    }
}
