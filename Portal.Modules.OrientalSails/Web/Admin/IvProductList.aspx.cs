using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    /// <summary>
    /// danh sách sản phẩm
    /// </summary>
    public partial class IvProductList : SailsAdminBase
    {
        protected IvStorage _ivStorage = new IvStorage();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Sản phẩm";
            pagerProduct.AllowCustomPaging = true;
            pagerProduct.PageSize = 20;
            if (!IsPostBack)
            {
                FillCateData();
                FillSearchData();
                FillProduct();
            }
        }

        private void FillProduct()
        {
            int count = 0;
            rptProduct.DataSource = Module.IvProductGetByQuery(Request.QueryString, pagerProduct.PageSize, pagerProduct.CurrentPageIndex, out count);
            rptProduct.DataBind();
            pagerProduct.VirtualItemCount = count;
        }

        private void FillSearchData()
        {
            if (!string.IsNullOrWhiteSpace(Request["name"]))
                txtName.Text = Request["name"];
            if (!string.IsNullOrWhiteSpace(Request["cateId"]))
                ddlCategory.SelectedValue = Request["cateId"];
        }

        private void FillCateData()
        {
            ddlCategory.DataSource = Module.IvCategoryGetAll(null);
            ddlCategory.DataValueField = "Id";
            ddlCategory.DataTextField = "NameTree";
            ddlCategory.DataBind();
            ddlCategory.Items.Insert(0, new ListItem(" -- chọn --", ""));

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            var url = "IvProductList.aspx" + base.GetBaseQueryString();
            if (_ivStorage.Id > 0)
                url += "&storageId=" + _ivStorage.Id;
            if (!string.IsNullOrWhiteSpace(ddlCategory.SelectedValue))
                url += "&cateId=" + ddlCategory.SelectedValue;
            if (!string.IsNullOrWhiteSpace(txtName.Text))
                url += "&name=" + txtName.Text.Trim();
            Response.Redirect(url);
        }

        protected void rptProduct_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is IvProduct)
            {
                var product = e.Item.DataItem as IvProduct;
                var litCategory = e.Item.FindControl("litCategory") as Literal;
                if (litCategory != null)
                {
                    litCategory.Text = product.Category.Name;
                }
                var litUnit = e.Item.FindControl("litUnit") as Literal;
                if (litUnit != null)
                {
                    if (product.Unit != null) litUnit.Text = product.Unit.Name;
                }
            }
        }
    }
}