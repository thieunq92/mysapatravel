using Aspose.Words;
using Aspose.Words.Tables;
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
    public partial class DashBoard : System.Web.UI.Page
    {
        private DashBoardBLL dashBoardBLL;
        private PermissionBLL permissionBLL;
        private UserBLL userBLL;
        private User currentUser;
        public DashBoardBLL DashBoardBLL
        {
            get
            {
                if (dashBoardBLL == null)
                {
                    dashBoardBLL = new DashBoardBLL();
                }
                return dashBoardBLL;
            }
        }
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
                return PermissionBLL.UserCheckPermission(CurrentUser, Enums.PermissionEnum.DASHBOARD_ACESS);
            }
        }
        public bool IsVisitedDashBoardManager
        {
            get
            {
                var isVisitedDashBoardManager = false;
                try
                {
                    isVisitedDashBoardManager = bool.Parse(Request.QueryString["ivdbm"]);
                }
                catch { }
                return isVisitedDashBoardManager;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckAccess();
            if (!IsPostBack)
            {
                var todayBookings = DashBoardBLL.BookingGetAllTodayBookings(CurrentUser);
                rptTodayBookings.DataSource = todayBookings;
                rptTodayBookings.DataBind();
                LoadCruiseAvaialability();
                var newBookings = DashBoardBLL.BookingGetAllNewBookings(CurrentUser);
                rptNewBookings.DataSource = newBookings;
                rptNewBookings.DataBind();
                LoadRecentMeetings();
                var lastActivityOfAgenciesNotVisited = DashBoardBLL.AgencyGetAllAgenciesNotVisitedInLast2Month(CurrentUser);
                rptAgencyNotVisited.DataSource = lastActivityOfAgenciesNotVisited;
                rptAgencyNotVisited.DataBind();
                var top10Agencies = DashBoardBLL.AgencyGetTop10(CurrentUser);
                rptTop10Partner.DataSource = top10Agencies;
                rptTop10Partner.DataBind();
                var agenciesSendNoBookings = DashBoardBLL.AgencyGetAllAgenciesSendNoBookingsLast3Month(CurrentUser);
                rptAgenciesSendNoBookings.DataSource = agenciesSendNoBookings;
                rptAgenciesSendNoBookings.DataBind();
                ddlMonthSearching.Items.AddRange(Enumerable.Range(1, 12).Select(x => new ListItem() { Text = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(x), Value = x.ToString() }).ToArray());
                ddlYearSearching.Items.AddRange(Enumerable.Range(2008, 50).Select(x => new ListItem() { Text = x.ToString(), Value = x.ToString() }).ToArray());
                ddlMonthSearching.SelectedValue = DateTime.Today.Month.ToString();
                ddlYearSearching.SelectedValue = DateTime.Today.Year.ToString();
                LoadYourMonthArchivement();
                ddlCruise.DataSource = DashBoardBLL.CruiseGetAll();
                ddlCruise.DataTextField = "Name";
                ddlCruise.DataValueField = "Id";
                ddlCruise.DataBind();
                ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(btnExport);
            }
        }

        public void CheckAccess()
        {
            if (!CanAccess && !IsVisitedDashBoardManager)
            {
                Response.Redirect("DashBoardManager.aspx?NodeId=1&SectionId=15&ivdb=true");
            }
            else if (!CanAccess && IsVisitedDashBoardManager)
            {
                Server.Transfer("AccessDenied.aspx?NodeId=1&SectionId=15");
            }
        }
        public void LoadCruiseAvaialability()
        {
            var dateSearching = txtDateSearching.Text;
            var from = DateTime.Today.AddDays(1);
            if (!String.IsNullOrEmpty(dateSearching))
            {
                try
                {
                    from = DateTime.ParseExact(dateSearching, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddDays(-3);
                }
                catch
                {
                    txtDateSearching.Text = "";
                }
            }
            //Tạo danh sách 7 ngày tới
            var to = from.AddDays(7);
            var dateRange = new List<DateTime>();
            var current = from;
            while (current < to)
            {
                dateRange.Add(current);
                current = current.AddDays(1);
            }
            //--
            RoomsAvaiableDTO = DashBoardBLL.CruiseGetRoomsAvaiableInDateRange(from, to);
            rptCruiseAvaibility.DataSource = dateRange;
            rptCruiseAvaibility.DataBind();
        }

        public void LoadYourMonthArchivement()
        {
            var month = DateTime.Today.Month;
            var year = DateTime.Today.Year;
            try
            {
                month = Int32.Parse(ddlMonthSearching.SelectedValue);
            }
            catch { }
            try
            {
                year = Int32.Parse(ddlYearSearching.SelectedValue);
            }
            catch { }
            lblNumberOfPax.Text = GetNumberOfPaxInMonth(month, year).ToString();
            lblNumberOfBookings.Text = GetNumberOfBookingsInMonth(month, year).ToString();
            lblTotalRevenue.Text = GetTotalOfBookingsInMonth(month, year).ToString();
            lblAgenciesVisited.Text = GetNumberOfAgenciesVisited(month, year).ToString();
            lblMeetingReports.Text = GetNumberOfMeetingsInMonth(month, year).ToString();
        }
        public IEnumerable<Activity> GetRecentMeetings()
        {
            var from = DateTime.Today.AddDays(-10);
            try
            {
                from = DateTime.ParseExact(txtFromRecentMeetingSearch.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }
            var to = DateTime.Today;
            try
            {
                to = DateTime.ParseExact(txtToRecentMeetingSearch.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }
            return DashBoardBLL.ActivityGetAllRecentMeetingsInDateRange(CurrentUser, from, to);
        }
        public void LoadRecentMeetings()
        {
            var recentMeetings = GetRecentMeetings().OrderByDescending(a => a.DateMeeting).ToList();
            rptRecentMeetings.DataSource = recentMeetings;
            rptRecentMeetings.DataBind();
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (dashBoardBLL != null)
            {
                dashBoardBLL.Dispose();
                dashBoardBLL = null;
            }
            if (permissionBLL != null)
            {
                permissionBLL.Dispose();
                permissionBLL = null;
            }
            if (userBLL != null)
            {
                userBLL.Dispose();
                userBLL = null;
            }
        }

        protected void rptCruiseAvaibility_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var cruises = DashBoardBLL.CruiseGetAll();
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
                foreach (var cruise in cruises)
                {
                    var numberOfRoomAvaiable = RoomsAvaiableDTO.Where(x => DateTimeUtil.EqualsUpToSeconds(x.Date, date) && x.CruiseId == cruise.Id).First().NoRAvaiable;
                    if (numberOfRoomAvaiable > 0)
                    {
                        rowItems += "<td class='" + className + "'>" + numberOfRoomAvaiable + "</td>";
                    }
                    else
                    {
                        rowItems += "<td class='td__not-avaiable'>" + numberOfRoomAvaiable + "</td>";
                    }
                };
                var row = string.Format("<tr>{0}</tr>", rowItems);
                ltrRow.Text = row;
            }
        }

        public int GetNumberOfRoomAvaiable(Cruise cruise, DateTime date)
        {
            return DashBoardBLL.GetNumberOfRoomAvaiable(cruise, date);
        }

        public Agency AgencyGetById(string agencyId)
        {
            var agencyIdAsInt = 0;
            try
            {
                agencyIdAsInt = Int32.Parse(agencyId);
            }
            catch { }
            return DashBoardBLL.AgencyGetById(agencyIdAsInt);
        }

        public AgencyContact AgencyContactGetById(int agencyContactId)
        {
            return DashBoardBLL.AgencyContactGetById(agencyContactId);
        }
        public IEnumerable<Booking> BookingGetAllBookingsInMonth()
        {
            return DashBoardBLL.BookingGetAllBookingsInMonth(CurrentUser);
        }
        public int GetNumberOfPaxInMonth(int month, int year)
        {
            return DashBoardBLL.CustomerGetNumberOfCustomersInMonth(month, year, CurrentUser);
        }
        public int GetNumberOfBookingsInMonth(int month, int year)
        {
            return DashBoardBLL.BookingGetNumberOfBookingsInMonth(month, year, CurrentUser);
        }
        public string GetTotalOfBookingsInMonth(int month, int year)
        {
            var totalRevenue = DashBoardBLL.BookingGetTotalRevenueInMonth(month, year, CurrentUser);
            return NumberUtil.FormatMoney(totalRevenue);
        }
        public IEnumerable<Activity> ActivityGetAllActivityInMonth(int month, int year)
        {
            return DashBoardBLL.ActivityGetAllActivityInMonth(month, year, CurrentUser);
        }
        public int GetNumberOfMeetingsInMonth(int month, int year)
        {
            return ActivityGetAllActivityInMonth(month, year).Count();
        }
        public int GetNumberOfAgenciesVisited(int month, int year)
        {
            return ActivityGetAllActivityInMonth(month, year).Select(x => x.Params).Distinct().Count();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var uploadPath = "";
            var contentType = "";
            if (fuAttachment.HasFile)
            {
                var fileName = DateTime.Today.Year.ToString() + DateTime.Today.Month.ToString() + DateTime.Today.Day.ToString() + Guid.NewGuid().ToString();
                var fileExtension = new FileInfo(fuAttachment.FileName).Extension;
                var fullFileName = fileName + fileExtension;
                uploadPath = Path.Combine(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Upload/"), fullFileName);
                fuAttachment.SaveAs(uploadPath);
                contentType = fuAttachment.PostedFile.ContentType;
            }
            var agencyContactId = 0;
            try
            {
                agencyContactId = Int32.Parse(Request.Params["ddlContact"]);
            }
            catch { }
            var agencyContact = DashBoardBLL.AgencyContactGetById(agencyContactId);
            var dateMeeting = new DateTime();
            try
            {
                dateMeeting = DateTime.ParseExact(txtDateMeeting.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }
            var problems = "";
            Cruise cruise = null;
            if (ddlType.SelectedValue == "Problem Report")
            {
                if (chkBus.Checked) problems += "Bus,";
                if (chkCabin.Checked) problems += "Cabin,";
                if (chkGuide.Checked) problems += "Guide,";
                if (chkFood.Checked) problems += "Food,";
                if (chkOthers.Checked) problems += "Others,";
                problems = problems.TrimEnd(new char[] { ',' });
                var cruiseId = 0;
                try
                {
                    cruiseId = Int32.Parse(ddlCruise.SelectedValue);
                }
                catch { }
                cruise = DashBoardBLL.CruiseGetById(cruiseId);
                if (cruise.Id == 0) cruise = null;
            }

            var activityId = 0;
            try
            {
                activityId = Int32.Parse(hidActivityId.Value);
            }
            catch { }
            var activity = DashBoardBLL.ActivityGetById(activityId);
            if (activity == null || activity.Id == 0) activity = new Activity();
            activity.UpdateTime = DateTime.Now;
            activity.Time = DateTime.Now;
            activity.Params = agencyContact != null && agencyContact.Agency != null ? agencyContact.Agency.Id.ToString() : 0.ToString();
            activity.DateMeeting = dateMeeting;
            activity.Note = txtNote.Text;
            activity.ObjectType = "MEETING";
            activity.ObjectId = agencyContact != null ? agencyContact.Id : 0;
            activity.Url = "AgencyView.aspx?NodeId=1&SectionId=15&agencyid=" + agencyContact != null && agencyContact.Agency != null ? agencyContact.Agency.Id.ToString() : 0.ToString();
            activity.User = CurrentUser;
            activity.Level = ImportantLevel.Important;
            activity.Type = ddlType.SelectedValue;
            activity.NeedManagerAttention = chkNeedManagerAttention.Checked;
            activity.Attachment = uploadPath == "" ? activity.Attachment : uploadPath;
            activity.AttachmentContentType = contentType == "" ? activity.AttachmentContentType : contentType;
            activity.Problems = problems;
            activity.Cruise = cruise;
            DashBoardBLL.ActivitySaveOrUpdate(activity);
            Response.Redirect(Request.RawUrl);
        }

        public double GetTotal(Booking booking)
        {
            var total = 0.0;
            if (booking.IsTotalUsd)
                total = booking.Total * 23000;
            else
                total = booking.Total;
            return total;
        }
        public string GetTotalAsString(Booking booking)
        {
            return NumberUtil.FormatMoney(GetTotal(booking));
        }
        public string GetTotalOfBookings(IEnumerable<Booking> bookings)
        {
            var total = 0.0;
            foreach (var booking in bookings)
            {
                total += GetTotal(booking);
            }
            return NumberUtil.FormatMoney(total);
        }

        protected void txtDateSearching_TextChanged(object sender, EventArgs e)
        {
            LoadCruiseAvaialability();
        }

        protected void ddlMonthSearching_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadYourMonthArchivement();
        }

        protected void ddlYearSearching_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadYourMonthArchivement();
        }

        protected void txtPartnerSearch_TextChanged(object sender, EventArgs e)
        {
            Response.Redirect("AgencyList.aspx?NodeId=1&SectionId=15&Name=" + txtPartnerSearch.Text);
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadRecentMeetings();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            var document = new Document();
            var builder = new DocumentBuilder(document);
            var activities = GetRecentMeetings().OrderByDescending(a => a.DateMeeting).ToList();
            var sales = activities.Select(x => x.User).Distinct().ToList();
            for (int i = 0; i < sales.Count(); i++)
            {
                var needInsertSalesHeader = true;
                for (int j = 0; j < activities.Count(); j++)
                {
                    var activity = activities[j] as Activity;
                    var uniqueSales = sales[i];
                    var salesInActivity = activity.User;
                    if (uniqueSales.Id != salesInActivity.Id)
                        continue;
                    var contact = DashBoardBLL.AgencyContactGetById(activity.ObjectId);
                    var contactName = contact != null ? contact.Name : "";
                    var contactPosition = contact != null ? contact.Position : "";
                    var dateMeeting = activity.DateMeeting.ToString("dd/MM/yyyy");
                    var agencyId = 0;
                    try
                    {
                        agencyId = Int32.Parse(activity.Params);
                    }
                    catch { }
                    var agency = DashBoardBLL.AgencyGetById(agencyId);
                    var agencyName = agency != null ? agency.Name : "";
                    var note = activity.Note;
                    var salesName = uniqueSales.FullName;
                    InsertTableActivityToDocument(builder, needInsertSalesHeader, dateMeeting, salesName, contactName, contactPosition, agencyName, note);
                    needInsertSalesHeader = false;
                }
            }
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/ms-word";
            Response.AppendHeader("content-disposition", "attachment; filename=" + string.Format("Meetings.doc"));
            MemoryStream m = new MemoryStream();
            document.Save(m, SaveFormat.Doc);
            Response.OutputStream.Write(m.GetBuffer(), 0, m.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();
            m.Close();
            Response.End();
        }
        private void InsertTableActivityToDocument(DocumentBuilder builder, bool needInsertSalesHeader,
           string dateMeeting, string sales, string meetingWith, string position, string belongToAgency, string note)
        {
            InsertHeader(builder, needInsertSalesHeader, sales);
            builder.StartTable();
            InsertRow(builder, dateMeeting, meetingWith, position, belongToAgency, note);
            builder.EndTable();
        }
        public void InsertHeader(DocumentBuilder builder, bool needInsertSalesHeader, string sales)
        {
            var font = builder.Font;
            font.Bold = true;
            font.Size = 16;
            var paragraph = builder.ParagraphFormat;
            paragraph.Alignment = ParagraphAlignment.Center;
            var from = DateTime.Today.AddDays(-10);
            try
            {
                from = DateTime.ParseExact(txtFromRecentMeetingSearch.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }
            var to = DateTime.Today;
            try
            {
                to = DateTime.ParseExact(txtToRecentMeetingSearch.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }
            catch { }
            if (needInsertSalesHeader)
            {
                builder.Writeln(string.Format("Sales {0} meetings from {1} to {2}", sales, from.ToString("dd/MM/yyyy"), to.ToString("dd/MM/yyyy")));
                builder.Writeln("");
            }
        }

        public void InsertRow(DocumentBuilder builder, string dateMeeting, string meetingWith, string position,
            string belongToAgency, string note)
        {
            var font = builder.Font;
            var paragraph = builder.ParagraphFormat;
            font.Size = 12;
            paragraph = builder.ParagraphFormat;
            paragraph.Alignment = ParagraphAlignment.Left;
            builder.CellFormat.Width = 80;
            builder.InsertCell().CellFormat.HorizontalMerge = CellMerge.None;
            if (dateMeeting != null)
            {
                builder.Writeln(dateMeeting);
            }

            builder.InsertCell();
            if (meetingWith != null)
            {
                builder.Writeln(meetingWith);
            }

            builder.InsertCell();
            if (position != null)
            {
                builder.Writeln(position);
            }

            font.Size = 10;
            builder.InsertCell().CellFormat.FitText = true;

            if (belongToAgency != null)
            {
                builder.Writeln(belongToAgency);
            }
            builder.CellFormat.Width = 200;
            builder.EndRow();

            font.Bold = false;
            font.Size = 12;

            builder.InsertCell().CellFormat.HorizontalMerge = CellMerge.First;
            if (note != null)
            {
                builder.Writeln(note);
            }
            builder.InsertCell().CellFormat.HorizontalMerge = CellMerge.Previous;
            builder.Writeln("");

            builder.InsertCell().CellFormat.HorizontalMerge = CellMerge.Previous;
            builder.Writeln("");

            builder.InsertCell().CellFormat.HorizontalMerge = CellMerge.Previous;
            builder.Writeln("");
            builder.EndRow();
        }

    }
}