using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CMS.Core.Util;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class IvProductInStockSelectPage : IvProductInStock
    {
        /// <summary>
        /// trang chọn sản phẩm trong kho
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
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
                }}", clientId, clientNameId, Request["OrtherControlId"].Trim(), Request["OrtherControlName"]);
            }

            Page.ClientScript.RegisterClientScriptBlock(typeof(IvProductInStockSelectPage), "done", script, true);
           
        }
        protected override void btnSearch_Click(object sender, EventArgs e)
        {
            string path = string.Format("IvProductInStockSelectPage.aspx?NodeId={0}&SectionId={1}", Node.Id, Section.Id);

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

            //if (!string.IsNullOrEmpty(txtPriceT.Text))
            //{
            //    query += "&pricet=" + txtPriceT.Text;
            //}
            if (!string.IsNullOrEmpty(ddlCruise.SelectedValue))
            {
                query += "&cruiseId=" + ddlCruise.SelectedValue;
            }
            if (!string.IsNullOrEmpty(ddlStorage.SelectedValue))
            {
                query += "&storageId=" + ddlStorage.SelectedValue;
            }
            if (!string.IsNullOrEmpty(Request["clientid"]))
                query += "&clientid=" + Request.QueryString["clientid"].Trim();
            if (!string.IsNullOrEmpty(Request["clientNameId"]))
                query += "&clientNameId=" + Request.QueryString["clientNameId"].Trim();
            if (!string.IsNullOrEmpty(Request["OrtherControlId"]))
                query += "&OrtherControlId=" + Request.QueryString["OrtherControlId"].Trim();
            PageRedirect(path + query);
        }
        public override void BindrptProduct()
        {
            int count;
            var query = new NameValueCollection(Request.QueryString);
            if (string.IsNullOrWhiteSpace(query["storageId"]))
                query.Add("storageId", ddlStorage.SelectedValue);
            rptProductList.DataSource = Module.GetSelectProductInStock(query, UserIdentity, pagerProduct.PageSize,
                pagerProduct.CurrentPageIndex, out count);
            pagerProduct.VirtualItemCount = count;
            rptProductList.DataBind();
        }
    }
}