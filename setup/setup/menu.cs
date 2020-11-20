using System;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Linq;
using System.Collections;

namespace setup
{
    public partial class menu : Form
    {

        #region 初始化
        public menu()
        {

            InitializeComponent();
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            //判断baseDir文件夹是否存在
            string currentDir = System.AppDomain.CurrentDomain.BaseDirectory;
            if (Directory.Exists(currentDir + "baseDir"))
            {
                //判断java环境是否正常
                if (JreInstalled())
                {
                    javaLabel.Text = "正常";
                }
                else
                {
                    javaLabel.Text = "异常";
                }
                //打开frp通道
                if (OpenChannel())
                {
                    frpLabel.Text = "正常";
                }
                else
                {
                    frpLabel.Text = "异常";
                }
                //启动zookeeper
                if (StartZooKeeper())
                {
                    zookeeperLabel.Text = "正常";
                }
                else
                {
                    zookeeperLabel.Text = "异常";
                }
                //启动kafka
                if (StartKafka())
                {
                    kafkaLabel.Text = "正常";
                    //kafka创建主题
                    //"kafka-topics.bat --create --zookeeper localhost:2181 --replication-factor 1 --partitions 1 --topic topic1"
                    //线程中启动命令防止界面卡死
                    Thread thKafkaThread = new Thread(() => KafkaCreatTopicThread());
                    thKafkaThread.IsBackground = true;
                    thKafkaThread.Start();
                }
                else
                {
                    kafkaLabel.Text = "异常";
                }

                //启动本地tomcat mysql
                //button_start.Visible = false;
            }


        }
        #endregion

        #region 提醒窗

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            if (this.Visible == true)
            {
                this.Hide();
                this.ShowInTaskbar = false;
            }
            else
            {
                this.Visible = true;
                this.ShowInTaskbar = true;
                this.WindowState = FormWindowState.Normal;
                this.BringToFront();
            }
        }
        #endregion

        #region java环境监测
        public static bool JreInstalled()
        {
            WriteLogToFile(" JreInstalled  开始");
            string strDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            Process proc = new Process();
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();
            proc.StandardInput.WriteLine("cd " + strDirectory + "baseDir\\tools\\");
            proc.StandardInput.WriteLine("getjavastatus.bat ");
            //ExecuteCommand("getjavastatus.bat ");
            proc.StandardInput.WriteLine("exit");
            proc.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
               MessageBox.Show("output>>" + e.Data);
            proc.BeginOutputReadLine();

            proc.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
                MessageBox.Show("error>>" + e.Data);
            proc.BeginErrorReadLine();

            proc.WaitForExit();
            while (!proc.StandardOutput.EndOfStream)
            {
                string line = proc.StandardOutput.ReadLine();
                if (line.Contains("Java environment normal"))
                {
                    return true;
                }
            }
            return false;


        }

        static void ExecuteCommand(string command)
        {
            string strDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            var processInfo = new ProcessStartInfo("cmd.exe", "/c " + command);
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = false;
            processInfo.RedirectStandardError = true;
            processInfo.RedirectStandardOutput = true;

           

            var process = Process.Start(processInfo);
           // process.StartInfo.RedirectStandardError = true;
          //  process.StartInfo.RedirectStandardInput = true;
           // process.StartInfo.RedirectStandardOutput = true;
          //  process.StandardInput.WriteLine("cd " + strDirectory + "baseDir\\tools\\");
           // process.StandardInput.WriteLine("getjavastatus.bat ");
            process.OutputDataReceived += (object sender, DataReceivedEventArgs e) =>
                MessageBox.Show("output>>" + e.Data);
            process.BeginOutputReadLine();

            process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) =>
                MessageBox.Show("error>>" + e.Data);
            process.BeginErrorReadLine();

            process.WaitForExit();

            MessageBox.Show("ExitCode: {0}"+ process.ExitCode);
            process.Close();
        }
        #endregion

        #region frp通道开启,动态指定端口号

        public static bool OpenChannel()
        {
            WriteLogToFile("OpenChannel 开始");
            //1、查找peer节点，该节点有流量服务。
            //2、查找到节点后向该节点peer发送消息，请求流量
            //3、服务端peer打开通道
            //4、客户端peer连接3处理打开的通道
            //1、判断映射后的外网网址是否可以访问
            //     可以访问的话通道已经开启
            //     不可以访问的话，重新开启通道
            string strDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            Process proc = new Process();
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();
            proc.StandardInput.WriteLine("cd " + strDirectory + "baseDir\\frp");
            proc.StandardInput.WriteLine("frp.bat ");
            while (!proc.StandardOutput.EndOfStream)
            {
                string line = proc.StandardOutput.ReadLine();
                if (line.Contains("success"))
                {
                    return true;
                }
                if (line.Contains("failed"))
                {
                    return false;
                }
            }
            return false;
        }
        #endregion
                     
        #region 启动 zookeeper 线程
        public static bool StartZooKeeper()
        {
            WriteLogToFile("StartZooKeeper 开始");
            //线程中启动命令防止界面卡死
            Thread thZookeeperThread = new Thread(() => ZookeeperThread());
            thZookeeperThread.IsBackground = true;
            thZookeeperThread.Start();
            return PortInUse(2181);
        }
        #endregion

        #region 启动 kafka 线程
        public static bool StartKafka()
        {
            WriteLogToFile("StartKafka 开始");
            //线程中启动命令防止界面卡死
            Thread thKafkaThread = new Thread(() => KafkaThread());
            thKafkaThread.IsBackground = true;
            thKafkaThread.Start();
            return PortInUse(9092);
            
        }
        #endregion

        #region  启动 zookeeper
        public static void ZookeeperThread()
        {
            string strDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            Process proc = new Process();
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();
            proc.StandardInput.WriteLine("cd " + strDirectory + "baseDir\\zookeeper\\bin");
            proc.StandardInput.WriteLine("zkServer.cmd ");
            proc.WaitForExit();
        }
        #endregion

        #region 启动  kafka
        public static void KafkaThread()
        {
            WriteLogToFile("KafkaThread 开始");
            string currentDir = System.AppDomain.CurrentDomain.BaseDirectory;
            Process proc = new Process();
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();
            //启动kafka服务
            string volume = System.Windows.Forms.Application.StartupPath.Substring(0, System.Windows.Forms.Application.StartupPath.IndexOf(":"));
            proc.StandardInput.WriteLine("cd " + volume+":\\kafka\\bin\\windows");
            proc.StandardInput.WriteLine("kafka-server-start.bat server.properties");
            while (!proc.StandardOutput.EndOfStream)
            {
                string line = proc.StandardOutput.ReadLine();
                if (line.Contains("success"))
                {
                   // return true;
                }
                if (line.Contains("failed"))
                {
                   // return false;
                }
            }
            proc.WaitForExit();
        }
        #endregion

        #region kafka 创建主题
        public static void KafkaCreatTopicThread()
        {
            WriteLogToFile("KafkaCreatTopicThread 开始");
            string strDirectory = System.AppDomain.CurrentDomain.BaseDirectory;
            Process proc = new Process();
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();
            //kafka创建主题
            string volume = System.Windows.Forms.Application.StartupPath.Substring(0, System.Windows.Forms.Application.StartupPath.IndexOf(":"));
            proc.StandardInput.WriteLine("cd " + volume + "\\kafak\\bin\\windows");
            proc.StandardInput.WriteLine("kafka-topics.bat --create --zookeeper localhost:2181 --replication-factor 1 --partitions 1 --topic topic1");
            proc.WaitForExit();
        }
        #endregion

        #region PortInUse 判断端口是否被占用
        public static bool PortInUse(int port)
        {
            WriteLogToFile("PortInUse 开始");
            bool inUse = false;
            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();
            foreach (IPEndPoint endPoint in ipEndPoints)
            {
                if (endPoint.Port == port)
                {
                    inUse = true;
                    break;
                }
            }
            return inUse;
        }
        #endregion

        #region  关闭退出
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();//退出应用程序
        }
        #endregion

        #region 写log

        public static void WriteLogToFile(string msg)
        {
            string strDic = System.AppDomain.CurrentDomain.BaseDirectory;
            System.DateTime currentTime = System.DateTime.Now;

            if (!Directory.Exists(strDic + "\\logFiles\\" + DateTime.Now.ToString("yyyy-MM-dd")))  //不存在则创建
            {
                Directory.CreateDirectory(strDic + "\\logFiles\\" + DateTime.Now.ToString("yyyy-MM-dd"));
            }
            string logPath = strDic + "\\logFiles\\" + DateTime.Now.ToString("yyyy-MM-dd") + "\\IMPORT_DTJC_LOG_" + currentTime.ToLongDateString() + ".txt";
            try
            {
                using (StreamWriter sw = File.AppendText(logPath))
                {
                    sw.WriteLine("[" + currentTime.ToString() + "]" + msg);
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                }
            }
            catch (IOException e)
            {
                using (StreamWriter sw = File.AppendText(logPath))
                {
                    sw.WriteLine("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "]" + e.Message);
                    sw.Flush();
                    sw.Close();
                    sw.Dispose();
                }
            }
        }
        #endregion

    }
}
