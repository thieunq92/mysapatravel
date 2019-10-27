using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal.Modules.OrientalSails.Domain;

namespace Portal.Modules.OrientalSails.Web.Controls
{
    public partial class QAgentLevelConfig : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public string AgentLevelCode = "";
        public void SetGroupCruise(string agentLvCode, SailsModule module, int groupId, QQuotation quotation)
        {
            var group = module.GetById<QCruiseGroup>(groupId);
            litGroupName.Text = group.Name;
            AgentLevelCode = agentLvCode;
            quotationPrice2Day.NewQuotationPriceConfig(module, agentLvCode, 2, group, quotation);
            quotationPrice3Day.NewQuotationPriceConfig(module, agentLvCode, 3, group, quotation);
        }

        public void SaveAgentLevelPriceConfig(string agentLvCode, SailsModule module, int groupId, QQuotation qQuotation)
        {
            quotationPrice2Day.SaveQuotationPriceConfig(module, agentLvCode, 2, groupId, qQuotation);
            quotationPrice3Day.SaveQuotationPriceConfig(module, agentLvCode, 3, groupId, qQuotation);
        }
    }
}