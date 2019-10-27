using Portal.Modules.OrientalSails.BusinessLogic;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.Util;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class TransferRequestByDate : System.Web.UI.Page
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

        public DateTime? Date
        {
            get
            {
                DateTime? date = DateTime.Now.Date;
                try
                {
                    date = DateTime.ParseExact(Request.QueryString["d"], "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }
                catch { }
                return date;
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
                if (busType == null || busType.Id <= 0)
                {
                    return new BusType() { Id = -1 };
                }
                return busType;
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
                if (route == null || route.Id <= 0)
                {
                    var listRoute = TransferRequestByDateBLL.RouteGetAll().Future().ToList();
                    return listRoute.Where(x => x.Way == "To").FirstOrDefault();
                }
                return route;
            }
        }
        public LockingTransfer LockingTransfer
        {
            get
            {
                return TransferRequestByDateBLL.LockingTransferGetAllByCriterion(Date).Future().ToList().FirstOrDefault(); ;
            }
        }
        public string LockingTransferString
        {
            get
            {
                if (LockingTransfer != null)
                {
                    return "true";
                }
                return "false";
            }

        }
        public string Order
        {
            get
            {
                var order = "";
                if (!String.IsNullOrEmpty(Request.QueryString["order"]))
                {
                    order = Request.QueryString["order"];
                }
                return order;
            }
        }
        public string Sort
        {
            get
            {
                var sort = "trip";
                if (!String.IsNullOrEmpty(Request.QueryString["sort"]))
                {
                    sort = Request.QueryString["sort"];
                }
                return sort;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDate.Text = Date.HasValue ? Date.Value.ToString("dd/MM/yyyy") : DateTime.Now.Date.ToString("dd/MM/yyyy");
                rptRoute.DataSource = TransferRequestByDateBLL.RouteGetAll().Where(x => x.Way == "To").Future().ToList();
                rptRoute.DataBind();
                rptBusType.DataSource = TransferRequestByDateBLL.BusTypeGetAll().Future().ToList();
                rptBusType.DataBind();
                if (LockingTransfer == null)
                {
                    btnLockDate.Visible = true;
                    btnUnlockDate.Visible = false;
                }
                else
                {
                    btnUnlockDate.Visible = true;
                    btnLockDate.Visible = false;
                }
                rptRouteByWay.DataSource = TransferRequestByDateBLL.RouteGetAllById(Route.Id).Future().ToList();
                rptRouteByWay.DataBind();
            }
        }
        public string TableRowColorGetByGroup(Booking booking, Route route)
        {
            var bookingBusByDate = TransferRequestByDateBLL.BookingBusByDateGetAllByCriterion(booking).Future()
                  .ToList().Where(x => x.BusByDate.Route.Id == route.Id).ToList().FirstOrDefault();
            var group = 0;
            if (bookingBusByDate != null && bookingBusByDate.Id > 0)
            {
                group = bookingBusByDate.BusByDate.Group;
            }
            if (group > 0)
            {
                return "";
            }
            return "custom-warning";
        }
        protected void Page_Unload(object sender, EventArgs e)
        {
            if (transferRequestByDateBLL != null)
            {
                transferRequestByDateBLL.Dispose();
                transferRequestByDateBLL = null;
            }
        }

        protected void btnDisplay_Click(object sender, EventArgs e)
        {
            Response.Redirect(GetCurrentPagePathWithoutQueryString() + QueryStringBuildByCriterion());
        }

        public string QueryStringBuildByCriterion()
        {
            return QueryStringBuildByCriterion(-1, -1);
        }
        public string QueryStringBuildByCriterionBusType(int busTypeId)
        {
            return QueryStringBuildByCriterion(busTypeId, -1);
        }
        public string QueryStringBuildByCriterionRoute(int routeId)
        {
            return QueryStringBuildByCriterion(-1, routeId);
        }
        public string QueryStringBuildByCriterion(int busTypeId, int routeId)
        {
            NameValueCollection nvcQueryString = new NameValueCollection();
            nvcQueryString.Add("NodeId", "1");
            nvcQueryString.Add("SectionId", "15");

            if (!string.IsNullOrEmpty(txtDate.Text))
            {
                nvcQueryString.Add("d", txtDate.Text);
            }

            if (busTypeId <= -1)
            {
                nvcQueryString.Add("bt", BusType.Id.ToString());
            }
            else
            {
                nvcQueryString.Add("bt", busTypeId.ToString());
            }

            if (routeId <= -1)
            {
                nvcQueryString.Add("r", Route.Id.ToString());
            }
            else
            {
                nvcQueryString.Add("r", routeId.ToString());
            }
            var criterions = (from key in nvcQueryString.AllKeys
                              from value in nvcQueryString.GetValues(key)
                              select string.Format("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(value))).ToArray();

            return "?" + string.Join("&", criterions);
        }
        public string GetCurrentPagePathWithoutQueryString()
        {
            string url = HttpContext.Current.Request.Url.AbsoluteUri;
            string path = url.Substring(0, url.IndexOf("?"));
            return path;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem repeaterItemRouteByWay in rptRouteByWay.Items)
            {
                var routeId = ((HiddenField)repeaterItemRouteByWay.FindControl("hidRouteId")).Value;
                var routeIdInt = -1;
                try
                {
                    routeIdInt = Int32.Parse(routeId);
                }
                catch { }
                var route = TransferRequestByDateBLL.RouteGetById(routeIdInt);
                if (route == null || route.Id <= 0)
                {
                    ShowErrors("Route doesn't exist. Please try again");
                    return;
                }
                var rptBusType = (Repeater)repeaterItemRouteByWay.FindControl("rptBusType");
                foreach (RepeaterItem repeaterItemBusType in rptBusType.Items)
                {
                    var busTypeId = ((HiddenField)repeaterItemBusType.FindControl("hidBusTypeId")).Value;
                    var busTypeIdInt = -1;
                    try
                    {
                        busTypeIdInt = Int32.Parse(busTypeId);
                    }
                    catch { }
                    var busType = TransferRequestByDateBLL.BusTypeGetById(busTypeIdInt);
                    if (route == null || route.Id <= 0)
                    {
                        ShowErrors("Bus type doesn't exist. Please try again");
                        return;
                    }
                    var rptTransportBooking = (Repeater)repeaterItemBusType.FindControl("rptTransportBooking");
                    //Xóa hết các liên kết BookingBusByDate cũ
                    var listBusByDate = TransferRequestByDateBLL.BusByDateGetAllByCriterion(Date, busType, route, route.Way)
                     .Future().ToList();
                    ClearOldBookingBusByDate(listBusByDate);
                    //--
                    foreach (RepeaterItem repeaterItemBooking in rptTransportBooking.Items)
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
                        //Tạo các liên kết BookingBusByDate mới
                        if (group != -1)
                        {
                            var busByDate = TransferRequestByDateBLL.BusByDateGetAllByCriterion(Date, busType, route, route.Way, group)
                                .Future().ToList().FirstOrDefault();
                            var bookingBusByDate = new BookingBusByDate()
                            {
                                Booking = booking,
                                BusByDate = busByDate,
                            };
                            TransferRequestByDateBLL.BookingBusByDateSaveOrUpdate(bookingBusByDate);
                        }
                        //--
                    }
                }
            }
            ShowSuccess("Saved successfully");
            Session["Redirect"] = true;
            Response.Redirect(Request.RawUrl);
        }
        private void ClearOldBookingBusByDate(IList<BusByDate> listBusByDate)
        {
            foreach (var busByDate in listBusByDate)
            {
                var listBookingBusByDate = TransferRequestByDateBLL
                    .BookingBusByDateGetAllByCriterion(busByDate).Future().ToList();
                listBookingBusByDate.ForEach(x =>
                {
                    TransferRequestByDateBLL.BookingBusByDateDelete(x);
                });
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

        protected void btnLockDate_Click(object sender, EventArgs e)
        {
            var lockingTransfer = LockingTransfer;
            if (lockingTransfer == null)
            {
                lockingTransfer = new LockingTransfer();
            }
            lockingTransfer.Date = Date;
            TransferRequestByDateBLL.LockingTransferSaveOrUpdate(lockingTransfer);
            ShowSuccess("Locked date successfully");
            Response.Redirect(Request.RawUrl);
        }

        protected void btnUnlockDate_Click(object sender, EventArgs e)
        {
            var lockingTransfer = LockingTransfer;
            if (lockingTransfer == null)
            {
                return;
            }
            TransferRequestByDateBLL.LockingTransferDelete(lockingTransfer);
            ShowSuccess("Unlocked date successfully");
            Response.Redirect(Request.RawUrl);
        }

        protected void rptTransportBooking_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var booking = (Booking)e.Item.DataItem;
                var busType = (BusType)((RepeaterItem)e.Item.Parent.Parent).DataItem;
                var route = (Route)((RepeaterItem)e.Item.Parent.Parent.Parent.Parent).DataItem;
                var ddlGroup = (DropDownList)e.Item.FindControl("ddlGroup");
                var listBusByDate = TransferRequestByDateBLL.BusByDateGetAllByCriterion(Date, busType, route, route.Way)
                    .Future().ToList();
                var listGroup = listBusByDate.Select(x => x.Group).Distinct().OrderBy(x => x).ToList();
                listGroup.ForEach(noOfGroup =>
                {
                    ddlGroup.Items.Add(new ListItem(busType.Name[0].ToString().ToUpper() + noOfGroup.ToString()
                        , noOfGroup.ToString()));
                });
                var bookingBusByDate = TransferRequestByDateBLL.BookingBusByDateGetAllByCriterion(booking).Future()
                    .ToList().Where(x => x.BusByDate.Route.Id == route.Id).ToList().FirstOrDefault();
                var group = "--";
                if (bookingBusByDate != null && bookingBusByDate.Id > 0)
                {
                    group = bookingBusByDate.BusByDate.Group.ToString();
                }
                ddlGroup.SelectedValue = group;
                var lblGroup = (Label)e.Item.FindControl("lblGroup");
                if (bookingBusByDate != null 
                    && bookingBusByDate.BusByDate != null
                    && bookingBusByDate.BusByDate.BusType != null)
                    lblGroup.Text = bookingBusByDate.BusByDate.BusType.Name.First().ToString().ToUpper() + group;
                else
                    lblGroup.Text = group;
            }
        }

        protected void rptRouteByWay_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var rptBusType = (Repeater)e.Item.FindControl("rptBusType");
                var listBusType = TransferRequestByDateBLL.BusTypeGetAll().Future().ToList();
                if (BusType != null && BusType.Id > 0)
                {
                    listBusType = listBusType.Where(x => x.Id == BusType.Id).ToList();
                }
                rptBusType.DataSource = listBusType;
                rptBusType.DataBind();
            }
        }

        protected void rptBusType_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var busType = (BusType)e.Item.DataItem;
                var route = (Route)((RepeaterItem)e.Item.Parent.Parent).DataItem;
                var rptTransportBooking = (Repeater)e.Item.FindControl("rptTransportBooking");
                var listTransportBooking = TransferRequestByDateBLL
                    .BookingGetAllByCriterionTransfer(busType, route, route.Way, Date)
                    .Future()
                    .ToList();
                if (busType.Name == "Standard")
                {
                    var listBookingUpgraded = new List<Booking>();
                    listTransportBooking.ForEach(standardBooking =>
                    {
                        var bookingUpgraded = standardBooking.ListBookingBusByDate
                          .Any(x => x.BusByDate != null && x.BusByDate.BusType.Id != standardBooking.Transfer_BusType.Id);
                        if (bookingUpgraded)
                        {
                            listBookingUpgraded.Add(standardBooking);
                        }
                    });
                    listTransportBooking = listTransportBooking.Except(listBookingUpgraded).ToList();
                }
                if (busType.Name == "Limousine")
                {
                    var standardBusType = TransferRequestByDateBLL.BusTypeGetAll()
                        .Future()
                        .ToList()
                        .Where(x => x.Name == "Standard")
                        .SingleOrDefault();
                    var listStandardTransportBooking = TransferRequestByDateBLL.BookingGetAllByCriterionTransfer(standardBusType, route, route.Way, Date)
                        .Future()
                        .ToList()
                        .Where(x => x
                            .ListBookingBusByDate
                            .Where(y => y.BusByDate != null && y.BusByDate.BusType.Name == "Limousine"
                                && y.BusByDate.Route.Way == route.Way
                                && y.BusByDate.Route.Group == route.Group)
                            .ToList()
                            .Count > 0)
                        .ToList();
                    listStandardTransportBooking.ForEach(standardTransportBooking =>
                    {
                        standardTransportBooking.Transfer_Upgraded = true;
                        listTransportBooking.Add(standardTransportBooking);
                    });
                }
                rptTransportBooking.DataSource = listTransportBooking.Where(x => x.Status == StatusType.Approved)
                    .Where(x => x.Deleted == false).ToList();
                rptTransportBooking.DataBind();
                var trBusType = (HtmlTableRow)e.Item.FindControl("trBusType");
                if (busType.Name != "Standard" && listTransportBooking.Count <= 0)
                {
                    trBusType.Visible = false;
                }
                if (BusType != null && BusType.Id > 0)
                {
                    trBusType.Visible = false;
                }
                if (listTransportBooking.Any(x => x.ListBookingBusByDate.Count <= 0))
                {
                    trBusType.Attributes.Add("class", "custom-text-danger");
                }
            }
        }
    }
}