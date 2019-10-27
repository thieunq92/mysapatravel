using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    /// <summary>
    /// danh sách đơn vị tính
    /// </summary>
    public partial class IvUnitList : SailsAdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Đơn vị tính";
            if (!IsPostBack)
            {
                FillCategory();
            }
        }

        private void FillCategory()
        {


            rptUnits.DataSource = Module.IvUnitGetAll(null);
            rptUnits.DataBind();
        }
    }
}