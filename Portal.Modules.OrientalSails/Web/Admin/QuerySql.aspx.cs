using Portal.Modules.OrientalSails.Web.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class QuerySql : Page
    {
        public SailsModule Module
        {
            get
            {
                return SailsModule.GetInstance();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}