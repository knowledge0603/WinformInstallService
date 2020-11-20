using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;
using System.Threading;
using System.IO;
using System.Collections;
using System.Diagnostics;

namespace setup
{
    class LoadData
    {
        #region 声明区域
        // 声明一个返回结束信息给主线程的委托
        public delegate void SendEndMessage(string mes);
        public SendEndMessage sendEndMes;
        #endregion

        #region 复制文件夹
        /// <summary>
        /// 文件夹下所有内容copy
        /// </summary>
        /// <param name="SourcePath">要Copy的文件夹</param>
        /// <param name="DestinationPath">要复制到哪个地方</param>
        /// <param name="overwriteexisting">是否覆盖</param>
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

        #region 解压与初始化配置
        /// <summary>
        /// 模拟一个处理时间很长的方法
        /// </summary>
        public void load(object obj)
        {
            // 当前线程sleep 10秒
            string currentDir = System.AppDomain.CurrentDomain.BaseDirectory;
            using (ZipInputStream s = new ZipInputStream(File.OpenRead(currentDir + "baseDir.zip")))
            {
                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    string directoryName = Path.GetDirectoryName(theEntry.Name);
                    string fileName = Path.GetFileName(theEntry.Name);

                    if (directoryName.Length > 0)
                    {
                        Directory.CreateDirectory(directoryName);
                    }

                    if (fileName != String.Empty)
                    {
                        using (FileStream streamWriter = File.Create(theEntry.Name))
                        {
                            int size = 1024*1024*7;
                            byte[] data = new byte[1024*1024*7];
                            while (true)
                            {
                                size = s.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    streamWriter.Write(data, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            //将kafka文件移动到盘符的根目录，kafka执行启动时所在路径太长，无法启动
            string volume = System.Windows.Forms.Application.StartupPath.Substring(0, System.Windows.Forms.Application.StartupPath.IndexOf(":"));

            CopyDirectory(currentDir + "baseDir\\kafka", volume + ":\\kafka",true);
            ArrayList batPathArray = new ArrayList();
            batPathArray.Add(volume + ":\\kafka\\bin\\windows\\" + "kafka-run-class.bat");
            batPathArray.Add(currentDir + "baseDir\\zookeeper\\bin\\zkEnv.cmd");
            batPathArray.Add(currentDir + "baseDir\\tools\\unzip.bat");
            batPathArray.Add(currentDir + "baseDir\\tools\\getjavastatus.bat");
            batPathArray.Add(currentDir + "baseDir\\frp\\frp.bat");
            //查看系统64位标记
            bool systemType = Environment.Is64BitOperatingSystem;
            for (int i = 0; i < batPathArray.Count; i++)
            {
                string path = batPathArray[i].ToString();
                string content = "";
                FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
                StreamReader streamReader = new StreamReader(fileStream);
                content = streamReader.ReadToEnd();
                //64位系统
                if (systemType)
                {
                    content = content.Replace("#@replaceString@#", currentDir + "openjdk");
                }
                else //32位系统
                {
                    content = content.Replace("#@replaceString@#", currentDir + "baseDir\\jre32bit");
                }
                
                content = content.Replace("#@zipFilePath@#", currentDir + "baseDir.zip");
                streamReader.Close();
                fileStream.Close();
                //删除旧文件写入新文件
                File.Delete(path);
                FileStream fileStreamNew = new FileStream(path, FileMode.Create, FileAccess.Write);//创建写入文件 
                StreamWriter streamWriterNew = new StreamWriter(fileStreamNew);
                streamWriterNew.WriteLine(content);
                streamWriterNew.Close();
                streamWriterNew.Close();
            }
            // 调用通知主程序处理结束的委托，并可以传递参数
            sendEndMes("处理结束");
        }
        #endregion
    }
}
