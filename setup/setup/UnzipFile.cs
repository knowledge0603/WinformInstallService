using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.Collections;
using System.Diagnostics;
using System.ComponentModel;
using System.Configuration.Install;
using System.Data;
using System.Drawing;
using System.ServiceProcess;


namespace setup
{
    class UnzipFile
    {

        #region Declarative region
        //Declares a delegate that returns the end information to the main thread
        public delegate void SendEndMessage(string mes);
        public SendEndMessage sendEndMes;
        string currentDir = System.AppDomain.CurrentDomain.BaseDirectory;
        #endregion

        #region Unzip and initialize configuration
        /// <summary>
        /// load
        /// </summary>
        public void Load(object obj)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.CreateNoWindow = true;
            process.Start();
            process.StandardInput.WriteLine("7z.exe x baseDir.zip  -aoa");
            process.StandardInput.WriteLine("exit");
            process.StandardOutput.ReadToEnd();// Wait until the compression is complete before you can grab the compressed file
            process.Close();

            //View the system's 64-bit markup
            bool systemType = Environment.Is64BitOperatingSystem;

            //Move kafka file to the root directory of the disk. Kafka will boot in a path that is too long to boot
            string volume = System.Windows.Forms.Application.StartupPath.Substring(0, System.Windows.Forms.Application.StartupPath.IndexOf(":"));
            //Kafka does not support long path execution so do the following
            CopyDirectory(currentDir + "baseDir\\kafka", volume + ":\\kafka",true);
            //The Kafka JDK path cannot contain Chinese and Spaces, so do the following
            CopyDirectory(currentDir + "baseDir\\openjdk", volume + ":\\openjdk", true);
            if (!systemType)//32bit 
            {
                //The Kafka JDK path cannot contain Chinese and Spaces, so do the following
                CopyDirectory(currentDir + "baseDir\\jre32bit", volume + ":\\jre32bit", true);
            }
            ArrayList batPathArray = new ArrayList();
            batPathArray.Add(volume + ":\\kafka\\bin\\windows\\" + "kafka-run-class.bat");
            batPathArray.Add(currentDir + "baseDir\\zookeeper\\bin\\zkEnv.cmd");
            batPathArray.Add(currentDir + "baseDir\\tools\\unzip.bat");
            batPathArray.Add(currentDir + "baseDir\\tools\\getjavastatus.bat");
            batPathArray.Add(currentDir + "baseDir\\frp\\frp.bat");
            batPathArray.Add(currentDir + "baseDir\\frp32bit\\frp.bat");
            batPathArray.Add(currentDir + "baseDir\\mysql-8.0.22-winx64\\mysql_init_db.bat");
            batPathArray.Add(currentDir + "baseDir\\mysql-8.0.22-winx64\\my.ini");
            batPathArray.Add(currentDir + "baseDir\\zookeeper\\conf\\zoo.cfg");
            batPathArray.Add(currentDir + "baseDir\\kafka\\config\\server.properties");
            
            for (int i = 0; i < batPathArray.Count; i++)
            {
                string path = batPathArray[i].ToString();
                string content = "";
                FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                StreamReader streamReader = new StreamReader(fileStream);
                content = streamReader.ReadToEnd();
                //64bit
                if (systemType)
                {
                    //java_home set 
                    content = content.Replace("#@replaceString@#", volume + ":\\openjdk");
                    //mysql set 
                    if (path.IndexOf("mysql")!=-1)
                    {
                        string tempString = currentDir + "baseDir";
                        string replaceString= tempString.Replace("\\", "\\\\");
                        //Slashes are replaced with double backslashes
                        content = content.Replace("#@replaceStringMySql@#", replaceString);
                    }
                    //zookeeper data dir set 
                    if (path.IndexOf("zookeeper") != -1)
                    {
                        content = content.Replace("#@replaceStringZookeeperDataDir@#", currentDir + "baseDir\\zookeeper\\conf\\data");
                    }
                    //kafka log dir set 
                    if (path.IndexOf("kafka") != -1)
                    {
                        content = content.Replace("#@replaceStringKafkaLogDir@#", currentDir + "baseDir\\kafka\\logs");
                    }
                }
                else //32bit
                {
                    content = content.Replace("#@replaceString@#", volume  +":\\jre32bit");
                }

                streamReader.Close();
                fileStream.Close();
                //Delete old files write new files
                File.Delete(path);
                FileStream fileStreamNew = new FileStream(path, FileMode.Create, FileAccess.Write);//Create write file
                StreamWriter streamWriterNew = new StreamWriter(fileStreamNew, Encoding.Default);
                streamWriterNew.WriteLine(content);
                streamWriterNew.Close();
            }


            // delegate send message
            sendEndMes("process end");

        }
        #endregion

        #region ReplaceLineInFile
        private static void ReplaceLineInFile(string filename, string lineToMatch, string replaceWith)
        {
            var fileToOutput = new List<string>();
            using (var inputStream = File.OpenRead(filename))
            using (var inputReader = new StreamReader(inputStream))
            {
                string currentLine;
                while ((currentLine = inputReader.ReadLine()) != null)
                {
                    if (currentLine.Contains(lineToMatch))
                    {
                        currentLine =currentLine.Replace(lineToMatch, replaceWith);
                    }
                    fileToOutput.Add(currentLine);
                }
            }

            File.WriteAllLines(filename, fileToOutput, Encoding.GetEncoding("gb2312"));
        }
       #endregion 
        
        #region Copy folders
        /// <summary>
        /// Copy everything under the folder
        /// </summary>
        /// <param name="SourcePath">The folder to Copy</param>
        /// <param name="DestinationPath">Where do I copy it</param>
        /// <param name="overwriteexisting">Whether or not covered</param>
        /// <returns></returns>
        private static bool CopyDirectory(string SourcePath, string DestinationPath, bool overwriteexisting)
        {
            bool ret = false;
            try
            {
                SourcePath = SourcePath.EndsWith(@"\") ? SourcePath : SourcePath + @"\";
                DestinationPath = DestinationPath.EndsWith(@"\") ? DestinationPath : DestinationPath + @"\";

                if (Directory.Exists(SourcePath))
                {
                    if (Directory.Exists(DestinationPath) == false)
                        Directory.CreateDirectory(DestinationPath);

                    foreach (string fls in Directory.GetFiles(SourcePath))
                    {
                        FileInfo flinfo = new FileInfo(fls);
                        flinfo.CopyTo(DestinationPath + flinfo.Name, overwriteexisting);
                    }
                    foreach (string drs in Directory.GetDirectories(SourcePath))
                    {
                        DirectoryInfo drinfo = new DirectoryInfo(drs);
                        if (CopyDirectory(drs, DestinationPath + drinfo.Name, overwriteexisting) == false)
                            ret = false;
                    }
                }
                ret = true;
            }
            catch (Exception ex)
            {
                ret = false;
            }
            return ret;
        }
        #endregion 

    }
}
