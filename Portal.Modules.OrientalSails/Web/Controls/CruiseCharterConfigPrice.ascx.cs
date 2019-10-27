using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal.Modules.OrientalSails.Domain;

namespace Portal.Modules.OrientalSails.Web.Controls
{
    public partial class CruiseCharterConfigPrice : System.Web.UI.UserControl
    {
        private SailsModule _module;
        private QQuotation _qQuotation;
        private int _trip;
        private string _agentLvCode;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void DisplayDataConfig(SailsModule module, Cruise cruise, QCruiseGroup group, string agentLvCode, int trip, QQuotation qQuotation)
        {
            litCruiseName.Text = cruise.Name;
            _module = module;
            _qQuotation = qQuotation;
            _trip = trip;
            _agentLvCode = agentLvCode;
            rptCharterRanger.DataSource = module.GetCruiseCharterPrice(group.Id, cruise, agentLvCode, trip, qQuotation);
            rptCharterRanger.DataBind();
        }

        protected void btnAddRange_OnClick(object sender, EventArgs e)
        {
            IList data = new List<QCharterPrice>();
            foreach (RepeaterItem item in rptCharterRanger.Items)
            {
                var charterPrice = new QCharterPrice();
                HiddenField hidCharterPriceId = item.FindControl("hidCharterPriceId") as HiddenField;

                TextBox txtValidFrom = item.FindControl("txtValidFrom") as TextBox;
                TextBox txtValidTo = item.FindControl("txtValidTo") as TextBox;
                TextBox txtPriceUSD = item.FindControl("txtPriceUSD") as TextBox;
                TextBox txtPriceVND = item.FindControl("txtPriceVND") as TextBox;
                CheckBox checkIsDelete = item.FindControl("checkIsDelete") as CheckBox;


                if (hidCharterPriceId != null && (!string.IsNullOrEmpty(hidCharterPriceId.Value) && hidCharterPriceId.Value != "0"))
                {
                    charterPrice.Id = Convert.ToInt32(hidCharterPriceId.Value);
                }
                if (txtValidFrom != null && !string.IsNullOrEmpty(txtValidFrom.Text))
                {
                    charterPrice.Validfrom = Convert.ToInt32(txtValidFrom.Text);
                }
                if (txtValidTo != null && !string.IsNullOrEmpty(txtValidTo.Text))
                {
                    charterPrice.Validto = Convert.ToInt32(txtValidTo.Text);
                }

                if (txtPriceUSD != null && !string.IsNullOrEmpty(txtPriceUSD.Text))
                {
                    charterPrice.Priceusd = Convert.ToDecimal(txtPriceUSD.Text);
                }
                if (txtPriceVND != null && !string.IsNullOrEmpty(txtPriceVND.Text))
                {
                    charterPrice.Pricevnd = Convert.ToDecimal(txtPriceVND.Text);
                }

                if (checkIsDelete != null) charterPrice.IsDeleted = checkIsDelete.Checked;
                data.Add(charterPrice);
            }

            data.Add(new QCharterPrice());
            rptCharterRanger.DataSource = data;
            rptCharterRanger.DataBind();
        }
        protected void rptCharterRanger_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var charterPrice = e.Item.DataItem as QCharterPrice;
            if (charterPrice != null)
            {
                TextBox txtValidFrom = e.Item.FindControl("txtValidFrom") as TextBox;
                TextBox txtValidTo = e.Item.FindControl("txtValidTo") as TextBox;
                TextBox txtPriceUSD = e.Item.FindControl("txtPriceUSD") as TextBox;
                TextBox txtPriceVND = e.Item.FindControl("txtPriceVND") as TextBox;
                CheckBox checkIsDelete = e.Item.FindControl("checkIsDelete") as CheckBox;

                if (charterPrice.Validfrom != null)
                {
                    if (txtValidFrom != null) txtValidFrom.Text = charterPrice.Validfrom.ToString();
                }
                if (charterPrice.Validto != null)
                {
                    if (txtValidTo != null) txtValidTo.Text = charterPrice.Validto.ToString();
                }

                if (charterPrice.Priceusd != null)
                {
                    if (txtPriceUSD != null) txtPriceUSD.Text = charterPrice.Priceusd.ToString();
                }
                if (charterPrice.Pricevnd != null)
                {
                    if (txtPriceVND != null) txtPriceVND.Text = charterPrice.Pricevnd.ToString();
                }
                if (checkIsDelete != null) checkIsDelete.Checked = charterPrice.IsDeleted;
            }
        }

        public void SaveDataConfig(SailsModule module, Cruise cruise, QCruiseGroup group, string agentLvCode, int trip, QQuotation qQuotation)
        {
            litCruiseName.Text = cruise.Name;
            _module = module;
            _qQuotation = qQuotation;
            _trip = trip;
            _agentLvCode = agentLvCode;
            foreach (RepeaterItem item in rptCharterRanger.Items)
            {
                var charterPrice = new QCharterPrice();
                HiddenField hidCharterPriceId = item.FindControl("hidCharterPriceId") as HiddenField;

                TextBox txtValidFrom = item.FindControl("txtValidFrom") as TextBox;
                TextBox txtValidTo = item.FindControl("txtValidTo") as TextBox;
                TextBox txtPriceUSD = item.FindControl("txtPriceUSD") as TextBox;
                TextBox txtPriceVND = item.FindControl("txtPriceVND") as TextBox;
                CheckBox checkIsDelete = item.FindControl("checkIsDelete") as CheckBox;


                if (hidCharterPriceId != null && (!string.IsNullOrEmpty(hidCharterPriceId.Value) && hidCharterPriceId.Value != "0"))
                {
                    charterPrice = _module.GetById<QCharterPrice>(Convert.ToInt32(hidCharterPriceId.Value));
                }
                if (txtValidFrom != null && !string.IsNullOrEmpty(txtValidFrom.Text))
                {
                    charterPrice.Validfrom = Convert.ToInt32(txtValidFrom.Text);
                }
                if (txtValidTo != null && !string.IsNullOrEmpty(txtValidTo.Text))
                {
                    charterPrice.Validto = Convert.ToInt32(txtValidTo.Text);
                }

                if (txtPriceUSD != null && !string.IsNullOrEmpty(txtPriceUSD.Text))
                {
                    charterPrice.Priceusd = Convert.ToDecimal(txtPriceUSD.Text);
                }
                if (txtPriceVND != null && !string.IsNullOrEmpty(txtPriceVND.Text))
                {
                    charterPrice.Pricevnd = Convert.ToDecimal(txtPriceVND.Text);
                }
                charterPrice.QQuotation = qQuotation;
                charterPrice.Trip = trip;
                charterPrice.AgentLevelCode = agentLvCode;
                charterPrice.Cruise = cruise;
                charterPrice.GroupCruise = group;
                if (checkIsDelete.Checked) module.Delete(charterPrice);
                else
                {
                    module.SaveOrUpdate(charterPrice);
                }
            }
        }

    }
}