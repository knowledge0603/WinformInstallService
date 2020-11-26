using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading;
using System.Linq;
using System.Collections;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.ServiceProcess;
using Microsoft.VisualBasic.Devices;

namespace setup
{
    public partial class menu : Form
    {
        #region  declare
        //Determine if the basedir folder exists
        static string currentDir = System.AppDomain.CurrentDomain.BaseDirectory;
        bool sendMessageStatus = false;
        bool mysqlMessageStatus = false;
        System.Timers.Timer zookeeperTimer = new System.Timers.Timer();
        System.Timers.Timer kafkaTimer = new System.Timers.Timer();
        System.Timers.Timer mySqlTimer = new System.Timers.Timer();
        int timerCount = 1;
        #endregion

        #region initialize
        public menu()
        {

            InitializeComponent();
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            System.OperatingSystem osInfo = System.Environment.OSVersion;
            //Determine if the system is Windows server 2012 2016 2019
            ComputerInfo info = new ComputerInfo();
            if (info.OSFullName.IndexOf("2012") != -1
                || info.OSFullName.IndexOf("2016") != -1
                || info.OSFullName.IndexOf("2019") != -1)
            {
                //Determine whether the local IP is public IP
                if (!Common.IsInnerIP(Common.GetLocalIp()))
                {
                    serverTabPage.Parent = serverTabPage;  //enable
                }
            }
            else 
            {
                serverTabPage.Parent = null;  //disable
            }

            if (Directory.Exists(currentDir + "baseDir"))
            {
                //frp
                if (FrpServiceStatus())
                {
                    frpLabel.Text = "normal";
                }
                else
                {
                    frpLabel.Text = "abnormal";
                    if (!Common.IsServiceExisted("FrpWindowsService"))
                    {
                        Common.InstallService(AppDomain.CurrentDomain.BaseDirectory + "baseDir\\frp\\FrpWindowsService.exe");
                    }
                    else 
                    {
                        using (ServiceController control = new ServiceController("FrpWindowsService"))
                        {
                            if (control.Status == ServiceControllerStatus.Stopped)
                            {
                                control.Start();
                            }
                        }
                    }
                    System.Windows.Forms.Timer frpTimer = new System.Windows.Forms.Timer();
                    frpTimer.Tick += new EventHandler(FrpTimerEvent);
                    frpTimer.Interval = 1000;
                    frpTimer.Enabled = true;
                    frpTimer.Start();
                }

                if (MysqlServiceStatus())
                {
                    mysqlLabel.Text = "normal";
                }
                else
                {
                    mysqlLabel.Text = "abnormal";
                    if (!Common.IsServiceExisted("MysqlWindowsService"))
                    {
                        InstallMySQL installMySQL = new InstallMySQL();
                        installMySQL.sendEndMes += TellEnd;
                        ParameterizedThreadStart mySqlThread = new ParameterizedThreadStart(installMySQL.Load);
                        Thread thread = new Thread(mySqlThread);
                        thread.IsBackground = true;
                        thread.Start();
                    }
                    else 
                    {
                        using (ServiceController control = new ServiceController("MysqlWindowsService"))
                        {
                            if (control.Status == ServiceControllerStatus.Stopped)
                            {
                                control.Start();
                            }
                        }
                    }
                    System.Windows.Forms.Timer mySqlTimer = new System.Windows.Forms.Timer();
                    mySqlTimer.Tick += new EventHandler(MysqlTimerEvent);
                    mySqlTimer.Interval = 1000;
                    mySqlTimer.Enabled = true;
                    mySqlTimer.Start();
                }
                //zookeeper
                /* if (ZooKeeperServerStatus())
                 {
                     zookeeperLabel.Text = "normal";
                 }
                 else
                 {
                     zookeeperLabel.Text = "abnormal";
                     System.Windows.Forms.Timer zookeeperTimer = new System.Windows.Forms.Timer();
                     zookeeperTimer.Tick += new EventHandler(ZookeeperTimerEvent);
                     zookeeperTimer.Enabled = true;
                     zookeeperTimer.Start();
                     InstallZookeeper installZookeeper = new InstallZookeeper();
                     installZookeeper.sendEndMessage += TellEnd;
                     ParameterizedThreadStart zookeeperThread = new ParameterizedThreadStart(installZookeeper.Load);
                     Thread thread = new Thread(zookeeperThread);
                     thread.IsBackground = true;
                     thread.Start();

                 }
                 if (KafkaServiceStatus())
                 {
                     kafkaLabel.Text = "normal";
                     //kafka create topic
                     Thread thKafkaThread = new Thread(() => KafkaCreatTopicThread());
                     thKafkaThread.IsBackground = true;
                     thKafkaThread.Start();
                 }
                 else
                 {
                     kafkaLabel.Text = "abnormal";
                     kafkaTimer.Enabled = true;
                     kafkaTimer.Start();
                     InstallKafka installKafka = new InstallKafka();
                     installKafka.sendEndMessage += TellEnd;
                     ParameterizedThreadStart kafkaThread = new ParameterizedThreadStart(installKafka.Load);
                     Thread thread = new Thread(kafkaThread);
                     thread.IsBackground = true;
                     thread.Start();
                 }
                 

                 string[] strArr = { "FrpWindowsService", "ZookeeperWindowsService", "KafkaWindowsService", "MysqlWindowsService" };
                 //install win service
                 foreach (string str in strArr)
                 {
                     if (this.IsServiceExisted(str))
                     {
                         this.ServiceStart(str);
                     }
                 }
                 timer1.Start();*/
            }
        }
        #endregion

        #region notifyIcon

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

        #region frp channel

        public bool FrpServiceStatus()
        {
            var ini = new IniFile();
            ini.Load(currentDir + "baseDir\\frp\\frpc.ini");
            string ip = ini["common"]["server_addr"].ToString().TrimStart().TrimEnd();
            string port = ini["common"]["server_port"].ToString();
            if (FrpPortInUse(ip, port)) 
            {
                /*  var iniFrp = new IniFile();
                  iniFrp.Load(currentDir + "baseDir\\frp\\FrpWindowsServiceStatus.ini");
                  string frpStatus = ini["frpServiceStatus"]["runing"].ToString().TrimStart().TrimEnd();
                  if (frpStatus=="true") 
                  {
                      return true;
                  } */
                if (Common.IsServiceExisted("FrpWindowsService"))
                {
                    using (ServiceController control = new ServiceController("FrpWindowsService"))
                    {
                        if (control.Status == ServiceControllerStatus.Running)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        #endregion

        #region  zookeeper  service status
        public static bool ZooKeeperServerStatus()
        {
            WriteLogToFile("StartZooKeeper start");
            return PortInUse(2181);
        }
        #endregion

        #region  kafka status
        public static bool KafkaServiceStatus()
        {
            WriteLogToFile("KafkaServiceStatus  methods start ");
            return PortInUse(9092);

        }
        #endregion

        #region  mysql status
        public static bool MysqlServiceStatus()
        {
            WriteLogToFile("MysqlServiceStatus  methods start ");
            return PortInUse(3306);

        }
        #endregion

        #region kafka create topic
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

        #region PortInUse
        public static bool PortInUse(int port)
        {
            WriteLogToFile("PortInUse start");
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

        #region  close
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {


        }
        #endregion

        #region write log

        public static void WriteLogToFile(string msg)
        {
            string strDic = System.AppDomain.CurrentDomain.BaseDirectory;
            System.DateTime currentTime = System.DateTime.Now;

            if (!Directory.Exists(strDic + "\\logFiles\\" + DateTime.Now.ToString("yyyy-MM-dd")))
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

        #region The delayed method is not blocked, and Thread Sleep will block and render the interface unreadable
        public static void Delay(int mm)
        {
            DateTime current = DateTime.Now;
            while (current.AddMilliseconds(mm) > DateTime.Now)
            {
                Application.DoEvents();
            }
            return;
        }
        #endregion

        #region timer  monitor  service
        private void timer1_Tick(object sender, EventArgs e)
        {
            /* if (ZooKeeperServerStatus())
             {
                 zookeeperLabel.Text = "normal";
             }
             else
             {
                 zookeeperLabel.Text = "abnormal";
                 if (this.IsServiceExisted("ZookeeperWindowsService"))
                 {
                     this.ServiceStart("ZookeeperWindowsService");
                 }
             }
             if (KafkaServiceStatus())
             {
                 kafkaLabel.Text = "normal";
             }
             else
             {
                 kafkaLabel.Text = "abnormal";
                 if (this.IsServiceExisted("KafkaWindowsService"))
                 {
                     using (ServiceController control = new ServiceController("KafkaWindowsService"))
                     {
                         if (control.Status == ServiceControllerStatus.Running)
                         {
                             control.Stop();
                         }
                         if (control.Status == ServiceControllerStatus.Stopped)
                         {
                             control.Start();
                             Thread.Sleep(7000);
                         }
                     }
                 }
             }*/
        }
        #endregion

        #region Close the form hidden to the tray
        private void menu_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Notice that the Reason for the close event comes from the form button, otherwise you cannot exit when you exit with the menu!
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;    //Cancel the closed Window event
                this.WindowState = FormWindowState.Minimized;    //Causes the window to shrink towards the lower right corner when closed
                notifyIcon1.Visible = true;
                this.Hide();
                return;
            }
        }
        #endregion

        #region Right-click tray exit
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region FrpPortInUse

        public static bool FrpPortInUse(string remoteIp, string port)
        {
            bool status = false;
            if (!string.IsNullOrEmpty(port))
            {
                IPAddress ip = IPAddress.Parse(remoteIp);
                IPEndPoint point = new IPEndPoint(ip, int.Parse(port));
                try
                {
                    TcpClient tcp = new TcpClient();
                    tcp.Connect(point);
                    status = true;
                    WriteLogToFile(remoteIp + port + " is open ");
                }
                catch (Exception ex)
                {
                    status = false;
                    WriteLogToFile(ex.ToString());
                }
            }
            return status;
        }
        #endregion

        #region End of processing message
        /// <summary>
        /// Method to notify load of the end of data
        /// This method is still a method in the child thread because the delegate is called in the child thread
        /// </summary>
        /// <param name="mes"></param>
        private void TellEnd(string mes)
        {
            sendMessageStatus = true;
            mysqlMessageStatus = true;
        }
        #endregion


        public void ZookeeperTimerEvent(object sender, EventArgs e)
        {
            if (timerCount % 6 == 1)
            {
                zookeeperStatusLabel.Text = ".";
            }
            else if (timerCount % 6 == 2)
            {
                zookeeperStatusLabel.Text = "..";
            }
            else if (timerCount % 6 == 3)
            {
                zookeeperStatusLabel.Text = "...";
            }
            else if (timerCount % 6 == 4)
            {
                zookeeperStatusLabel.Text = "....";
            }
            else if (timerCount % 6 == 5)
            {
                zookeeperStatusLabel.Text = ".....";
            }
            else
            {
                zookeeperStatusLabel.Text = "......";
            }
            timerCount++;
        }

        public void FrpTimerEvent(object sender, EventArgs e)
        {
          
            //install frp service and  start frp service 
            if (!Common.IsServiceExisted("FrpWindowsService"))
            {
                Common.InstallService(AppDomain.CurrentDomain.BaseDirectory + "baseDir\\frp\\FrpWindowsService.exe");
            }
            if (Common.IsServiceExisted("FrpWindowsService"))
            {
                using (ServiceController control = new ServiceController("FrpWindowsService"))
                {
                    if (control.Status == ServiceControllerStatus.Stopped)
                    {
                        control.Start();
                        //Thread.Sleep(7000);
                    }
                }
            }
            var ini = new IniFile();
            ini.Load(currentDir + "baseDir\\frp\\frpc.ini");
            string ip = ini["common"]["server_addr"].ToString().TrimStart().TrimEnd();
            string port = ini["common"]["server_port"].ToString();
            if (FrpPortInUse(ip, port))
            {
                if (Common.IsServiceExisted("FrpWindowsService"))
                {
                    using (ServiceController control = new ServiceController("FrpWindowsService"))
                    {
                        if (control.Status == ServiceControllerStatus.Running)
                        {
                            frpLabel.Text = "normal";
                        }
                    }
                }
            }
        }

        public void MysqlTimerEvent(object sender, EventArgs e)
        {
            if (PortInUse(3306)) 
            {
                mysqlLabel.Text = "normal";
            }
        }

    }
}
