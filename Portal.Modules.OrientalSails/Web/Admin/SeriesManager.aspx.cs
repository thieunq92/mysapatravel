using CMS.Core.Domain;
using Portal.Modules.Orientalsails.Service.Share;
using Portal.Modules.OrientalSails.BusinessLogic;
using Portal.Modules.OrientalSails.BusinessLogic.Share;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Enums;
using Portal.Modules.OrientalSails.Web.UI;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class SeriesManager : SailsAdminBasePage
    {
        private SeriesManagerBLL seriesManagerBLL;
        public SeriesManagerBLL SeriesManagerBLL
        {
            get
            {
                if (seriesManagerBLL == null)
                {
                    seriesManagerBLL = new SeriesManagerBLL();
                }
                return seriesManagerBLL;
            }
        }

        private UserBLL userBLL;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Role role = Module.RoleGetById(Convert.ToInt32(Module.ModuleSettings("Sale")));
                ddlSalesInCharge.DataSource = role.Users;
                ddlSalesInCharge.DataTextField = "Username";
                ddlSalesInCharge.DataValueField = "Id";
                ddlSalesInCharge.DataBind();

                ControlLoadData();
                SeriesLoadData();
            }

        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (seriesManagerBLL != null)
            {
                seriesManagerBLL.Dispose();
                seriesManagerBLL = null;
            }

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

        public void ControlLoadData()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["p"]))
            {
                txtPartner.Text = Request.QueryString["p"];
            }

            if (!string.IsNullOrEmpty(Request.QueryString["sc"]))
            {
                txtSeriesCode.Text = Request.QueryString["sc"];
            }

            if (Request.QueryString["sic"] != null)
            {
                ddlSalesInCharge.SelectedValue = Request.QueryString["sic"];
            }
        }

        public void SeriesLoadData()
        {
            int count = 0;
            var listSeries = SeriesManagerBLL.SeriesBookingGetAllByQueryString(Request.QueryString, pagerSeries.PageSize,
                pagerSeries.CurrentPageIndex, out count).OrderByDescending(x => x.CreatedDate);
            rptListSeries.DataSource = listSeries;
            pagerSeries.AllowCustomPaging = true;
            pagerSeries.VirtualItemCount = count;
            rptListSeries.DataBind();
        }

        protected void rptListSeries_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var seriesId = Int32.Parse(e.CommandArgument.ToString());
            var series = SeriesManagerBLL.SeriesGetById(seriesId);
            var haveCancelAllSeriesPermission = PermissionBLL.UserCheckPermission(UserBLL.UserGetCurrent().Id, (int)PermissionEnum.CANCELALLSERIES);
            if (e.CommandName == "cancel")
            {
                if (!haveCancelAllSeriesPermission)
                {
                    if (series.Agency.Sale.Id != UserBLL.UserGetCurrent().Id)
                    {
                        ShowError("Không được phép xóa series do người khác quản lý. Liên hệ manager để được cấp quyền ");
                        return;
                    }
                }

                var numberOfBookingApproved = series.ListBooking.Where(x => x.Status == Util.StatusType.Approved).Count();
                var didSeriesHaveBookingApproved = false;
                if (numberOfBookingApproved > 0)
                {
                    didSeriesHaveBookingApproved = true;
                }

                var listMustCancel = new List<Booking>();
                foreach (var booking in series.ListBooking)
                {
                    if (booking.StartDate <= DateTime.Now.Date)
                    {
                        continue;
                    }
                    if (didSeriesHaveBookingApproved)
                    {
                        if (booking.Status == Util.StatusType.Pending)
                        {
                            listMustCancel.Add(booking);
                        }
                    }
                    else
                    {
                        listMustCancel.Add(booking);
                    }
                }

                foreach (var booking in listMustCancel)
                {
                    booking.Status = Util.StatusType.Cancelled;
                    booking.ModifiedBy = UserBLL.UserGetCurrent();
                    booking.ModifiedDate = DateTime.Now;
                    SeriesManagerBLL.BookingSaveOrUpdate(booking);
                    BookingHistorySave(booking);
                }
                ShowSuccess("Đã cancel tất cả booking pending trong series <strong>" + series.SeriesCode + "</strong>");

                if (listMustCancel.Count() > 0)
                    SendEmailCancelSeries(listMustCancel, series);
            }

            Reload();
        }

        public void SendEmailCancelSeries(IList<Booking> listMustCancel, Series series)
        {
            MailMessage message = new MailMessage();
            MailAddress fromAddress = new MailAddress("no-reply@orientalsails.com", "Hệ Thống MO OrientalSails");
            message.From = fromAddress;
            message.To.Add("it2@atravelmate.com");
            var emailSalesInCharge = "";
            try {
                emailSalesInCharge = series.Agency.Sale.Email;
            }
            catch { }
            //message.To.Add(emailSalesInCharge);
            //message.To.Add(UserBLL.UserGetCurrent().Email);
            message.Subject = "Thông báo việc hủy series";

            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modules/Sails/Admin/EmailTemplate/CancelSeriesTemplate.htm")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{seriescode}", series.SeriesCode);
            try
            {
                body = body.Replace("{agency}", series.Agency.Name);
            }
            catch { }

            try
            {
                body = body.Replace("{booker}", series.Booker.Name);
            }
            catch { }

            try
            {
                body = body.Replace("{salesincharge}", series.Agency.Sale.FullName);
            }
            catch { }

            try
            {
                body = body.Replace("{numberofbooking}", series.ListBooking.Count().ToString());
            }
            catch { }

            body = body.Replace("{cutoffdate}", series.CutoffDate.ToString());
            body = body.Replace("{noofdays}", series.NoOfDays.ToString());
            body = body.Replace("{submiter}", UserBLL.UserGetCurrent().Name);

            var appPath = string.Format("{0}://{1}{2}{3}",
                                Request.Url.Scheme,
                                Request.Url.Host,
                                Request.Url.Port == 80
                                    ? string.Empty
                                    : ":" + Request.Url.Port,
                                Request.ApplicationPath);
            body = body.Replace("{link}", appPath + "Modules/Sails/Admin/BookingView.aspx?NodeId=1&SectionId=15&si=" + series.Id);
            message.Body = body;

            var bookingitems = "";
            foreach (var booking in listMustCancel)
            {
                var tripCode = "";
                try
                {
                    tripCode = booking.Trip.TripCode;
                }
                catch { }

                var cruise = "";
                try
                {
                    cruise = booking.Cruise.Name;
                }
                catch { }

             
                bookingitems = bookingitems +
                   
                        booking.Id +
                        tripCode +
                        cruise +
                         booking.Pax +
                        booking.BookingRooms.Count();  
            }
            body = body.Replace("{bookingitems}", bookingitems);
            EmailService emailService = new EmailService();
            emailService.SendMessage(message);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Request.Url.GetLeftPart(UriPartial.Path) + QueryStringBuildByCriterion());
        }

        public string QueryStringBuildByCriterion()
        {
            NameValueCollection nvcQueryString = new NameValueCollection();
            nvcQueryString.Add("NodeId", "1");
            nvcQueryString.Add("SectionId", "15");
            if (!string.IsNullOrEmpty(txtPartner.Text))
            {
                nvcQueryString.Add("p", txtPartner.Text);
            }

            if (!string.IsNullOrEmpty(txtSeriesCode.Text))
            {
                nvcQueryString.Add("sc", txtSeriesCode.Text);
            }

            if (!string.IsNullOrEmpty(ddlSalesInCharge.SelectedValue))
            {
                nvcQueryString.Add("sic", ddlSalesInCharge.SelectedValue);
            }
            var criterions = (from key in nvcQueryString.AllKeys
                              from value in nvcQueryString.GetValues(key)
                              select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value))).ToArray();

            return "?" + string.Join("&", criterions);
        }

        public void BookingHistorySave(Booking booking)
        {
            var bookingHistory = new BookingHistory()
            {
                Booking = booking,
                Date = DateTime.Now,
                User = UserBLL.UserGetCurrent(),
                CabinNumber = booking.BookingRooms.Count,
                CustomerNumber = booking.Pax,
                StartDate = booking.StartDate,
                Status = booking.Status,
                Trip = booking.Trip,
                Agency = booking.Agency,
                Total = booking.Total,
                TotalCurrency = booking.IsTotalUsd ? "USD" : "VND",
            };
            SeriesManagerBLL.BookingHistorySaveOrUpdate(bookingHistory);
        }

        public string SeriesGetStatus(int seriesId)
        {
            var series = SeriesManagerBLL.SeriesGetById(seriesId);
            var result = "";
            var listBookingGroupByStatus = series.ListBooking.GroupBy(x => x.Status);
            foreach (var bookingGroupByStatus in listBookingGroupByStatus)
            {
                result = result + bookingGroupByStatus.Count() + " " + bookingGroupByStatus.Key.ToString() + "</br>";
            }
            return result;
        }

        public void ShowWarning(string warning)
        {
            Session["WarningMessage"] = "<strong>Warning!</strong> " + warning + "<br/>" + Session["WarningMessage"];
        }

        public void ShowErrors(string error)
        {
            Session["ErrorMessage"] = "<strong>Error!</strong> " + error + "<br/>" + Session["ErrorMessage"];
        }

        public void ShowSuccess(string success)
        {
            Session["SuccessMessage"] = "<strong>Success!</strong> " + success + "<br/>" + Session["SuccessMessage"];
        }

        public void Reload()
        {
            Response.Redirect(Request.RawUrl);
        }
    }
}