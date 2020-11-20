using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using System.Collections;
using System.Diagnostics;
using System.Threading;

namespace setup
{
    public partial class start : Form
    {
        #region 变量区域
        //消息标记，加载页到主页标记
        bool startFlag = false;
        int timerCount = 1;
        #endregion

        #region 启动页初始化
        public start()
        {

            InitializeComponent();

            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            progressBar1.Minimum = 0;//设置ProgressBar组件最小值为0
            progressBar1.Maximum = 10;//Maximum最大值为10
            progressBar1.MarqueeAnimationSpeed = 50;//设定进度快在进度栏中移动的时间段

            string currentDir = System.AppDomain.CurrentDomain.BaseDirectory;

            if (!Directory.Exists(currentDir + "baseDir"))
            {
               
                // 开启定时器
                timer1.Enabled = true;
                timer1.Start();
                // 将loadData中的委托绑定主程序中的通知结束的方法
                LoadData loadData = new LoadData();
                loadData.sendEndMes += tellEnd;
                // 开启线程处理数据
                ParameterizedThreadStart loadThread = new ParameterizedThreadStart(loadData.load);
                Thread thread = new Thread(loadThread);
                thread.IsBackground = true;
                thread.Start();
                WriteLogToFile("解压线程开启");
            }
        }
        #endregion

        #region 时钟计数器
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timerCount % 6 == 1)
            {
                label1.Text = "initializing.";
            }
            else if (timerCount % 6 == 2)
            {
                label1.Text = "initializing..";
            }
            else if (timerCount % 6 == 3)
            {
                label1.Text = "initializing...";
            }
            else if (timerCount % 6 == 4)
            {
                label1.Text = "initializing....";
            }
            else if (timerCount % 6 == 5)
            {
                label1.Text = "initializing.....";
            }
            else 
            {
                label1.Text = "initializing......";
            }
            timerCount++;
            if (startFlag)
            {
                WriteLogToFile("解压线程结束");
                //定时时间到了处理事件
                this.Hide();//隐藏本窗体
                menu MainForm = new menu();//实例化一个MainForm对象
                MainForm.Show();//显示窗体
                timer1.Stop();//定制定时器
             }
        }
        #endregion 

        #region 处理结束消息
        /// <summary>
        /// 通知load数据结束的方法
        /// 此方法仍为子线程中的方法，因为被子线程中的委托调用
        /// </summary>
        /// <param name="mes"></param>
        private void tellEnd(string mes)
        {
            startFlag = true;
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
