using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CMS.ServerControls;
using CMS.Web.Util;
using GemBox.Spreadsheet;
using log4net;
using NHibernate.Criterion;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;
using Portal.Modules.OrientalSails.Web.Util;
using CMS.Core.Domain;
using Portal.Modules.OrientalSails.BusinessLogic;
using Portal.Modules.OrientalSails.Utils;
using Portal.Modules.OrientalSails.Enums;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Portal.Modules.OrientalSails.BusinessLogic.Share;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class ReportDebtReceivables : SailsAdminBase
    {

        private double _totalReceivableUSD = 0.0;
        private double _totalReceivableVND = 0.0;
        private PermissionBLL permissionBLL;
        public PermissionBLL PermissionBLL
        {
            get
            {
                if (permissionBLL == null)
                {
                    permissionBLL = new PermissionBLL();
                }
                return permissionBLL;
            }
        }
        private UserBLL userBLL;
        public UserBLL UserBLL
        {
            get
            {
                if (userBLL == null)
                    userBLL = new UserBLL();
                return userBLL;
            }
        }

        public User CurrentUser
        {
            get
            {
                return UserBLL.UserGetCurrent();
            }
        }
        public bool AllowExportDebtReceivables
        {
            get
            {
                //return PermissionBLL.UserCheckPermission(CurrentUser.Id, (int)PermissionEnum.AllowExportDebtReceivables);
                return true;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.Title = "Báo cáo nợ phải thu";
            //var allowAccess = PermissionBLL.UserCheckPermission(CurrentUser.Id, (int)PermissionEnum.AllowAccessReportDebtReceivablePage);
            var allowAccess = true;
            if (!allowAccess)
            {
                ShowErrors("Bạn không có quyền truy cập vào trang này");
                plhAdminContent.Visible = false;
                return;
            }
            if (!IsPostBack)
            {
                GetReportData();
            }
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (userBLL != null)
            {
                userBLL.Dispose();
                userBLL = null;
            }
            if (permissionBLL != null)
            {
                permissionBLL.Dispose();
                permissionBLL = null;
            }
        }
        private void GetReportData()
        {
            //DateTime from = DateTimeUtil.DateGetDefaultFromDate();

            //if (!string.IsNullOrWhiteSpace(Request.QueryString["f"]))
            //    from = DateTime.ParseExact(Request.QueryString["f"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //txtFrom.Text = from.ToString("dd/MM/yyyy");

            var to = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            if (!string.IsNullOrEmpty(Request.QueryString["t"]))
            {
                to = DateTime.ParseExact(Request.QueryString["t"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            txtTo.Text = to.ToString("dd/MM/yyyy");
            var agencyId = -1;
            if (!string.IsNullOrEmpty(Request.QueryString["ai"]))
            {
                agencyId = Int32.Parse(Request.QueryString["ai"]);
                var agency = Module.AgencyGetById(Convert.ToInt32(agencyId));
                agencySelector.Value = agency.Id.ToString();
                agencySelectornameid.Text = agency.Name;
            }
            var list = Module.GetBookingDebtReceivables(to, agencyId);
            list = list.Where(b => b.Value != 0.0).ToList();
            Dictionary<Agency, DebtReceivable> dictionary = new Dictionary<Agency, DebtReceivable>();
            foreach (Booking booking in list)
            {
                if (dictionary.ContainsKey(booking.Agency))
                {
                    if (booking.IsTotalUsd)
                        dictionary[booking.Agency].TotalReceivableUSD += booking.Value;
                    else dictionary[booking.Agency].TotalReceivableVND += booking.Value;

                }
                else
                {
                    var debt = new DebtReceivable();
                    debt.Agency = booking.Agency;
                    if (booking.IsTotalUsd)
                        debt.TotalReceivableUSD += booking.Value;
                    else debt.TotalReceivableVND += booking.Value;
                    dictionary.Add(booking.Agency, debt);
                }
            }

            List<DebtReceivable> debtReceivables = new List<DebtReceivable>();

            foreach (KeyValuePair<Agency, DebtReceivable> valuePair in dictionary)
            {
                debtReceivables.Add(valuePair.Value);
            }
            debtReceivables = debtReceivables.OrderByDescending(x => x.TotalReceivableUSD).ToList();

            rptReport.DataSource = debtReceivables;
            rptReport.DataBind();
        }
        protected void btnDisplay_Click(object sender, EventArgs e)
        {
            Response.Redirect("ReportDebtReceivables.aspx" + QueryStringBuildByCriterion());
        }

        public string QueryStringBuildByCriterion()
        {
            NameValueCollection nvcQueryString = new NameValueCollection();
            nvcQueryString.Add("NodeId", "1");
            nvcQueryString.Add("SectionId", "15");

            //if (!string.IsNullOrEmpty(txtFrom.Text))
            //{
            //    nvcQueryString.Add("f", txtFrom.Text);
            //}
            if (!string.IsNullOrEmpty(txtTo.Text))
            {
                nvcQueryString.Add("t", txtTo.Text);
            }

            if (!string.IsNullOrEmpty(agencySelector.Value))
            {
                nvcQueryString.Add("ai", agencySelector.Value);
            }
            var criterions = (from key in nvcQueryString.AllKeys
                              from value in nvcQueryString.GetValues(key)
                              select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value))).ToArray();

            return "?" + string.Join("&", criterions);
        }

        protected void rptReport_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var debtReceivable = e.Item.DataItem as DebtReceivable;
            if (debtReceivable != null)
            {
                var hplAgency = (HyperLink)e.Item.FindControl("hplAgency");
                if (hplAgency != null)
                {
                    hplAgency.Text = debtReceivable.Agency != null ? debtReceivable.Agency.Name : "";
                    hplAgency.NavigateUrl = "javascript:;";
                    if (debtReceivable.Agency != null)
                        hplAgency.Attributes.Add("onclick",
                            string.Format("GotoReceivable('{0}','{1}','{2}')", Request["NodeId"], Request["SectionId"],
                                debtReceivable.Agency.Id));
                    //var url = "Receivables.aspx" + GetUrlAgency(debtReceivable.Agency);
                    //hplAgency.NavigateUrl = url;
                }
                Literal litTotalReceivableUSD = (Literal)e.Item.FindControl("litTotalReceivableUSD");
                if (litTotalReceivableUSD != null)
                {
                    _totalReceivableUSD += debtReceivable.TotalReceivableUSD;
                    litTotalReceivableUSD.Text = debtReceivable.TotalReceivableUSD.ToString("#,##0.##");
                }
                Literal litTotalReceivableVND = (Literal)e.Item.FindControl("litTotalReceivableVND");
                if (litTotalReceivableVND != null)
                {
                    _totalReceivableVND += debtReceivable.TotalReceivableVND;
                    litTotalReceivableVND.Text = debtReceivable.TotalReceivableVND.ToString("#,##0.##");
                }
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Literal litTotalUSD = (Literal)e.Item.FindControl("litTotalUSD");
                if (litTotalUSD != null)
                {
                    litTotalUSD.Text = _totalReceivableUSD.ToString("#,##0.##");
                }
                Literal litTotalVND = (Literal)e.Item.FindControl("litTotalVND");
                if (litTotalVND != null)
                {
                    litTotalVND.Text = _totalReceivableVND.ToString("#,##0.##");
                }
            }
        }

        private object GetUrlAgency(Agency agency)
        {
            NameValueCollection nvcQueryString = new NameValueCollection();
            nvcQueryString.Add("NodeId", "1");
            nvcQueryString.Add("SectionId", "15");

            if (!string.IsNullOrEmpty(txtTo.Text))
            {
                nvcQueryString.Add("t", txtTo.Text);
            }
            if (agency != null)
            {
                nvcQueryString.Add("ai", agency.Id.ToString());
            }
            nvcQueryString.Add("spay", "1");

            var criterions = (from key in nvcQueryString.AllKeys
                              from value in nvcQueryString.GetValues(key)
                              select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value))).ToArray();

            return "?" + string.Join("&", criterions);
        }

        protected void btnExport_OnClick(object sender, EventArgs e)
        {
            if (!AllowExportDebtReceivables)
            {
                ShowErrors("Bạn không có quyền xuất file báo cáo nợ phải thu");
                return;
            }
            //DateTime from = DateTimeUtil.DateGetDefaultFromDate();

            //if (!string.IsNullOrWhiteSpace(Request.QueryString["f"]))
            //    from = DateTime.ParseExact(Request.QueryString["f"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //txtFrom.Text = from.ToString("dd/MM/yyyy");
            var to = DateTimeUtil.DateGetDefaultToDate();
            if (!string.IsNullOrEmpty(Request.QueryString["t"]))
            {
                to = DateTime.ParseExact(Request.QueryString["t"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            txtTo.Text = to.ToString("dd/MM/yyyy");
            var agencyId = -1;
            if (!string.IsNullOrEmpty(Request.QueryString["ai"]))
            {
                agencyId = Int32.Parse(Request.QueryString["ai"]);
                var agency = Module.AgencyGetById(Convert.ToInt32(agencyId));
                agencySelector.Value = agency.Id.ToString();
                agencySelectornameid.Text = agency.Name;
            }
            var list = Module.GetBookingDebtReceivables(to, agencyId);
            list = list.Where(b => b.Value != 0.0).ToList();
            Dictionary<Agency, DebtReceivable> dictionary = new Dictionary<Agency, DebtReceivable>();
            foreach (Booking booking in list)
            {
                if (dictionary.ContainsKey(booking.Agency))
                {
                    if (booking.IsTotalUsd)
                        dictionary[booking.Agency].TotalReceivableUSD += booking.Value;
                    else dictionary[booking.Agency].TotalReceivableVND += booking.Value;

                }
                else
                {
                    var debt = new DebtReceivable();
                    debt.Agency = booking.Agency;
                    if (booking.IsTotalUsd)
                        debt.TotalReceivableUSD += booking.Value;
                    else debt.TotalReceivableVND += booking.Value;
                    dictionary.Add(booking.Agency, debt);
                }
            }

            List<DebtReceivable> debtReceivables = new List<DebtReceivable>();

            foreach (KeyValuePair<Agency, DebtReceivable> valuePair in dictionary)
            {
                debtReceivables.Add(valuePair.Value);
            }
            debtReceivables = debtReceivables.OrderByDescending(x => x.TotalReceivableUSD).ToList();
            ExcelFile excelFile = ExcelFile.Load(Server.MapPath("/Modules/Sails/Admin/ExportTemplates/bao_cao_no_phai_thu.xls"));
            ExcelWorksheet sheet = excelFile.Worksheets[0];

            const int firstrow = 6;
            int crow = firstrow;

            sheet.Rows.InsertCopy(crow, list.Count, sheet.Rows[firstrow]);
            var index = 1;
            //sheet.Cells["B3"].Value = from.ToString("dd/MM/yyyy");
            sheet.Cells["B4"].Value = to.ToString("dd/MM/yyyy");
            foreach (DebtReceivable debtReceivable in debtReceivables)
            {
                sheet.Cells[crow, 0].Value = index;
                sheet.Cells[crow, 1].Value = debtReceivable.Agency != null ? debtReceivable.Agency.Name : "";
                _totalReceivableUSD += debtReceivable.TotalReceivableUSD;
                sheet.Cells[crow, 2].Value = debtReceivable.TotalReceivableUSD.ToString("#,##0.##");
                _totalReceivableVND += debtReceivable.TotalReceivableVND;
                sheet.Cells[crow, 3].Value = debtReceivable.TotalReceivableVND.ToString("#,##0.##");
                crow++;
                index++;
            }

            sheet.Cells[crow, 1].Value = "Tổng";
            sheet.Cells[crow, 2].Value = _totalReceivableUSD.ToString("#,##0.##");
            sheet.Cells[crow, 3].Value = _totalReceivableVND.ToString("#,##0.##");
            excelFile.Save(Response, string.Format("bao_cao_no_phai_thu_{0:dd_MM_yyyy}.xlsx", to));

        }

        //protected void btnSaveStatusExport_OnClick(object sender, EventArgs e)
        //{
        //    foreach (RepeaterItem item in rptReport.Items)
        //    {
        //        if (item.ItemType == ListItemType.Item)
        //        {
        //            CheckBox chkIsExportVat = (CheckBox)item.FindControl("chkIsExportVat");
        //            if (chkIsExportVat != null)
        //            {
        //                if (chkIsExportVat.Checked && chkIsExportVat.Visible)
        //                {
        //                    HiddenField hidId = (HiddenField)item.FindControl("hidId");
        //                    if (hidId != null)
        //                    {
        //                        var booking = Module.BookingGetById(Convert.ToInt32(hidId.Value));
        //                        if (booking != null)
        //                        {
        //                            booking.IsExportVat = true;
        //                            Module.SaveOrUpdate(booking);
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    GetReportData();
        //}
    }
}