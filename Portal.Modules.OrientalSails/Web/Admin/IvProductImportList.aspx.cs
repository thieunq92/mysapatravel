using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CMS.Core.Domain;
using CMS.Core.Util;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    /// <summary>
    /// danh sách sản phẩm nhập
    /// </summary>
    public partial class IvProductImportList : SailsAdminBase
    {
        private double sumTotal;

        #region --- PAGE EVENT ---

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Danh sách phiếu nhập";
            pagerProduct.AllowCustomPaging = true;
            pagerProduct.PageSize = 20;
            if (!IsPostBack)
            {
                LoadCruise();
                LoadStorage();
                LoadInfoSearch();
                BindrptImportList();
            }
        }
        private void LoadCruise()
        {
            ddlCruise.DataSource = Module.CruiseGetByUser(UserIdentity);
            ddlCruise.DataTextField = "Name";
            ddlCruise.DataValueField = "Id";
            ddlCruise.DataBind();
            ddlCruise.Items.Insert(0, new ListItem("-- Chọn tàu --", ""));
        }
        private void LoadStorage()
        {
            ddlStorage.DataSource = Module.IvStorageGetByUser(UserIdentity);
            ddlStorage.DataTextField = "Name";
            ddlStorage.DataValueField = "Id";
            ddlStorage.DataBind();
            ddlStorage.Items.Insert(0, new ListItem("-- chọn kho --", ""));
        }
        #endregion

        #region --- CONTROL EVENT ---

        protected void rptImportList_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "delete":
                    IvImport import = Module.GetById<IvImport>(Convert.ToInt32(e.CommandArgument));
                    Module.Delete(import);
                    BindrptImportList();
                    break;
                default:
                    break;
            }

        }

        protected void rptImportList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //if (e.Item.ItemType == ListItemType.Header)
            //{
            //    RepeaterOrder.FILE_NAME = "IvProductImportList.aspx";
            //    RepeaterOrder.SetOrderLink(e, "Name", Request.QueryString);
            //    RepeaterOrder.SetOrderLink(e, "Code", Request.QueryString);
            //    RepeaterOrder.SetOrderLink(e, "ImportDate", Request.QueryString);
            //    RepeaterOrder.SetOrderLink(e, "Total", Request.QueryString);
            //    RepeaterOrder.SetOrderLink(e, "ImportFrom", Request.QueryString);
            //    RepeaterOrder.SetOrderLink(e, "ImportedBy", Request.QueryString);

            //    return;
            //}
            if (e.Item.DataItem is IvImport)
            {
                IvImport import = (IvImport)e.Item.DataItem;

                HyperLink hplName = e.Item.FindControl("hplName") as HyperLink;
                if (hplName != null)
                {
                    hplName.Text = import.Name;
                    hplName.NavigateUrl = string.Format("IvImportAdd.aspx?NodeId={0}&SectionID={1}&ImportId={2}", Node.Id, Section.Id, import.Id);
                }


                Label lblStorage = e.Item.FindControl("lblStorage") as Label;
                if (lblStorage != null)
                {
                    lblStorage.Text = import.Storage.Name;
                }
                Label lblCode = e.Item.FindControl("lblCode") as Label;
                if (lblCode != null)
                {
                    lblCode.Text = import.Code;
                }

                Label lblImportDate = e.Item.FindControl("lblImportDate") as Label;
                if (lblImportDate != null)
                {
                    lblImportDate.Text = import.ImportDate.ToString("dd/MM/yyyy");
                }

                Label lblImportedBy = e.Item.FindControl("lblImportedBy") as Label;
                if (lblImportedBy != null)
                {
                    lblImportedBy.Text = import.ImportedBy;
                }

                Label lblTotal = e.Item.FindControl("lblTotal") as Label;
                if (lblTotal != null)
                {
                    lblTotal.Text = import.Total.ToString("#,0.#");
                }

                Label lblSupp = e.Item.FindControl("lblSupp") as Label;
                if (lblSupp != null)
                {
                    if (import.Agency != null) lblSupp.Text = import.Agency.Name;
                }

                HyperLink hplEdit = e.Item.FindControl("hplEdit") as HyperLink;
                if (hplEdit != null)
                {
                    hplEdit.NavigateUrl = string.Format("IvImportAdd.aspx?NodeId={0}&SectionId={1}&ImportId={2}", Node.Id,
                        Section.Id, import.Id);
                }
                var btnDelete = e.Item.FindControl("btnDelete") as ImageButton;
                if (btnDelete != null)
                {
                    if (!UserIdentity.HasPermission(AccessLevel.Administrator))
                    {
                        btnDelete.Visible = false;
                    }
                }
            }

            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lblTotalMonth = e.Item.FindControl("lblTotalMonth") as Label;
                if (lblTotalMonth != null)
                {
                    lblTotalMonth.Text = sumTotal.ToString("#,0.#");
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string path = string.Format("IvProductImportList.aspx?NodeId={0}&SectionId={1}", Node.Id, Section.Id);

            string query = string.Empty;

            if (!string.IsNullOrEmpty(txtNameSearch.Text))
            {
                query += "&name=" + txtNameSearch.Text;
            }

            if (!string.IsNullOrEmpty(txtCodeSearch.Text))
            {
                query += "&code=" + txtCodeSearch.Text;
            }

            if (!string.IsNullOrEmpty(txtFromDate.Text))
            {
                DateTime firstDay = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                Double timeConvert = firstDay.ToOADate();
                query += "&fromDate=" + timeConvert;
            }
            if (!string.IsNullOrEmpty(txtToDate.Text))
            {
                DateTime firstDay = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                Double timeConvert = firstDay.ToOADate();
                query += "&toDate=" + timeConvert;
            }
            if (!string.IsNullOrEmpty(ddlCruise.SelectedValue))
            {
                query += "&cruiseId=" + ddlCruise.SelectedValue;
            }
            if (!string.IsNullOrEmpty(ddlStorage.SelectedValue))
            {
                query += "&storageId=" + ddlStorage.SelectedValue;
            }
            path.ToString();
            PageRedirect(path + query);
        }

        #endregion

        #region --- PRIVATE METHOD ---

        private void BindrptImportList()
        {
            int count;
            var query = new NameValueCollection(Request.QueryString);
            if (string.IsNullOrWhiteSpace(query["storageId"]))
                query.Add("storageId", ddlStorage.SelectedValue);
            rptImportList.DataSource = Module.IvImportGetByQueryString(query, UserIdentity, pagerProduct.PageSize, pagerProduct.CurrentPageIndex, out count, out sumTotal);
            pagerProduct.VirtualItemCount = count;
            rptImportList.DataBind();
        }

        private void LoadInfoSearch()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["fromDate"]))
            {
                double date = Convert.ToDouble(Request.QueryString["fromDate"]);
                DateTime timeConvert = DateTime.FromOADate(date);
                txtFromDate.Text = timeConvert.ToString("dd/MM/yyyy");
            }
            //else
            //{
            //    txtFromDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //}
            if (!string.IsNullOrEmpty(Request.QueryString["fromDate"]))
            {
                double date = Convert.ToDouble(Request.QueryString["toDate"]);
                DateTime timeConvert = DateTime.FromOADate(date);
                txtToDate.Text = timeConvert.ToString("dd/MM/yyyy");
            }
            //else
            //{
            //    txtToDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            //}
            if (!string.IsNullOrEmpty(Request.QueryString["name"]))
            {
                txtNameSearch.Text = Request.QueryString["name"];
            }
            else
            {
                txtNameSearch.Text = "";
            }

            if (!string.IsNullOrEmpty(Request.QueryString["code"]))
            {
                txtCodeSearch.Text = Request.QueryString["code"];
            }
            else
            {
                txtCodeSearch.Text = "";
            }
            if (!string.IsNullOrEmpty(Request["cruiseId"]))
            {
                ddlCruise.SelectedValue = Request["cruiseId"];
            }
            if (!string.IsNullOrEmpty(Request.QueryString["storageId"]))
            {
                ddlStorage.SelectedValue = Request.QueryString["storageId"];
            }

        }
        protected void ddlCRuise_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var list = (IList<IvStorage>)Module.IvStorageGetByUser(UserIdentity);
            ddlStorage.DataSource = list.Where(s => s.Cruise.Id == Convert.ToInt32(ddlCruise.SelectedValue));
            ddlStorage.DataTextField = "Name";
            ddlStorage.DataValueField = "Id";
            ddlStorage.DataBind();
        }
        #endregion
    }
}
