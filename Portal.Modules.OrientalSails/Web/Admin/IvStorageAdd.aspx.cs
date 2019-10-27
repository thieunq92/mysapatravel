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
    /// thêm sửa kho
    /// </summary>
    public partial class IvStorageAdd : SailsAdminBase
    {
        private IvStorage _storage = new IvStorage();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Kho";
            if (!string.IsNullOrWhiteSpace(Request["id"]))
            {
                _storage = Module.IvStorageGetById(Convert.ToInt32(Request["id"]));
            }
            if (!IsPostBack)
            {
                FillParentData();
                if (_storage.Id > 0) FillInfo();
            }
        }

        private void FillInfo()
        {
            txtName.Text = _storage.Name;
            txtNote.Text = _storage.Note;
            chkIsInventoryTracking.Checked = _storage.IsInventoryTracking;
            if (_storage.Cruise != null)
                ddlCruise.SelectedValue = _storage.Cruise.Id.ToString();
            if (_storage.Parent != null)
            {
                ddlParent.SelectedValue = _storage.Parent.Id.ToString();
            }
        }

        private void FillParentData()
        {

            ddlParent.DataSource = Module.IvStorageGetAll(_storage);
            ddlParent.DataValueField = "Id";
            ddlParent.DataTextField = "NameTree";
            ddlParent.DataBind();
            ddlParent.Items.Insert(0, new ListItem(" -- chọn kho --", ""));

            ddlCruise.DataSource = Module.CruiseGetAll();
            ddlCruise.DataValueField = "Id";
            ddlCruise.DataTextField = "Name";
            ddlCruise.DataBind();
            ddlCruise.Items.Insert(0, new ListItem(" -- chọn tàu --", ""));

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            _storage.Name = txtName.Text;
            _storage.Note = txtNote.Text;
            _storage.IsInventoryTracking = chkIsInventoryTracking.Checked;
            if (!string.IsNullOrWhiteSpace(ddlParent.SelectedValue))
            {
                _storage.Parent = Module.IvStorageGetById(Convert.ToInt32(ddlParent.SelectedValue));
            }
            if (!string.IsNullOrWhiteSpace(ddlCruise.SelectedValue))
            {
                _storage.Cruise = Module.CruiseGetById(Convert.ToInt32(ddlCruise.SelectedValue));
            }
            Module.SaveOrUpdate(_storage);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "RefreshParentPage", "RefreshParentPage();", true);
        }
    }
}