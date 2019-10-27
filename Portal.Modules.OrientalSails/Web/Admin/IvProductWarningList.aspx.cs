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
    /// danh sách cảnh báo sản phẩm sắp hết
    /// </summary>
    public partial class IvProductWarningList : SailsAdminBase
    {
        protected IvStorage _ivStorage = new IvStorage();
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Sản phẩm";
            pagerProduct.AllowCustomPaging = true;
            pagerProduct.PageSize = 20;
            if (!string.IsNullOrWhiteSpace(Request["storageId"]))
            {
                _ivStorage = Module.GetById<IvStorage>(Convert.ToInt32(Request["storageId"]));
                litStorage.Text = _ivStorage.Name;
            }
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
            var url = "IvProductWarningList.aspx" + base.GetBaseQueryString();
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
                var ddlWarning = (DropDownList)e.Item.FindControl("ddlWarning");
                if (ddlWarning != null)
                {
                    for (int i = 1; i <= 100; i++)
                    {
                        ddlWarning.Items.Add(new ListItem(i.ToString(), i.ToString()));
                    }
                    ddlWarning.Items.Insert(0, new ListItem("--- Chọn ---", ""));
                }
                var productWarning = Module.GetProductWarning(product, _ivStorage);
                if (productWarning != null)
                {
                    if (ddlWarning != null) ddlWarning.SelectedValue = productWarning.WarningLimit.ToString();
                    var hdProductWarningId = e.Item.FindControl("hdProductWarningId") as HiddenField;
                    if (hdProductWarningId != null)
                        hdProductWarningId.Value = productWarning.Id.ToString();
                }
            }
        }

        protected void btnSaveLimit_OnClick(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in rptProduct.Items)
            {
                var hdProductWarningId = item.FindControl("hdProductWarningId") as HiddenField;
                var hdproductId = item.FindControl("hdproductId") as HiddenField;
                var ddlWarning = (DropDownList)item.FindControl("ddlWarning");
                if (ddlWarning != null & hdProductWarningId != null && hdproductId != null)
                {
                    var product = Module.GetById<IvProduct>(Convert.ToInt32(hdproductId.Value));
                    var productWarning = new IvProductWarning();
                    if (!string.IsNullOrWhiteSpace(hdProductWarningId.Value))
                        productWarning = Module.GetById<IvProductWarning>(Convert.ToInt32(hdProductWarningId.Value));
                    if (!string.IsNullOrWhiteSpace(ddlWarning.SelectedValue))
                    {
                        productWarning.Product = product;
                        productWarning.Storage = _ivStorage;
                        productWarning.WarningLimit = Convert.ToInt32(ddlWarning.SelectedValue);
                        Module.SaveOrUpdate(productWarning);
                    }
                    if (!string.IsNullOrWhiteSpace(hdProductWarningId.Value) && string.IsNullOrWhiteSpace(ddlWarning.SelectedValue))
                    {
                        Module.Delete(productWarning);
                    }
                }
            }
        }
    }
}