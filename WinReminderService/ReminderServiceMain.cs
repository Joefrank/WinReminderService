
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceProcess;
using System.IO;

namespace WinReminderService
{
    public class ReminderServiceMain
    {
         public string _strLogPath;
        public int _intSleepTime;

        public ReminderServiceMain()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static void Main(System.String[] args)
        {
            ReminderServiceMain anandServiceMain = new ReminderServiceMain();
            anandServiceMain._strLogPath = args[0];
            anandServiceMain._intSleepTime = Convert.ToInt32(args[1]);
            System.ServiceProcess.ServiceBase.Run(new ReminderServiceHandler(anandServiceMain));
        }
    }
}
