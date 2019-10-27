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
    /// danh sách kho
    /// </summary>
    public partial class IvStorageList : SailsAdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Kho";
            if (!IsPostBack)
            {
                FillParentData();
                FillSearchData();
                FillStorage();
            }
        }

        private void FillSearchData()
        {
            if (!string.IsNullOrWhiteSpace(Request["name"]))
                txtName.Text = Request["name"];
            if (!string.IsNullOrWhiteSpace(Request["parentId"]))
                ddlParent.SelectedValue = Request["parentId"];
        }

        private void FillParentData()
        {
            ddlParent.DataSource = Module.IvStorageGetAll(null);
            ddlParent.DataValueField = "Id";
            ddlParent.DataTextField = "NameTree";
            ddlParent.DataBind();
            ddlParent.Items.Insert(0, new ListItem(" -- Chọn --", ""));

        }
        private void FillStorage()
        {
            IList list = new List<IvStorage>();

            if (!string.IsNullOrWhiteSpace(Request["name"]) || !string.IsNullOrWhiteSpace(Request["parentId"]))
            {
                list = Module.IvStorageGetByQuery(Request.QueryString);
            }
            else
            {
                list = Module.IvStorageGetAll(null);
            }
            rptStorage.DataSource = list;
            rptStorage.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var url = "IvStorageList.aspx" + base.GetBaseQueryString();
            if (!string.IsNullOrWhiteSpace(txtName.Text))
                url += "&name=" + txtName.Text.Trim();
            if (!string.IsNullOrWhiteSpace(ddlParent.SelectedValue))
                url += "&parentId=" + ddlParent.SelectedValue;
            Response.Redirect(url);

        }

        protected void rptStorage_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is IvStorage)
            {
                var storage = e.Item.DataItem as IvStorage;
                var litParent = e.Item.FindControl("litParent") as Literal;
                if (litParent != null)
                {
                    if (storage.Parent != null) litParent.Text = storage.Parent.Name;
                }
            }
        }
    }
}