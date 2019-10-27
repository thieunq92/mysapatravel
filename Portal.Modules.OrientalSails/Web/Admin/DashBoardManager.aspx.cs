using CMS.Core.Domain;
using Portal.Modules.OrientalSails.BusinessLogic;
using Portal.Modules.OrientalSails.BusinessLogic.Share;
using Portal.Modules.OrientalSails.DataTransferObject;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.Admin.Utility;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class DashBoardManager : System.Web.UI.Page
    {
        private UserBLL userBLL;
        private User currentUser;
        private DashBoardManagerBLL dashBoardManagerBLL;
        private PermissionBLL permissionBLL;
        public UserBLL UserBLL
        {
            get
            {
                if (userBLL == null)
                {
                    userBLL = new UserBLL();
                }
                return userBLL;
            }
        }
        public User CurrentUser
        {
            get
            {
                if (currentUser == null)
                {
                    currentUser = UserBLL.UserGetCurrent();
                }
                return currentUser;
            }
        }
        public DashBoardManagerBLL DashBoardManagerBLL
        {
            get
            {
                if (dashBoardManagerBLL == null)
                {
                    dashBoardManagerBLL = new DashBoardManagerBLL();
                }
                return dashBoardManagerBLL;
            }
        }
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
        public IEnumerable<RoomsAvaiableDTO> RoomsAvaiableDTO { get; set; }
        public bool CanAccess
        {
            get
            {
                return PermissionBLL.UserCheckPermission(CurrentUser, Enums.PermissionEnum.DASHBOARDMANAGER_ACCESS);
            }
        }
        public bool IsVisitedDashBoard
        {
            get
            {
                var isVisitedDashBoard = false;
                try
                {
                    isVisitedDashBoard = bool.Parse(Request.QueryString["ivdb"]);
                }
                catch { }
                return isVisitedDashBoard;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAccess();
            if (!IsPostBack)
            {
                var from = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                var to = from.AddMonths(1).AddDays(-1);
                LoadSalesMonthSummary(from, to);
                ddlMonthSearching.Items.AddRange(Enumerable.Range(1, 12).Select(x => new ListItem() { Text = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(x), Value = x.ToString() }).ToArray());
                ddlYearSearching.Items.AddRange(Enumerable.Range(2008, 50).Select(x => new ListItem() { Text = x.ToString(), Value = x.ToString() }).ToArray());
                ddlMonthSearching.SelectedValue = DateTime.Today.Month.ToString();
                ddlYearSearching.SelectedValue = DateTime.Today.Year.ToString();
                LoadCruiseAvaialability();
                LoadBookingReport();
                var sales = DashBoardManagerBLL.SalesGetAll();
                ddlSales.DataTextField = "UserName";
                ddlSales.DataValueField = "Id";
                ddlSales.DataSource = sales.Where(s => s.IsUsedInDashBoardManager == true);
                ddlSales.DataBind();
                ddlRecentMeetingSearchSales.DataTextField = "UserName";
                ddlRecentMeetingSearchSales.DataValueField = "Id";
                ddlRecentMeetingSearchSales.DataSource = sales.Where(s => s.IsUsedInDashBoardManager == true);
                ddlRecentMeetingSearchSales.DataBind();
                ddlAgenciesNotVisitedSearchSales.DataTextField = "UserName";
                ddlAgenciesNotVisitedSearchSales.DataValueField = "Id";
                ddlAgenciesNotVisitedSearchSales.DataSource = sales.Where(s => s.IsUsedInDashBoardManager == true);
                ddlAgenciesNotVisitedSearchSales.DataBind();
                ddlMonthTopPartner.Items.AddRange(Enumerable.Range(1, 12).Select(x => new ListItem() { Text = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(x), Value = x.ToString() }).ToArray());
                ddlMonthTopPartner.SelectedValue = DateTime.Today.Month.ToString();
                LoadAgenciesSendNoBookingLast3Month();
                LoadRecentMeetings();
                LoadAgenciesNotVisitedOrUpdatedLast2Month();
                LoadTopAgencies();
            }
        }

        public void LoadBookingReport()
        {
            var date = DateTime.Today;
            try
            {
                date = DateTime.ParseExact(txtBookingReportDateSearching.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }
            var newBookings = GetNewBookings(date);
            var cancelledBookings = GetCancelledBookings(date);
            var bookingsReported = new List<Booking>();
            if (newBookings != null) bookingsReported.AddRange(newBookings);
            if (cancelledBookings != null) bookingsReported.AddRange(cancelledBookings);
            rptNewBookings.DataSource = bookingsReported;
            rptNewBookings.DataBind();
        }

        public IEnumerable<Booking> GetCancelledBookings(DateTime date)
        {
            return DashBoardManagerBLL.BookingGetAllCancelledBookingOnDate(date);
        }

        public IEnumerable<Booking> GetNewBookings(DateTime date)
        {
            return DashBoardManagerBLL.BookingGetAllNewBookings(date);
        }

        private void LoadTopAgencies()
        {
            var month = 1;
            try
            {
                month = Int32.Parse(ddlMonthTopPartner.SelectedValue);
            }
            catch { }
            var from = new DateTime(DateTime.Today.Year, month, 1);
            var to = from.AddMonths(1).AddDays(-1);
            var top10Agencies = DashBoardManagerBLL.AgencyGetTop10(from, to);
            rptTop10Partner.DataSource = top10Agencies;
            rptTop10Partner.DataBind();
            ddlMonthTopPartner.SelectedValue = month.ToString();
        }

        public void LoadAgenciesNotVisitedOrUpdatedLast2Month()
        {
            var salesId = 0;
            try
            {
                salesId = Int32.Parse(ddlAgenciesNotVisitedSearchSales.SelectedValue);
            }
            catch { }
            var agenciesNotVisitedUpdated = DashBoardManagerBLL.GetAgenciesNotVistedUpdatedLast2Month(salesId).OrderBy(x => x.LastMeetingDate).ToList();
            rptAgenciesNotVisitedUpdated.DataSource = agenciesNotVisitedUpdated;
            rptAgenciesNotVisitedUpdated.DataBind();
        }

        /// <summary>
        ///     Load meetings theo sales được create trong 7 ngày gần nhất
        /// </summary>
        public void LoadRecentMeetings()
        {
            var salesId = 0;
            try
            {
                salesId = Int32.Parse(ddlRecentMeetingSearchSales.SelectedValue);
            }
            catch { }
            var from = DateTime.Today.AddDays(-7);
            var to = DateTime.Today;
            var recentMeetings = DashBoardManagerBLL.ActivityGetAllRecentMeetingsInDateRange(salesId, from, to).OrderByDescending(a => a.DateMeeting).ToList();
            rptRecentMeetings.DataSource = recentMeetings;
            rptRecentMeetings.DataBind();
        }

        public void LoadAgenciesSendNoBookingLast3Month()
        {
            var salesId = 0;
            try
            {
                salesId = Int32.Parse(ddlSales.SelectedValue);
            }
            catch { }
            var agenciesSendNoBookingLast3Month = DashBoardManagerBLL.GetAgenciesSendNoBookingLast3Month(salesId).OrderByDescending(x => x.LastBookingDate).ToList();
            rptAgenciesSendNoBookingLast3Months.DataSource = agenciesSendNoBookingLast3Month;
            rptAgenciesSendNoBookingLast3Months.DataBind();
        }
        public void CheckAccess()
        {
            if (!CanAccess && !IsVisitedDashBoard)
            {
                Response.Redirect("DashBoard.aspx?NodeId=1&SectionId=15&ivdbm=true");
            }
            else if (!CanAccess && IsVisitedDashBoard)
            {
                Server.Transfer("AccessDenied.aspx?NodeId=1&SectionId=15");
            }
        }

        public void LoadSalesMonthSummary(DateTime from, DateTime to)
        {
            var salesMonthSummary = DashBoardManagerBLL.GetSalesMonthSummary(from, to);
            rptSales.DataSource = salesMonthSummary;
            rptSales.DataBind();
            rptNoOfPax2Days.DataSource = salesMonthSummary;
            rptNoOfPax2Days.DataBind();
            rptNoOfPax3Days.DataSource = salesMonthSummary;
            rptNoOfPax3Days.DataBind();
            rptNoOfBookings.DataSource = salesMonthSummary;
            rptNoOfBookings.DataBind();
            rptRevenueInUSD.DataSource = salesMonthSummary;
            rptRevenueInUSD.DataBind();
            rptMeetingReports.DataSource = salesMonthSummary;
            rptMeetingReports.DataBind();
            rptTotalPax.DataSource = salesMonthSummary;
            rptTotalPax.DataBind();
        }
        public void LoadSalesMonthSummaryWhenMonthYearChanged()
        {
            var year = Int32.Parse(ddlYearSearching.SelectedValue);
            var month = Int32.Parse(ddlMonthSearching.SelectedValue);
            var from = new DateTime(year, month, 1);
            var to = from.AddMonths(1).AddDays(-1);
            LoadSalesMonthSummary(from, to);
        }
        public void LoadCruiseAvaialability()
        {
            var dateSearching = txtDateSearching.Text;
            var from = DateTime.Today;
            if (!String.IsNullOrEmpty(dateSearching))
            {
                try
                {
                    from = DateTime.ParseExact(dateSearching, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                catch
                {
                    txtDateSearching.Text = "";
                }
            }
            //Tạo danh sách 15 ngày tới
            var to = from.AddDays(15);
            var dateRange = new List<DateTime>();
            var current = from;
            while (current < to)
            {
                dateRange.Add(current);
                current = current.AddDays(1);
            }
            //--
            RoomsAvaiableDTO = DashBoardManagerBLL.CruiseGetRoomsAvaiableInDateRange(from, to);
            rptCruiseAvaibility.DataSource = dateRange;
            rptCruiseAvaibility.DataBind();
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (userBLL != null)
            {
                userBLL.Dispose();
                userBLL = null;
            }
            if (dashBoardManagerBLL != null)
            {
                dashBoardManagerBLL.Dispose();
                dashBoardManagerBLL = null;
            }
            if (permissionBLL != null)
            {
                permissionBLL.Dispose();
                permissionBLL = null;
            }
        }

        protected void ddlMonthSearching_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSalesMonthSummaryWhenMonthYearChanged();
        }

        protected void ddlYearSearching_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSalesMonthSummaryWhenMonthYearChanged();
        }

        protected void txtDateSearching_TextChanged(object sender, EventArgs e)
        {
            LoadCruiseAvaialability();
        }

        protected void rptCruiseAvaibility_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var cruises = DashBoardManagerBLL.CruiseGetAll();
            var dateSearching = new DateTime();
            try
            {
                dateSearching = DateTime.ParseExact(txtDateSearching.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }
            if (e.Item.ItemType == ListItemType.Header)
            {
                var ltrHeader = (Literal)e.Item.FindControl("ltrHeader");
                var header = "";
                foreach (var cruise in cruises)
                {
                    header += "<th style='width:10%'>" + (cruise.Code != null ? cruise.Code.ToUpper() : "") + "</th>";
                }
                ltrHeader.Text = header;
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var date = (DateTime)e.Item.DataItem;
                var className = date.Date == dateSearching.Date ? "--high-light" : "";
                var ltrRow = (Literal)e.Item.FindControl("ltrRow");
                var rowItems = "<td class=" + className + ">" +
                                    "<a href=BookingReport.aspx?NodeId=1&SectionId=15&date=" + date.ToString("dd/MM/yyyy") + ">" + date.ToString("dd/MM/yyyy") +
                                    "</a>" +
                               "</td>";
                var defaultClassName = className;
                foreach (var cruise in cruises)
                {
                    var numberOfRoom = RoomsAvaiableDTO.Where(x => DateTimeUtil.EqualsUpToSeconds(x.Date, date) && x.CruiseId == cruise.Id).First().TotalRoom;
                    var numberOfRoomAvaiable = RoomsAvaiableDTO.Where(x => DateTimeUtil.EqualsUpToSeconds(x.Date, date) && x.CruiseId == cruise.Id).First().NoRAvaiable;
                    double percentOfRoomAvailable = (numberOfRoomAvaiable / numberOfRoom);
                    if (percentOfRoomAvailable == 1)
                    {
                        className = "td__no-room-using";
                    }
                    if (numberOfRoomAvaiable > 0)
                    {
                        rowItems += "<td class='" + className + "'>" + numberOfRoomAvaiable + "</td>";
                    }
                    else
                    {
                        rowItems += "<td class='td__not-avaiable'>" + numberOfRoomAvaiable + "</td>";
                    }
                    className = defaultClassName;
                };
                var row = string.Format("<tr>{0}</tr>", rowItems);
                ltrRow.Text = row;
            }
        }
        public double GetTotal(Booking booking)
        {
            var total = 0.0;
            if (booking.IsTotalUsd)
                total = booking.Total;
            else
                total = booking.Total / 23000;
            return total;
        }
        public string GetTotalAsString(Booking booking)
        {
            return NumberUtil.FormatMoney(GetTotal(booking));
        }
        public Agency AgencyGetById(string agencyId)
        {
            var agencyIdAsInt = 0;
            try
            {
                agencyIdAsInt = Int32.Parse(agencyId);
            }
            catch { }
            return DashBoardManagerBLL.AgencyGetById(agencyIdAsInt);
        }
        public AgencyContact AgencyContactGetById(int agencyContactId)
        {
            return DashBoardManagerBLL.AgencyContactGetById(agencyContactId);
        }

        protected void ddlSales_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAgenciesSendNoBookingLast3Month();
        }

        protected void ddlRecentMeetingSearchSales_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRecentMeetings();
        }

        protected void ddlAgenciesNotVisitedSearchSales_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadAgenciesNotVisitedOrUpdatedLast2Month();
        }

        protected void ddlMonthTopPartner_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTopAgencies();
        }

        protected void txtBookingReportDateSearching_TextChanged(object sender, EventArgs e)
        {
            LoadBookingReport();
        }

        protected void rptRecentMeetings_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {
                var arguments = e.CommandArgument.ToString().Split(new char[] { ',' });
                string filePath = "";
                string contentType = @"application/octet-stream";
                try
                {
                    filePath = arguments[0];
                }
                catch { }
                try
                {
                    contentType = arguments[1];
                }
                catch { }
                FileInfo file = new FileInfo(filePath);
                if (file.Exists)
                {
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = @contentType;
                    Response.Flush();
                    Response.WriteFile(filePath);
                    Response.End();
                }
                else
                {
                    if (Request.UrlReferrer != null)
                    {
                        Type csType = GetType();
                        string jsScript = "alert('File doesn't exist');";
                        ScriptManager.RegisterClientScriptBlock(Page, csType, "popup", jsScript, true);
                    }
                }
            }
        }

        protected void rptRecentMeetings_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(e.Item.FindControl("lbtDownload"));
        }
    }
}