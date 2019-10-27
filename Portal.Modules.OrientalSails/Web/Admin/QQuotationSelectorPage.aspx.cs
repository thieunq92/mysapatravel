using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal.Modules.OrientalSails.Web.Controls;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class QQuotationSelectorPage : QQuotationList
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            string clientId = Request.QueryString["clientid"];

            string script = string.Format(
                @"function Done(name, id)
{{
    idcontrol = window.opener.document.getElementById('{0}');
	idcontrol.value = id;
    namecontrol = window.opener.document.getElementById('{1}');
    namecontrol.value = name;
    window.close();
}}", clientId, clientId + AgencySelector.PNAMEID);

            Page.ClientScript.RegisterClientScriptBlock(typeof(QQuotationSelectorPage), "done", script, true);
        }
    }
}