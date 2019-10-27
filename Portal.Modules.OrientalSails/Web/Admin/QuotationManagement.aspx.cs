using Portal.Modules.OrientalSails.BusinessLogic;
using Portal.Modules.OrientalSails.Enums.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class QuotationManagement : System.Web.UI.Page
    {
        public QuotationManagementBLL quotationManagementBLL;
        public QuotationManagementBLL QuotationManagementBLL
        {
            get
            {
                if (quotationManagementBLL == null)
                    quotationManagementBLL = new QuotationManagementBLL();
                return quotationManagementBLL;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            rptQuotation.DataSource = QuotationManagementBLL.QuotationGetAll();
            rptQuotation.DataBind();
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (quotationManagementBLL != null)
            {
                quotationManagementBLL.Dispose();
                quotationManagementBLL = null;
            }
        }
        public string GetCurrency(int currency)
        {
            switch (currency)
            {
                case (int)CurrencyEnum.USD:
                    return "USD";
                case (int)CurrencyEnum.VND:
                    return "VND";
                default:
                    return "";
            }
        }
    }
}