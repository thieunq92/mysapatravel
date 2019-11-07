using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS.Web.Server
{
    public partial class BackupSql : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Microsoft.SqlServer.Management.Smo.Server myServer = new Microsoft.SqlServer.Management.Smo.Server(@"112.78.2.202,1433");
            myServer.ConnectionContext.LoginSecure = false;
            myServer.ConnectionContext.Login = "mys05843_mys05843";
            myServer.ConnectionContext.Password = "@#$mys1nineninetwo";
            myServer.ConnectionContext.Connect();

            Backup bkpDBFull = new Backup();
            bkpDBFull.Action = BackupActionType.Database;
            bkpDBFull.Database = "mys05843_mysapatravel";
            bkpDBFull.Devices.AddDevice(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"mys.bak"), DeviceType.File);
            bkpDBFull.BackupSetName = "mys";
            bkpDBFull.BackupSetDescription = "mys database - Full Backup";
            bkpDBFull.ExpirationDate = DateTime.Today.AddDays(360);
            bkpDBFull.Initialize = false;
            bkpDBFull.SqlBackup(myServer);
        }
    }
}