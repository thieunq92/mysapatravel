using System;
using System.Collections.Generic;
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
    /// danh sách sản phẩm xuất
    /// </summary>
    public partial class IvProductExportList : SailsAdminBase
    {
        private double sumTotal;

        #region --- PAGE EVENT ---

        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Title = "Danh sách phiếu xuất";
            pagerProduct.AllowCustomPaging = true;
            pagerProduct.PageSize = 20;
            if (!IsPostBack)
            {
                LoadStorage();
                LoadInfoSearch();
                BindrptExportList();
            }
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

        protected void rptExportList_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            switch (e.CommandName.ToLower())
            {
                case "delete":
                    IvExport export = Module.GetById<IvExport>(Convert.ToInt32(e.CommandArgument));
                    Module.Delete(export);
                    BindrptExportList();
                    break;
                default:
                    break;
            }

        }

        protected void rptExportList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //if (e.Item.ItemType == ListItemType.Header)
            //{
            //    RepeaterOrder.FILE_NAME = "IvProductExportList.aspx";
            //    RepeaterOrder.SetOrderLink(e, "Name", Request.QueryString);
            //    RepeaterOrder.SetOrderLink(e, "Code", Request.QueryString);
            //    RepeaterOrder.SetOrderLink(e, "ExportDate", Request.QueryString);
            //    RepeaterOrder.SetOrderLink(e, "ExportBy", Request.QueryString);
            //    RepeaterOrder.SetOrderLink(e, "Total", Request.QueryString);
            //    RepeaterOrder.SetOrderLink(e, "ExportTo", Request.QueryString);


            //    return;
            //}

            if (e.Item.DataItem is IvExport)
            {
                IvExport export = (IvExport)e.Item.DataItem;

                HyperLink hplName = e.Item.FindControl("hplName") as HyperLink;
                if (hplName != null)
                {
                    hplName.Text = export.Name;
                    hplName.NavigateUrl = string.Format("IvExportAdd.aspx?NodeId={0}&SectionID={1}&ExportId={2}", Node.Id, Section.Id, export.Id);
                }

                Label lblCode = e.Item.FindControl("lblCode") as Label;
                if (lblCode != null)
                {
                    lblCode.Text = export.Code;
                }
                Label lblRoom = e.Item.FindControl("lblRoom") as Label;
                if (lblRoom != null)
                {
                    if (export.Room != null) lblRoom.Text = export.Room.Name;
                }
                Label lblCustomer = e.Item.FindControl("lblCustomer") as Label;
                if (lblCustomer != null)
                {
                    lblCustomer.Text = export.CustomerName;
                }

                Label lblExportDate = e.Item.FindControl("lblExportDate") as Label;
                if (lblExportDate != null)
                {
                    lblExportDate.Text = export.ExportDate.ToString("dd/MM/yyyy");
                }

                Label lblExportBy = e.Item.FindControl("lblExportBy") as Label;
                if (lblExportBy != null)
                {
                    lblExportBy.Text = export.ExportedBy;
                }
                Label lblExportTo = e.Item.FindControl("lblExportTo") as Label;
                if (lblExportTo != null)
                {
                    lblExportTo.Text = export.CustomerName;
                }

                Label lblTotal = e.Item.FindControl("lblTotal") as Label;
                if (lblTotal != null)
                {
                    lblTotal.Text = export.Total.ToString("#,0.#");
                }
                Label lblPay = e.Item.FindControl("lblPay") as Label;
                if (lblPay != null)
                {
                    lblPay.Text = export.Pay.ToString("#,0.#");
                }
                Label lblStatus = e.Item.FindControl("lblStatus") as Label;
                if (lblStatus != null)
                {
                    if (export.Pay >= export.Total)
                        lblStatus.Text = "Đã thanh toán";
                    if (0 < export.Pay && export.Pay < export.Total)
                        lblStatus.Text = "Còn tồn";
                    if (export.Pay <= 0)
                        lblStatus.Text = "Chưa thanh toán";
                }

                HyperLink hplEdit = e.Item.FindControl("hplEdit") as HyperLink;
                if (hplEdit != null)
                {
                    hplEdit.NavigateUrl = string.Format("IvExportAdd.aspx?NodeId={0}&SectionId={1}&ExportId={2}", Node.Id,
                                                            Section.Id, export.Id);
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
                Label lblsumTotal = e.Item.FindControl("lblsumTotal") as Label;
                if (lblsumTotal != null)
                {
                    lblsumTotal.Text = sumTotal.ToString("#,0.#");
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string path = string.Format("IvProductExportList.aspx?NodeId={0}&SectionId={1}", Node.Id, Section.Id);

            string query = string.Empty;

            if (!string.IsNullOrEmpty(txtNameSearch.Text))
            {
                query += "&name=" + txtNameSearch.Text;
            }

            if (!string.IsNullOrEmpty(txtCodeSearch.Text))
            {
                query += "&code=" + txtCodeSearch.Text;
            }
            if (!string.IsNullOrEmpty(ddlStorage.SelectedValue))
            {
                query += "&StorageId=" + ddlStorage.SelectedValue;
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
            if (!string.IsNullOrWhiteSpace(ddlDebt.SelectedValue))
            {
                query += "&debt=" + ddlDebt.SelectedValue;
            }

            PageRedirect(path + query);
        }

        #endregion

        #region --- PRIVATE METHOD ---

        private void BindrptExportList()
        {
            int count;
            rptExportList.DataSource = Module.IvExportGetByQueryString(Request.QueryString, UserIdentity, pagerProduct.PageSize, pagerProduct.CurrentPageIndex, out count, out sumTotal);
            pagerProduct.VirtualItemCount = count;
            rptExportList.DataBind();
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
            if (!string.IsNullOrEmpty(Request.QueryString["StorageId"]))
            {
                ddlStorage.SelectedValue = Request.QueryString["StorageId"];
            }
            if (!string.IsNullOrEmpty(Request.QueryString["debt"]))
            {
                ddlDebt.SelectedValue = Request.QueryString["debt"];
            }
        }

        #endregion

        protected void btnAddNewExport_OnClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("IvExportAdd.aspx?NodeId={0}&SectionID={1}", Node.Id, Section.Id));

        }
    }
}
