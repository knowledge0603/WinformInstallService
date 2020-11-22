using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace setup
{
    class InstallKafka
    {
        #region Declarative region
        //Declares a delegate that returns the end information to the main thread
        public delegate void SendEndMessage(string mes);
        public SendEndMessage sendEndMessage;
        string currentDir = System.AppDomain.CurrentDomain.BaseDirectory;
        #endregion

        #region install kafka service
        /// <summary>
        /// load
        /// </summary>
        public void Load(object obj)
        {
            //install kafka service and  start kafka service 
            if (Common.IsServiceExisted("KafkaWindowsService"))
            {
                Common.UninstallService(Common.volume + ":\\kafka\\bin\\windows\\" + "KafkaWindowsService.exe");
            }
            Common.InstallService(Common.volume + ":\\kafka\\bin\\windows\\" + "KafkaWindowsService.exe");
            if (Common.IsServiceExisted("KafkaWindowsService"))
            {
                Common.ServiceStart("KafkaWindowsService");
            }
        }
        #endregion
    }
}
