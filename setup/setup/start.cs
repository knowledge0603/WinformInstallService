using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Diagnostics;
using System.Threading;

namespace setup
{
    public partial class start : Form
    {
        #region declare
        bool startFlag = false;
        int timerCount = 1;
        #endregion

        #region Start page initialization
        public start()
        {
            InitializeComponent();
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            progressBar1.Minimum = 0;//Set the ProgressBar component to a minimum value of 0
            progressBar1.Maximum = 10;//Maximum value is 10
            progressBar1.MarqueeAnimationSpeed = 50;//Set the time period for fast progress to move in the progress bar

            string currentDir = System.AppDomain.CurrentDomain.BaseDirectory;

            if (!Directory.Exists(currentDir + "baseDir"))
            {
                // On timer
                timer1.Enabled = true;
                timer1.Start();
                // A method that binds a delegate in loadData to the end of a notification in the main program
                LoadData loadData = new LoadData();
                loadData.sendEndMes += tellEnd;
                // Start the thread to process the data
                ParameterizedThreadStart loadThread = new ParameterizedThreadStart(loadData.load);
                Thread thread = new Thread(loadThread);
                thread.IsBackground = true;
                thread.Start();
                WriteLogToFile("The unzip thread opens");
            }
        }
        #endregion

        #region Clock counter
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
            //Delegate messaging flags to startFlag
            if (startFlag)
            {
                WriteLogToFile("The decompression thread ends");
                this.Hide();//Hide this form
                menu MainForm = new menu();//Instantiate a MainForm object
                MainForm.Show();//According to the form
                timer1.Stop();//Custom timer
             }
        }
        #endregion 

        #region End of processing message
        /// <summary>
        /// Method to notify load of the end of data
        /// This method is still a method in the child thread because the delegate is called in the child thread
        /// </summary>
        /// <param name="mes"></param>
        private void tellEnd(string mes)
        {
            startFlag = true;
        }
        #endregion

        #region write log

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
