using Portal.Modules.OrientalSails.BusinessLogic;
using Portal.Modules.OrientalSails.BusinessLogic.Share;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Enums;
using Portal.Modules.OrientalSails.Web.UI;
using Portal.Modules.OrientalSails.Web.Util;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class SeriesView : SailsAdminBasePage
    {
        private SeriesViewBLL seriesViewBLL;
        public SeriesViewBLL SeriesViewBLL
        {
            get
            {
                if (seriesViewBLL == null)
                {
                    seriesViewBLL = new SeriesViewBLL();
                }
                return seriesViewBLL;


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
                var seriesId = -1;
                try
                {
                    seriesId = Convert.ToInt32(Request.QueryString["si"]);
                }
                catch { }
                var series = SeriesViewBLL.SeriesGetById(seriesId);

                var haveViewAllSeriesPermission = PermissionBLL.UserCheckPermission(UserBLL.UserGetCurrent().Id, (int)PermissionEnum.VIEWALLSERIES);
                if (!haveViewAllSeriesPermission)
                {
                    if (series.Agency.Sale.Id != userBLL.UserGetCurrent().Id)
                    {
                        ShowError("Không có quyền xem series do người khác quản lý. Liên hệ với manager để cấp quyền");
                        return;
                    }
                }

                try
                {
                    lblSeriesCode.Text = series.SeriesCode + " - " + series.Agency.Name;
                }
                catch { }

                try
                {
                    lblCreatedBy.Text = "Create by " + series.CreatedBy.Name;
                }
                catch { }

                try
                {
                    lblCreatedDate.Text = " at " + series.CreatedDate.Value.ToString("dd/MM/yyyy HH:mm");
                }
                catch { }

                ControlLoadData();
                BookingLoadData();
            }
        }

        public void ControlLoadData()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["tc"]))
            {
                txtTACode.Text = Request.QueryString["tc"];
            }

            if (!string.IsNullOrEmpty(Request.QueryString["bc"]))
            {
                txtBookingCode.Text = Request.QueryString["bc"];
            }

            if (!string.IsNullOrEmpty(Request.QueryString["sd"]))
            {
                txtStartDate.Text = Request.QueryString["sd"];
            }
        }

        public void BookingLoadData()
        {
            var listBooking = SeriesViewBLL.BookingGetAllByQueryString(Request.QueryString).OrderBy(x => x.Status).ThenByDescending(x => x.StartDate);
            rptListBooking.DataSource = listBooking;
            rptListBooking.DataBind();
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (seriesViewBLL != null)
            {
                seriesViewBLL.Dispose();
                seriesViewBLL = null;
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

        protected void rptListBooking_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var bookingId = Convert.ToInt32(e.CommandArgument);
            var booking = SeriesViewBLL.BookingGetById(bookingId);

            if (e.CommandName == "approved")
            {
                booking.Status = StatusType.Approved;
                booking.ModifiedBy = UserBLL.UserGetCurrent();
                booking.ModifiedDate = DateTime.Now;
                SeriesViewBLL.BookingSaveOrUpdate(booking);
                BookingHistorySave(booking);
                ShowSuccess("Đã approved booking này thành công.");
            }
            if (e.CommandName == "cancel")
            {
                booking.Status = StatusType.Cancelled;
                booking.ModifiedBy = UserBLL.UserGetCurrent();
                booking.ModifiedDate = DateTime.Now;
                SeriesViewBLL.BookingSaveOrUpdate(booking);
                BookingHistorySave(booking);
                ShowSuccess("Đã cancel booking này thành công.");
            }
            Reload();
        }

        public string GetSeriesBackground(int status)
        {
            switch (status)
            {
                case (int)Util.StatusType.Approved:
                    return "success";
                case (int)Util.StatusType.Cancelled:
                    return "danger";
                case (int)Util.StatusType.Pending:
                    return "";
                default:
                    return "";
            }
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
            nvcQueryString.Add("si", Request.QueryString["si"]);
            if (!string.IsNullOrEmpty(txtTACode.Text))
            {
                nvcQueryString.Add("tc", txtTACode.Text);
            }

            if (!string.IsNullOrEmpty(txtBookingCode.Text))
            {
                nvcQueryString.Add("bc", txtBookingCode.Text);
            }

            if (!string.IsNullOrEmpty(txtStartDate.Text))
            {
                nvcQueryString.Add("sd", txtStartDate.Text);
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
            SeriesViewBLL.BookingHistorySaveOrUpdate(bookingHistory);
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