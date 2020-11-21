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
            if (Utils.IsServiceExisted("KafkaWindowsService"))
            {
                Utils.UninstallService(Utils.volume + ":\\kafka\\bin\\windows\\" + "KafkaWindowsService.exe");
            }
            Utils.InstallService(Utils.volume + ":\\kafka\\bin\\windows\\" + "KafkaWindowsService.exe");
            if (Utils.IsServiceExisted("KafkaWindowsService"))
            {
                Utils.ServiceStart("KafkaWindowsService");
            }
        }
        #endregion
    }
}
