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
    public partial class ContractManagement : System.Web.UI.Page
    {
        public ContractManagementBLL contractManagementBLL;
        public ContractManagementBLL ContractManagementBLL
        {
            get
            {
                if (contractManagementBLL == null)
                    contractManagementBLL = new ContractManagementBLL();
                return contractManagementBLL;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            rptContract.DataSource = ContractManagementBLL.ContractGetAll();
            rptContract.DataBind();
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (contractManagementBLL != null)
            {
                contractManagementBLL.Dispose();
                contractManagementBLL = null;
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