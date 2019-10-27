using System;
using System.Collections.Generic;
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
    /// báo cáo sản phẩm sắp hết
    /// </summary>
    public partial class IvReportWarningLimit : SailsAdminBasePage
    {
        #region --- PAGE EVENT ---
        protected IvStorage _ivStorage = new IvStorage();

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = @"Cảnh báo sản phẩm sắp hết";
            if (!string.IsNullOrWhiteSpace(Request["storageId"]))
            {
                _ivStorage = Module.GetById<IvStorage>(Convert.ToInt32(Request["storageId"]));
            }
            if (!IsPostBack)
            {
                LoadStorage();
                FillQueryToForm();
                if (!string.IsNullOrWhiteSpace(ddlStorage.SelectedValue))
                    BindrptProduct();
            }
        }



        private void LoadStorage()
        {
            ddlStorage.DataSource = Module.IvStorageGetByUser(UserIdentity);
            ddlStorage.DataTextField = "Name";
            ddlStorage.DataValueField = "Id";
            ddlStorage.DataBind();
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

            IvProduct product = (IvProduct)e.Item.DataItem;

            HyperLink hplName = e.Item.FindControl("hplName") as HyperLink;
            if (hplName != null)
            {
                hplName.Text = product.Name;
                hplName.NavigateUrl = string.Format("IvProductAdd.aspx?NodeId={0}&SectionID={1}&ProductID={2}", Node.Id,
                                                    Section.Id, product.Id);
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
            HyperLink hplEdit = e.Item.FindControl("hplEdit") as HyperLink;
            if (hplEdit != null)
            {
                hplEdit.NavigateUrl = string.Format("IvProductAdd.aspx?NodeId={0}&SectionId={1}&ProductId={2}", Node.Id,
                                                    Section.Id, product.Id);
            }

            //Check Hàng tồn kho

            var trItem = e.Item.FindControl("trItem") as HtmlControl;
            if (trItem != null) trItem.Attributes.Add("style", "background: #ff572291;color: #fff;");
            Label lblUnitInstock = e.Item.FindControl("lblUnitInstock") as Label;

            if (lblUnitInstock != null)
            {
                lblUnitInstock.Text = product.NumberInStock.ToString("#,0.#");
            }
            Label lblWarningLimit = e.Item.FindControl("lblWarningLimit") as Label;

            if (lblWarningLimit != null)
            {
                lblWarningLimit.Text = product.WarningLimit.ToString();
            }
            Label lblUnit = e.Item.FindControl("lblUnit") as Label;

            if (lblUnit != null)
            {
                if (product.Unit != null) lblUnit.Text = product.Unit.Name;
            }
        }
        private void FillQueryToForm()
        {
            //if (!string.IsNullOrEmpty(Request["name"]))
            //{
            //    txtNameSearch.Text = Request["name"];
            //}

            //if (!string.IsNullOrEmpty(Request["code"]))
            //{
            //    txtCodeSearch.Text = Request["code"];
            //}

            //if (!string.IsNullOrEmpty(Request["pricef"]))
            //{
            //    txtPriceF.Text = Request["pricef"];
            //}

            //if (!string.IsNullOrEmpty(Request["pricet"]))
            //{
            //    txtPriceT.Text = Request["pricet"];
            //}
            if (!string.IsNullOrEmpty(Request["storageId"]))
            {
                ddlStorage.SelectedValue = Request["storageId"];
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string path = string.Format("IvReportWarningLimit.aspx?NodeId={0}&SectionId={1}", Node.Id, Section.Id);

            string query = string.Empty;

            //if (!string.IsNullOrEmpty(txtNameSearch.Text))
            //{
            //    query += "&name=" + txtNameSearch.Text;
            //}

            //if (!string.IsNullOrEmpty(txtCodeSearch.Text))
            //{
            //    query += "&code=" + txtCodeSearch.Text;
            //}

            //if (!string.IsNullOrEmpty(txtPriceF.Text))
            //{
            //    query += "&pricef=" + txtPriceF.Text;
            //}

            //if (!string.IsNullOrEmpty(txtPriceT.Text))
            //{
            //    query += "&pricet=" + txtPriceT.Text;
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
            if (_ivStorage.Id <= 0)
                _ivStorage = Module.GetById<IvStorage>(Convert.ToInt32(ddlStorage.SelectedValue));
            var list = Module.GetProductWarningByStorage(_ivStorage);
            var products = new List<IvProduct>();
            foreach (IvProductWarning productWarning in list)
            {
                var product = productWarning.Product;
                int sumImport = Module.SumProductImport(_ivStorage, product);
                double sumExport = Module.SumProductExport(_ivStorage, product);
                var result = Convert.ToDouble(sumImport) - Convert.ToDouble(sumExport);
                double warningLimit = Module.GetWarningLimit(_ivStorage, product);
                if (warningLimit > 0 && warningLimit >= result)
                {
                    product.WarningLimit = Convert.ToInt32(warningLimit);
                    product.NumberInStock = result;
                    products.Add(product);
                }
            }
            rptProductList.DataSource = products;
            rptProductList.DataBind();
        }
        #endregion
    }
}