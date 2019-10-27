using System;
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
    /// thêm, sửa đơn vị tính
    /// </summary>
    public partial class IvUnitAdd : SailsAdminBase
    {
        private IvUnit _ivUnit = new IvUnit();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Đơn vị tính";
            if (!string.IsNullOrWhiteSpace(Request["id"]))
            {
                _ivUnit = Module.IvUnitGetById(Convert.ToInt32(Request["id"]));
            }
            if (!IsPostBack)
            {
                FillParent();
                FillInfo();
            }
        }

        private void FillParent()
        {
            ddlParent.DataSource = Module.IvUnitGetAllParent(_ivUnit);
            ddlParent.DataValueField = "Id";
            ddlParent.DataTextField = "Name";
            ddlParent.DataBind();
            ddlParent.Items.Insert(0, new ListItem(" -- chọn --", ""));
        }

        private void FillInfo()
        {
            txtName.Text = _ivUnit.Name;
            txtRate.Text = _ivUnit.Rate.ToString();
            txtNote.Text = _ivUnit.Note;
            ddlMath.SelectedValue = _ivUnit.Math;
            if (_ivUnit.Parent != null)
            {
                ddlParent.SelectedValue = _ivUnit.Parent.Id.ToString();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            _ivUnit.Name = txtName.Text;
            if (!string.IsNullOrWhiteSpace(txtRate.Text))
                _ivUnit.Rate = Convert.ToInt32(txtRate.Text);
            _ivUnit.Note = txtNote.Text;
            _ivUnit.Math = ddlMath.SelectedValue;
            if (!string.IsNullOrWhiteSpace(ddlParent.SelectedValue))
                _ivUnit.Parent = Module.GetById<IvUnit>(Convert.ToInt32(ddlParent.SelectedValue));
            Module.SaveOrUpdate(_ivUnit);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "RefreshParentPage", "RefreshParentPage();", true);
        }
    }
}