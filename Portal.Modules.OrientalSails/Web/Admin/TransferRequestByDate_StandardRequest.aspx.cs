using Portal.Modules.OrientalSails.BusinessLogic;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class TransferRequestByDate_StandardRequest : System.Web.UI.Page
    {
        private TransferRequestByDateBLL transferRequestByDateBLL;
        public TransferRequestByDateBLL TransferRequestByDateBLL
        {
            get
            {
                if (transferRequestByDateBLL == null)
                {
                    transferRequestByDateBLL = new TransferRequestByDateBLL();
                }
                return transferRequestByDateBLL;
            }
        }
        public Route Route
        {
            get
            {
                var routeId = -1;
                try
                {
                    routeId = Int32.Parse(Request.QueryString["r"]);
                }
                catch { }
                var route = TransferRequestByDateBLL.RouteGetById(routeId);
                return route;
            }
        }
        public BusType BusType
        {
            get
            {
                var busTypeId = -1;
                try
                {
                    busTypeId = Int32.Parse(Request.QueryString["bt"]);
                }
                catch { }
                var busType = TransferRequestByDateBLL.BusTypeGetById(busTypeId);
                return busType;
            }
        }
        public string Way
        {
            get
            {
                return Request.QueryString["w"];
            }
        }
        public DateTime? Date
        {
            get
            {
                DateTime? date = null;
                try
                {
                    date = DateTime.ParseExact(Request.QueryString["d"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                catch { }
                return date;
            }
        }
        public LockingTransfer LockingTransfer
        {
            get
            {
                return TransferRequestByDateBLL.LockingTransferGetAllByCriterion(Date).Future().ToList().FirstOrDefault(); ;
            }
        }
        public bool LockingTransferBoolean
        {
            get
            {
                if (LockingTransfer != null)
                {
                    return true;
                }
                return false;
            }
        }
        public int Group
        {
            get
            {
                var group = -1;
                try
                {
                    group = Int32.Parse(Request.QueryString["gr"]);
                }
                catch { }
                return group;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var busTypeStandard = TransferRequestByDateBLL.BusTypeGetAll()
                    .Future()
                    .ToList()
                    .Where(x => x.Name == "Standard").SingleOrDefault();
                rptTransportBookingStandard.DataSource =
                    TransferRequestByDateBLL.BookingGetAllByCriterionTransfer(busTypeStandard, Route, Way, Date)
                    .Future().ToList()
                    .Where(x => x.ListBookingBusByDate
                            .Where(y => y.BusByDate != null && y.BusByDate.Route.Way == Route.Way && y.BusByDate.Route.Group == Route.Group)
                            .ToList()
                            .Count <= 0).ToList();
                rptTransportBookingStandard.DataBind();
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (transferRequestByDateBLL != null)
            {
                transferRequestByDateBLL.Dispose();
                transferRequestByDateBLL = null;
            }
        }

        protected void rptTransportBookingStandard_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var booking = (Booking)e.Item.DataItem;
                var ddlGroup = (DropDownList)e.Item.FindControl("ddlGroup");
                var listBusByDate = TransferRequestByDateBLL.BusByDateGetAllByCriterion(Date, BusType, Route, Way).Future().ToList();
                var listGroup = listBusByDate.Select(x => x.Group).Distinct().OrderBy(x => x).ToList();
                listGroup.ForEach(feGroup =>
                {
                    ddlGroup.Items.Add(new ListItem(BusType.Name[0].ToString().ToUpper() + feGroup.ToString(), feGroup.ToString()));
                });
                var bookingBusByDate = TransferRequestByDateBLL.BookingBusByDateGetAllByCriterion(booking).Future()
                    .ToList().Where(x => x.BusByDate.Route.Id == Route.Id).ToList().FirstOrDefault();
                var group = "0";
                if (bookingBusByDate != null && bookingBusByDate.Id > 0)
                {
                    group = bookingBusByDate.BusByDate.Group.ToString();
                }
                ddlGroup.SelectedValue = group;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveGroup(rptTransportBookingStandard);
            ShowSuccess("Saved successfully");
            Session["ParentNeedReload"] = true;
            Session["Redirect"] = true;
            Response.Redirect(Request.RawUrl);
        }
        public void SaveGroup(Repeater repeater)
        {
            foreach (RepeaterItem repeaterItemBooking in repeater.Items)
            {
                var bookingId = ((HiddenField)repeaterItemBooking.FindControl("hidBookingId")).Value;
                var bookingIdInt = -1;
                try
                {
                    bookingIdInt = Int32.Parse(bookingId);
                }
                catch { }
                var booking = TransferRequestByDateBLL.BookingGetById(bookingIdInt);
                if (booking == null || booking.Id <= 0)
                {
                    ShowErrors("Booking doesn't exist. Please try again");
                    return;
                }
                var ddlGroup = (DropDownList)repeaterItemBooking.FindControl("ddlGroup");
                var group = -1;
                try
                {
                    group = Int32.Parse(ddlGroup.SelectedValue);
                }
                catch { }
                var listBusByDate = new List<BusByDate>();
                if (group != -1)
                {
                    listBusByDate = TransferRequestByDateBLL.BusByDateGetAllByCriterion(Date, BusType, Route, Way, group)
                        .Future().ToList();
                }
                var listBookingBusByDate = TransferRequestByDateBLL.BookingBusByDateGetAllByCriterion(booking)
                                .Future().ToList()
                                .Where(x => x.BusByDate.BusType.Id == BusType.Id)
                                .Where(x => x.BusByDate.Route.Id == Route.Id && x.BusByDate.Route.Way == Way)
                                .ToList();
                if (listBookingBusByDate == null)
                {
                    listBookingBusByDate = new List<BookingBusByDate>();
                }
                for (int i = 0; i < listBusByDate.Count; i++)
                {
                    var busByDate = listBusByDate[i];
                    var bookingBusByDate = new BookingBusByDate();
                    bookingBusByDate.Booking = booking;
                    bookingBusByDate.BusByDate = busByDate;
                    var addMore = true;
                    for (int j = i; j < listBookingBusByDate.Count; j++)
                    {
                        bookingBusByDate = listBookingBusByDate[j];
                        bookingBusByDate.Booking = booking;
                        bookingBusByDate.BusByDate = busByDate;
                        TransferRequestByDateBLL.BookingBusByDateSaveOrUpdate(bookingBusByDate);
                        addMore = false;
                    }
                    if (addMore)
                    {
                        TransferRequestByDateBLL.BookingBusByDateSaveOrUpdate(bookingBusByDate);
                    }
                }
                var numberOfBookingBusByDateUnnecessary = listBookingBusByDate.Count - listBusByDate.Count;
                if (numberOfBookingBusByDateUnnecessary > 0)
                {
                    for (int i = 0; i < numberOfBookingBusByDateUnnecessary; i++)
                    {
                        var bookingBusByDate = listBookingBusByDate.OrderByDescending(x => x.Id).ToList()[i];
                        TransferRequestByDateBLL.BookingBusByDateDelete(bookingBusByDate);
                    }
                }
            }
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
    }
}