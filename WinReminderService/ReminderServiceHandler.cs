using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.ServiceProcess;
using System.IO;
 

namespace WinReminderService
{
    public class ReminderServiceHandler :  System.ServiceProcess.ServiceBase
    {
         /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        ReminderServiceMain reminderMainSvs = null;
        System.Threading.Thread ReminderThread = null;
 
        int i = 0;
        public ReminderServiceHandler(ReminderServiceMain reminderMainSvs)
        {
            this.reminderMainSvs = reminderMainSvs;
            // This call is required by the Windows.Forms Component Designer.
            InitializeComponent();
            // TODO: Add any initialization after the InitComponent call
        }
 
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.ServiceName = "WinReminderService";
        }
 
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }
 
        /// <summary>
        /// Set things in motion so your service can do its work.
        /// </summary>
        protected override void OnStart(string[] args)
        {
            var reminderThreadStart = new System.Threading.ThreadStart(WriteToLog);
            ReminderThread = new System.Threading.Thread(reminderThreadStart);
            ReminderThread.Start();
        }
 
        /// <summary>
        /// Stop this service.
        /// </summary>
        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }
        public void WriteToLog()
        {
            bool blnLog = true;
            while (blnLog == true)
            {
                i = i + 1;
                StreamWriter w = File.AppendText(reminderMainSvs._strLogPath);
                Log(i.ToString(CultureInfo.InvariantCulture), w);
                System.Threading.Thread.Sleep(reminderMainSvs._intSleepTime);
                if (i > 10)
                {
                    blnLog = false;
                }
            }
        }
        public void Log(string logMessage, TextWriter w)
        {
            w.WriteLine("+----------------------------------------------------------------+");
            w.Write("      Log Entry " + logMessage + ": " + DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + "\r\n");
            w.Flush();
            w.Close();
        }
    }
}
