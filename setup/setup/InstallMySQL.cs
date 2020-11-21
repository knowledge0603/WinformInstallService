using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace setup
{
    class InstallMySQL
    {
        #region Declarative region
        //Declares a delegate that returns the end information to the main thread
        public delegate void SendEndMessage(string mes);
        public SendEndMessage sendEndMes;
        string currentDir = System.AppDomain.CurrentDomain.BaseDirectory;
        #endregion

        #region install mysql service  Load 
        /// <summary>
        /// load
        /// </summary>
        public void Load(object obj)
        {
            //install mysql service and  start mysql service 
            Process proc = new Process();
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();
            proc.StandardInput.WriteLine("cd " + currentDir + "baseDir\\mysql-8.0.22-winx64\\");
            proc.StandardInput.WriteLine("mysql_init_db.bat ");
            proc.StandardInput.WriteLine("exit");
            proc.StandardOutput.ReadToEnd();
            proc.Close();
        }
        #endregion
    }
}
