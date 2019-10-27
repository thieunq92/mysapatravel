using CMS.Core.Domain;
using CMS.Web.HttpModules;
using Portal.Modules.OrientalSails.BusinessLogic;
using Portal.Modules.OrientalSails.BusinessLogic.Share;
using Portal.Modules.OrientalSails.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class MO_NoScriptManager : MasterPage
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