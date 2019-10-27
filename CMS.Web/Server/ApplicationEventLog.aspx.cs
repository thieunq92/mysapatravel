using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace CMS.Web.Server
{
    public partial class ApplicationEventLog : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            SaveLog("application_event_log");
        }

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern IntPtr OpenEventLog(string UNCServerName, string sourceName);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        static extern bool BackupEventLog(IntPtr hEventLog, string backupFile);

        [DllImport("advapi32.dll", SetLastError = true)]
        static extern bool CloseEventLog(IntPtr hEventLog);

        void SaveLog(string eventLogName)
        {
            string exportedEventLogFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, eventLogName + ".evt");

            //Returns handle to Application log if Custom log does not exist.    
            IntPtr logHandle = OpenEventLog(Environment.MachineName, eventLogName);

            if (IntPtr.Zero != logHandle)
            {
                bool retValue = BackupEventLog(logHandle, exportedEventLogFileName);
                //If false, notify.
                CloseEventLog(logHandle);
            }
        }
    }
}