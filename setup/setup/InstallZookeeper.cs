﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace setup
{
    class InstallZookeeper
    {
        #region Declarative region
        //Declares a delegate that returns the end information to the main thread
        public delegate void SendEndMessage(string mes);
        public SendEndMessage sendEndMessage;
        string currentDir = System.AppDomain.CurrentDomain.BaseDirectory;
        #endregion

        #region install zookeeper service
        /// <summary>
        /// load
        /// </summary>
        public void Load(object obj)
        {
            //install zookeeper service and  start zookeeper service 
            if (Utils.IsServiceExisted("ZookeeperWindowsService"))
            {
                Utils.UninstallService(AppDomain.CurrentDomain.BaseDirectory + "baseDir\\zookeeper\\bin\\ZookeeperWindowsService.exe");
            }
            Utils.InstallService(AppDomain.CurrentDomain.BaseDirectory + "baseDir\\zookeeper\\bin\\ZookeeperWindowsService.exe");
            if (Utils.IsServiceExisted("ZookeeperWindowsService"))
            {
                Utils.ServiceStart("ZookeeperWindowsService");
            }
        }
        #endregion
    }
}
