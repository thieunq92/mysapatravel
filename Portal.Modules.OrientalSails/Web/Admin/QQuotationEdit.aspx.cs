using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    /// <summary>
    /// thêm,sửa 1 cấu hình giá thuê phòng, tàu
    /// </summary>
    public partial class QQuotationEdit : SailsAdminBasePage
    {
        private QQuotation _qQuotation = new QQuotation();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Request["qid"]))
            {
                _qQuotation = Module.GetById<QQuotation>(Convert.ToInt32(Request["qid"]));
            }
            if (!IsPostBack)
            {
                GetGroupCruise();
                if (_qQuotation.Id > 0)
                {
                    FillInfoQuotation();
                }
                SetAgentLevel();
            }
        }

        private void SetAgentLevel()
        {
            if (!string.IsNullOrWhiteSpace(ddlGroupCruise.SelectedValue))
            {
                ucAgentLevel1.SetGroupCruise("AL1", Module, Convert.ToInt32(ddlGroupCruise.SelectedValue), _qQuotation);
                ucAgentLevel2.SetGroupCruise("AL2", Module, Convert.ToInt32(ddlGroupCruise.SelectedValue), _qQuotation);
                ucAgentLevel3.SetGroupCruise("AL3", Module, Convert.ToInt32(ddlGroupCruise.SelectedValue), _qQuotation);
            }
        }

        private void FillInfoQuotation()
        {
            if (_qQuotation.GroupCruise != null) ddlGroupCruise.SelectedValue = _qQuotation.GroupCruise.Id.ToString();
            txtValidFrom.Text = _qQuotation.Validfrom.ToString("dd/MM/yyyy");
            txtValidTo.Text = _qQuotation.Validto.ToString("dd/MM/yyyy");
        }

        private void GetGroupCruise()
        {
            ddlGroupCruise.DataSource = Module.GetCruiseGroup();
            ddlGroupCruise.DataTextField = "Name";
            ddlGroupCruise.DataValueField = "Id";
            ddlGroupCruise.DataBind();
        }

        protected void btnCreateQuotation_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(ddlGroupCruise.SelectedValue))
                _qQuotation.GroupCruise = Module.GetById<QCruiseGroup>(Convert.ToInt32(ddlGroupCruise.SelectedValue));
            if (!string.IsNullOrWhiteSpace(txtValidFrom.Text))
            {
                _qQuotation.Validfrom = DateTime.ParseExact(txtValidFrom.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrWhiteSpace(txtValidTo.Text))
            {
                _qQuotation.Validto = DateTime.ParseExact(txtValidTo.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            if (string.IsNullOrWhiteSpace(Request["qid"]))
            {
                _qQuotation.CreatedBy = UserIdentity;
                _qQuotation.CreatedDate = DateTime.Now;
            }
            _qQuotation.ModifiedBy = UserIdentity;
            _qQuotation.ModifiedDate = DateTime.Now;
            _qQuotation.Enable = false;
            Module.SaveOrUpdate(_qQuotation);
            if (!string.IsNullOrWhiteSpace(ddlGroupCruise.SelectedValue))
            {
                ucAgentLevel1.SaveAgentLevelPriceConfig("AL1", Module, Convert.ToInt32(ddlGroupCruise.SelectedValue), _qQuotation);
                ucAgentLevel2.SaveAgentLevelPriceConfig("AL2", Module, Convert.ToInt32(ddlGroupCruise.SelectedValue), _qQuotation);
                ucAgentLevel3.SaveAgentLevelPriceConfig("AL3", Module, Convert.ToInt32(ddlGroupCruise.SelectedValue), _qQuotation);
            }
            _qQuotation.Enable = true;
            Module.SaveOrUpdate(_qQuotation);
            Response.Redirect("QQuotationList.aspx" + GetBaseQueryString());
        }

        protected void ddlGroupCruise_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            SetAgentLevel();
        }
    }
}