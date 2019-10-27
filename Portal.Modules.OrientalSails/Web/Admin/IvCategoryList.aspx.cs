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
    /// trang danh sách danh mục sản phẩm
    /// </summary>
    public partial class IvCategoryList : SailsAdminBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Danh mục";
            if (!IsPostBack)
            {
                FillParentData();
                FillSearchData();
                FillCategory();
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
            ddlParent.DataSource = Module.IvCategoryGetAll(null);
            ddlParent.DataValueField = "Id";
            ddlParent.DataTextField = "NameTree";
            ddlParent.DataBind();
            ddlParent.Items.Insert(0, new ListItem(" -- chọn --", ""));
        }
        private void FillCategory()
        {
            IList list = new List<IvCategory>();

            if (!string.IsNullOrWhiteSpace(Request["name"]) || !string.IsNullOrWhiteSpace(Request["parentId"]))
            {
                list = Module.IvCategoryGetByQuery(Request.QueryString);
            }
            else
            {
                list = Module.IvCategoryGetAll(null);
            }
            rptCategory.DataSource = list;
            rptCategory.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var url = "IvCategoryList.aspx" + base.GetBaseQueryString();
            if (!string.IsNullOrWhiteSpace(txtName.Text))
                url += "&name=" + txtName.Text.Trim();
            if (!string.IsNullOrWhiteSpace(ddlParent.SelectedValue))
                url += "&parentId=" + ddlParent.SelectedValue;
            Response.Redirect(url);

        }

        protected void rptCategory_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is IvCategory)
            {
                var category = e.Item.DataItem as IvCategory;
                var litParent = e.Item.FindControl("litParent") as Literal;
                if (litParent != null)
                {
                    if (category.Parent != null) litParent.Text = category.Parent.Name;
                }
            }
        }
    }
}