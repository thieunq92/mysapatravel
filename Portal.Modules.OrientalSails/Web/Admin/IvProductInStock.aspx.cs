using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CMS.Core.Util;
using Common.Logging.Configuration;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;
using NameValueCollection = System.Collections.Specialized.NameValueCollection;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    /// <summary>
    /// tình trạng sản phẩm trong kho
    /// </summary>
    public partial class IvProductInStock : SailsAdminBasePage
    {
        #region --- PAGE EVENT ---
        protected IvStorage _ivStorage = new IvStorage();

        protected virtual void Page_Load(object sender, EventArgs e)
        {
            Page.Title = @"Danh sách sản phẩm";
            pagerProduct.AllowCustomPaging = true;
            pagerProduct.PageSize = 20;
            if (!string.IsNullOrWhiteSpace(Request["storageId"]))
            {
                _ivStorage = Module.GetById<IvStorage>(Convert.ToInt32(Request["storageId"]));
            }
            if (!IsPostBack)
            {
                LoadCruise();
                LoadStorage();
                FillQueryToForm();
                BindrptProduct();
            }
        }

        private void LoadCruise()
        {
            ddlCruise.DataSource = Module.CruiseGetByUser(UserIdentity);
            ddlCruise.DataTextField = "Name";
            ddlCruise.DataValueField = "Id";
            ddlCruise.DataBind();
            //            ddlStorage.Items.Insert(0, new ListItem("-- Chọn tàu --", ""));
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

            var product = (IvInStock)e.Item.DataItem;

            HyperLink aName = e.Item.FindControl("hplName") as HyperLink;
            if (aName != null)
            {
                aName.Text = product.Name;
                aName.Attributes.CssStyle.Add("cursor", "pointer");
                aName.NavigateUrl = "javascript:;";
                string script = string.Format("Done('{0}','{1}')", product.Name.Trim().Replace("'", @"\'").Replace("\"", ""), product.Id);
                aName.Attributes.Add("onclick", script);
            }
            var lblName = e.Item.FindControl("lblName") as Label;
            if (lblName != null)
            {
                lblName.Text = product.Name;
            }


            Label lblCode = e.Item.FindControl("lblCode") as Label;
            if (lblCode != null)
            {
                lblCode.Text = product.Code;
            }
            Label lblStorage = e.Item.FindControl("lblStorage") as Label;
            if (lblStorage != null)
            {
                lblStorage.Text = product.StorageName;
            }
            HyperLink hplEdit = e.Item.FindControl("hplEdit") as HyperLink;
            if (hplEdit != null)
            {
                hplEdit.NavigateUrl = string.Format("IvProductAdd.aspx?NodeId={0}&SectionId={1}&ProductId={2}", Node.Id,
                                                    Section.Id, product.Id);
            }
            //Check Hàng tồn kho

            Label lblUnitInstock = e.Item.FindControl("lblUnitInstock") as Label;
            var trItem = e.Item.FindControl("trItem") as HtmlControl;
            if (lblUnitInstock != null)
            {
                var result = product.Quantity;
                if (result == 0.0)
                {
                    lblUnitInstock.Text = @"Hết hàng";
                    //lblUnitInstock.Parent.Visible = false;
                }
                else
                {

                    double warningLimit = product.WarningLimit;
                    if (warningLimit > 0 && warningLimit >= result)
                    {
                        trItem.Attributes.Add("style", "background: #ff572291;color: #fff;");
                    }

                    lblUnitInstock.Text = result.ToString("#,0.#");
                }
            }
            Label lblUnit = e.Item.FindControl("lblUnit") as Label;
            if (lblUnit != null)
            {
                lblUnit.Text = product.UnitName;
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

            if (!string.IsNullOrEmpty(Request["cruiseId"]))
            {
                ddlCruise.SelectedValue = Request["cruiseId"];
            }
            if (!string.IsNullOrEmpty(Request["storageId"]))
            {
                ddlStorage.SelectedValue = Request["storageId"];
            }
        }
        protected virtual void btnSearch_Click(object sender, EventArgs e)
        {
            string path = string.Format("IvProductInStock.aspx?NodeId={0}&SectionId={1}", Node.Id, Section.Id);

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

            if (!string.IsNullOrEmpty(ddlCruise.SelectedValue))
            {
                query += "&cruiseId=" + ddlCruise.SelectedValue;
            }
            if (!string.IsNullOrEmpty(ddlStorage.SelectedValue))
            {
                query += "&storageId=" + ddlStorage.SelectedValue;
            }
            PageRedirect(path + query);
        }

        #endregion

        #region --- PRIVATE METHOD ---

        public virtual void BindrptProduct()
        {
            int count;
            var query = new NameValueCollection(Request.QueryString);
            if (string.IsNullOrWhiteSpace(query["storageId"]))
                query.Add("storageId", ddlStorage.SelectedValue);
            var list = Module.GetProductInStock(query, UserIdentity, pagerProduct.PageSize,
                                                                       pagerProduct.CurrentPageIndex, out count);
            rptProductList.DataSource = list;
            pagerProduct.VirtualItemCount = count;
            rptProductList.DataBind();
        }

        #endregion

        protected void ddlCRuise_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var list = (IList<IvStorage>)Module.IvStorageGetByUser(UserIdentity);
            ddlStorage.DataSource = list.Where(s => s.Cruise.Id == Convert.ToInt32(ddlCruise.SelectedValue));
            ddlStorage.DataTextField = "Name";
            ddlStorage.DataValueField = "Id";
            ddlStorage.DataBind();
        }
    }
}