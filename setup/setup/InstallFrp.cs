using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace setup
{
    class InstallFrp
    {
        #region Declarative region
        //Declares a delegate that returns the end information to the main thread
        public delegate void SendEndMessage(string mes);
        public SendEndMessage sendEndMes;
        string currentDir = System.AppDomain.CurrentDomain.BaseDirectory;
        #endregion

        #region install frp service
        /// <summary>
        /// load
        /// </summary>
        public void Load(object obj)
        {
            //install frp service and  start frp service 
            if (!Common.IsServiceExisted("FrpWindowsService"))
            {
                Common.InstallService(AppDomain.CurrentDomain.BaseDirectory + "baseDir\\frp\\FrpWindowsService.exe");
            }
            if (Common.IsServiceExisted("FrpWindowsService"))
            {
                Common.ServiceStart("FrpWindowsService");
            }
        }
        #endregion
    }
}
