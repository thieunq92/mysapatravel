using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    /// <summary>
    /// cấu hình giá sản phẩm
    /// </summary>
    public partial class IvProductPriceList : SailsAdminBasePage
    {
        #region --- PAGE EVENT ---
        protected IvStorage IvStorage = new IvStorage();

        protected virtual void Page_Load(object sender, EventArgs e)
        {
            Page.Title = @"Danh sách sản phẩm";
            pagerProduct.AllowCustomPaging = true;
            pagerProduct.PageSize = 20;
            if (!string.IsNullOrWhiteSpace(Request["storageId"]))
            {
                IvStorage = Module.GetById<IvStorage>(Convert.ToInt32(Request["storageId"]));
            }
            if (!IsPostBack)
            {
                LoadStorage();
                FillQueryToForm();
                BindrptProduct();
            }
        }
        private void LoadStorage()
        {
            ddlStorage.DataSource = Module.IvStorageGetByUser(UserIdentity);
            ddlStorage.DataTextField = "Name";
            ddlStorage.DataValueField = "Id";
            ddlStorage.DataBind();
            //            ddlStorage.Items.Insert(0, new ListItem("-- Chọn kho --", ""));
        }
        #endregion

        #region --- CONTROL EVENT ---

        protected void rptProductList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                //RepeaterOrder.FILE_NAME = "SaleProductInStock.aspx";
                //RepeaterOrder.SetOrderLink(e, "Name", Request.QueryString);
                //RepeaterOrder.SetOrderLink(e, "BarCode", Request.QueryString);
                //RepeaterOrder.SetOrderLink(e, "Manufacturer", Request.QueryString);
                //RepeaterOrder.SetOrderLink(e, "SalePrice", Request.QueryString);

                return;
            }

            var ivInStock = (IvInStock)e.Item.DataItem;

            HyperLink aName = e.Item.FindControl("hplName") as HyperLink;
            if (aName != null)
            {
                aName.Text = ivInStock.Name;
                aName.Attributes.CssStyle.Add("cursor", "pointer");
                aName.NavigateUrl = "javascript:;";
            }
            var lblName = e.Item.FindControl("lblName") as Label;
            if (lblName != null)
            {
                lblName.Text = ivInStock.Name;
            }

            Label lblCode = e.Item.FindControl("lblCode") as Label;
            if (lblCode != null)
            {
                lblCode.Text = ivInStock.Code;
            }
            Label lblStorage = e.Item.FindControl("lblStorage") as Label;
            if (lblStorage != null)
            {
                lblStorage.Text = ivInStock.StorageName;
            }
            if (ivInStock.UnitId > 0)
            {
                var unit = Module.GetById<IvUnit>(ivInStock.UnitId);
                var list = new List<IvUnit>(unit.ListChild) { unit };
                var listPrice = new List<IvProductPrice>();
                foreach (IvUnit ivUnit in list)
                {
                    var product = Module.GetById<IvProduct>(ivInStock.Id);
                    var storage = Module.GetById<IvStorage>(ivInStock.StorageId);
                    var price = Module.GetProductPrice(product, ivUnit, storage);
                    if (price != null) listPrice.Add(price);
                    else listPrice.Add(new IvProductPrice() { Product = product, Storage = storage, Unit = ivUnit });
                }
                var rptPrice = e.Item.FindControl("rptPrice") as Repeater;
                if (rptPrice != null)
                {
                    rptPrice.DataSource = listPrice;
                    rptPrice.DataBind();
                }
            }
        }
        private void FillQueryToForm()
        {
            if (!string.IsNullOrEmpty(Request["name"]))
            {
                txtNameSearch.Text = Request["name"];
            }

            if (!string.IsNullOrEmpty(Request["code"]))
            {
                txtCodeSearch.Text = Request["code"];
            }

            //if (!string.IsNullOrEmpty(Request["pricef"]))
            //{
            //    txtPriceF.Text = Request["pricef"];
            //}

            if (!string.IsNullOrEmpty(Request["storageId"]))
            {
                ddlStorage.SelectedValue = Request["storageId"];
                ddlStorage.Enabled = false;
            }
        }
        protected virtual void btnSearch_Click(object sender, EventArgs e)
        {
            string path = string.Format("IvProductPriceList.aspx?NodeId={0}&SectionId={1}", Node.Id, Section.Id);

            string query = string.Empty;

            if (!string.IsNullOrEmpty(txtNameSearch.Text))
            {
                query += "&name=" + txtNameSearch.Text;
            }

            if (!string.IsNullOrEmpty(txtCodeSearch.Text))
            {
                query += "&code=" + txtCodeSearch.Text;
            }

            //if (!string.IsNullOrEmpty(txtPriceF.Text))
            //{
            //    query += "&pricef=" + txtPriceF.Text;
            //}
            if (!string.IsNullOrEmpty(ddlStorage.SelectedValue))
            {
                query += "&storageId=" + ddlStorage.SelectedValue;
            }
            PageRedirect(path + query);
        }

        #endregion

        #region --- PRIVATE METHOD ---

        private void BindrptProduct()
        {
            int count;
            var query = new NameValueCollection(Request.QueryString);
            if (string.IsNullOrWhiteSpace(query["storageId"]))
                query.Add("storageId", ddlStorage.SelectedValue);
            rptProductList.DataSource = Module.GetProductInStock(query, UserIdentity, pagerProduct.PageSize,
                                                                       pagerProduct.CurrentPageIndex, out count);
            pagerProduct.VirtualItemCount = count;
            rptProductList.DataBind();
        }

        #endregion

        protected void btnSavePrice_OnClick(object sender, EventArgs e)
        {
            foreach (RepeaterItem productItem in rptProductList.Items)
            {
                var rptPrice = productItem.FindControl("rptPrice") as Repeater;
                if (rptPrice != null)
                {
                    foreach (RepeaterItem priceItem in rptPrice.Items)
                    {
                        HiddenField hidProductPriceId = priceItem.FindControl("hidProductPriceId") as HiddenField;
                        HiddenField hidProductId = priceItem.FindControl("hidProductId") as HiddenField;
                        HiddenField hidUnitId = priceItem.FindControl("hidUnitId") as HiddenField;
                        HiddenField hidStorageId = priceItem.FindControl("hidStorageId") as HiddenField;
                        TextBox txtPrice = priceItem.FindControl("txtPrice") as TextBox;
                        if (hidProductPriceId != null && !string.IsNullOrWhiteSpace(hidProductPriceId.Value) && Convert.ToInt32(hidProductPriceId.Value) > 0)
                        {
                            var productPrice = Module.GetById<IvProductPrice>(Convert.ToInt32(hidProductPriceId.Value));
                            if (txtPrice != null) productPrice.Price = Convert.ToDouble(txtPrice.Text);
                            Module.SaveOrUpdate(productPrice);
                        }
                        else
                        {
                            var product = Module.GetById<IvProduct>(Convert.ToInt32(hidProductId.Value));
                            var storage = Module.GetById<IvStorage>(Convert.ToInt32(hidStorageId.Value));
                            var unit = Module.GetById<IvUnit>(Convert.ToInt32(hidUnitId.Value));
                            var productPrice = new IvProductPrice() { Product = product, Storage = storage, Unit = unit };
                            if (txtPrice != null && !string.IsNullOrWhiteSpace(txtPrice.Text))
                                productPrice.Price = Convert.ToDouble(txtPrice.Text);
                            Module.SaveOrUpdate(productPrice);

                        }
                    }
                }
            }
        }
    }
}