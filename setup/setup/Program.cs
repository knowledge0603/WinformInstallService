using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;

namespace setup
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string currentDir = System.AppDomain.CurrentDomain.BaseDirectory;
            if (!Directory.Exists(currentDir + "baseDir"))
            {
                Application.Run(new start());
            }
            if (Directory.Exists(currentDir + "baseDir"))
            {
                Application.Run(new menu());
            }
            
        }
    }
}
