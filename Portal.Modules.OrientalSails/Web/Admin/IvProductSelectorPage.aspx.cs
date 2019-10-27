using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.Core.Util;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class IvProductSelectorPage : SailsAdminBasePage
    {
        /// <summary>
        /// Chọn sản phẩm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            string clientId = Request.QueryString["clientid"].Trim();
            string clientNameId = Request.QueryString["clientNameId"].Trim();
            string script = "";
            if (string.IsNullOrEmpty(Request["OrtherControlId"]))
            {
                script = string.Format(
                    @"function Done(name, id)
                {{
                    idcontrol = window.opener.document.getElementById('{0}');
	                idcontrol.value = id;
                    idcontrol.focus();
                    namecontrol = window.opener.document.getElementById('{1}');
                    if(namecontrol)namecontrol.textContent  = name;    
                    window.close();
                }}", clientId, clientNameId);
            }
            else
            {
                script = string.Format(
                    @"function Done(name, id)
                {{
                    idcontrol = window.opener.document.getElementById('{0}');
	                idcontrol.value = id;
                    namecontrol = window.opener.document.getElementById('{1}');
                    namecontrol.value = name;    
                    OrtherControlId = window.opener.document.getElementById('{2}');
                    OrtherControlId.value = id;
                    window.opener.__doPostBack('{3}','');
                    window.close();
                }}", clientId, clientNameId, Request["OrtherControlId"], Request["OrtherControlName"]);
            }

            Page.ClientScript.RegisterClientScriptBlock(typeof(IvProductSelectorPage), "done", script, true);


            Page.Title = @"Danh sách sản phẩm";
            pagerProduct.AllowCustomPaging = true;
            pagerProduct.PageSize = 20;

            if (!IsPostBack)
            {
                BinddrpGroup();
                BindQueryString();
                BindrptProduct();
            }
        }
        private void BindQueryString()
        {
            if (!string.IsNullOrEmpty(Request["name"]))
            {
                txtNameSearch.Text = Request["name"];
            }

            if (!string.IsNullOrEmpty(Request["code"]))
            {
                txtCodeSearch.Text = Request["code"];
            }


            if (!string.IsNullOrEmpty(Request["cateId"]))
            {
                drpGroup.SelectedValue = Request["cateId"];
            }

        }

        #region ---CONTROL EVENT---

        protected void rptProductList_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "delete":
                    IvProduct product = Module.GetById<IvProduct>(Convert.ToInt32(e.CommandArgument));
                    Module.Delete(product);
                    BindrptProduct();
                    break;
                default:
                    break;
            }
        }

        protected void rptProductList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                //    RepeaterOrder.FILE_NAME = "IvProductSelectorPage.aspx";
                //    RepeaterOrder.SetOrderLink(e, "Name", Request.QueryString);
                //    RepeaterOrder.SetOrderLink(e, "BarCode", Request.QueryString);
                //    RepeaterOrder.SetOrderLink(e, "Manufacturer", Request.QueryString);
                //    RepeaterOrder.SetOrderLink(e, "SalePrice", Request.QueryString);
                return;
            }

            IvProduct product = (IvProduct)e.Item.DataItem;

            HyperLink aName = e.Item.FindControl("hplName") as HyperLink;
            if (aName != null)
            {
                aName.Text = product.Name;
                aName.Attributes.CssStyle.Add("cursor", "pointer");
                aName.NavigateUrl = "javascript:;";
                string script = string.Format("Done('{0}','{1}')", product.Name.Trim().Replace("'", @"\'").Replace("\"", ""), product.Id);
                aName.Attributes.Add("onclick", script);
            }
            Label lblCode = e.Item.FindControl("lblCode") as Label;
            if (lblCode != null)
            {
                lblCode.Text = product.Code;
            }
        }

        #endregion

        #region ---PRIVATE METHOD---

        private void BindrptProduct()
        {
            int count;
            rptProductList.DataSource = Module.IvProductGetByQueryString(Request.QueryString, UserIdentity, pagerProduct.PageSize,
                pagerProduct.CurrentPageIndex, out count);
            pagerProduct.VirtualItemCount = count;
            rptProductList.DataBind();
        }

        private void BinddrpGroup()
        {
            drpGroup.DataSource = Module.IvCategoryGetAll(null);
            drpGroup.DataTextField = "NameTree";
            drpGroup.DataValueField = "Id";
            drpGroup.DataBind();
            drpGroup.Items.Insert(0, "");
        }

        #endregion
        protected void btnSearch_Click(object sender, EventArgs e)
        {

            string query = string.Empty;

            if (!string.IsNullOrEmpty(txtNameSearch.Text))
            {
                query += "&name=" + txtNameSearch.Text;
            }

            if (!string.IsNullOrEmpty(txtCodeSearch.Text))
            {
                query += "&code=" + txtCodeSearch.Text;
            }
            if (drpGroup.SelectedIndex > 0)
            {
                query += "&cateId=" + drpGroup.SelectedValue;
            }
            query += "&clientid=" + Request.QueryString["clientid"];

            if (!string.IsNullOrEmpty(Request["OrtherControlId"]))
                query += "&OrtherControlId=" + Request.QueryString["OrtherControlId"].Trim();
            if (!string.IsNullOrEmpty(Request["OrtherControlName"]))
                query += "&OrtherControlName=" + Request.QueryString["OrtherControlName"].Trim();
            if (!string.IsNullOrEmpty(Request["clientid"]))
                query += "&clientid=" + Request.QueryString["clientid"].Trim();
            if (!string.IsNullOrEmpty(Request["OrtherControlName"]))
                query += "&clientNameId=" + Request.QueryString["clientNameId"].Trim();
            PageRedirect(string.Format("IvProductSelectorPage.aspx?NodeId={0}&SectionId={1}{2}", Node.Id, Section.Id, query));
        }
    }
}