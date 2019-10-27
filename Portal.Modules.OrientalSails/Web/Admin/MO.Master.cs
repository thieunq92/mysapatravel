using System;
using System.Collections;
using System.Reflection;
using System.Web.UI;
using CMS.Core.Domain;
using CMS.Web.HttpModules;
using Portal.Modules.OrientalSails.Domain;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Web.Hosting;
using Portal.Modules.OrientalSails.BusinessLogic;
using Portal.Modules.OrientalSails.Utils;
using System.Web;
using Portal.Modules.OrientalSails.Enums;
using Portal.Modules.OrientalSails.BusinessLogic.Share;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class MO : MasterPage
    {
        public string Title
        {
            get
            {
                return this.Master.Title;
            }
            set
            {
                this.Master.Title = value;
            }
        }
    }
}