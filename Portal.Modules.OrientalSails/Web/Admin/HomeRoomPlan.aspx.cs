using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Portal.Modules.OrientalSails.BusinessLogic.Share;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Enums;
using Portal.Modules.OrientalSails.Web.UI;
using Portal.Modules.OrientalSails.Web.Util;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    /// <summary>
    /// trang trộn phòng, đổi phòng
    /// </summary>
    public partial class HomeRoomPlan : SailsAdminBasePage
    {
        private IList<Cruise> _cruises;
        private IList<Room> _roomCruises;
        private IList<Booking> _currentDayBookings;
        private IList<Booking> _nextDayBookings;
        private IList<Booking> _allBooking;
        private List<String> color = new List<string> { "#009688", "#2196F3", "#00BCD4", "#8BC34A",
            "#FFC107", "#F44336", "#E91E63", "#9C27B0", "#3F51B5", "#795548", "#9E9E9E","#ffc0cb",
            "#ff4e00","#0b3bf5","#ffff00","#f0ad4e","#d9534f","#a94442","#337ab7","#8a6d3b",
            "#e5a7dd","#e10fc6","#730fe1","#3d3ada","#3acbda"
        };
        public Dictionary<string, int> _currentRoomDic;

        public Dictionary<int, string> BookingColor = new Dictionary<int, string>();

        private List<int> _floors = new List<int>();
        public Cruise _currentCruise = new Cruise();
        public DateTime _currentDate = DateTime.Now;
        private PermissionBLL permissionBLL;

        public PermissionBLL PermissionUtil
        {
            get
            {
                if (permissionBLL == null)
                    permissionBLL = new PermissionBLL();
                return permissionBLL;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!PermissionUtil.UserCheckPermission(UserIdentity.Id, (int)PermissionEnum.VIEW_HOME_PLAN))
            {
                ShowErrors("You don't have permission to perform this action");
                return;
            }
            _cruises = Module.CruiseGetByUser(UserIdentity);

            var cruiseId = Request["cruiseId"];
            if (!string.IsNullOrWhiteSpace(cruiseId))
            {
                _currentCruise = _cruises.FirstOrDefault(c => c.Id == Convert.ToInt32(cruiseId));
            }
            else if (_cruises != null && _cruises.Count > 0)
            {
                _currentCruise = _cruises.FirstOrDefault();
            }

            if (!IsPostBack)
            {
                var dateStr = Request["date"];
                if (!string.IsNullOrWhiteSpace(dateStr))
                {
                    txtStartDate.Text = dateStr;
                }
                else txtStartDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }

            _currentDate = DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (_currentCruise != null && _currentCruise.Id > 0)
            {
                _roomCruises = Module.RoomGetAll2(_currentCruise);
                _currentDayBookings = Module.GetBookingByDate(_currentDate, _currentCruise);
                _nextDayBookings = Module.GetBookingByDate(_currentDate.AddDays(1), _currentCruise);
                _allBooking = _currentDayBookings.Concat(_nextDayBookings).ToList();
                ShowEmptyRoomDay(_currentDate, litCurrentRooms, true);
            }
            if (!IsPostBack)
            {
                GetDataRoomInFloor();
                rptCruises.DataSource = _cruises;
                rptCruises.DataBind();
            }
        }

        private void GetDataRoomInFloor()
        {
            for (int i = 1; i <= _currentCruise.NumberOfFloors; i++)
            {
                _floors.Add(i);
            }
            rptFloors.DataSource = _floors;
            rptFloors.DataBind();
        }

        private void ShowEmptyRoomDay(DateTime dateTime, Literal litStatus, bool isCurrentDay)
        {
            var list = new List<Room>(_roomCruises);
            var pax = 0;
            var cabins = 0;
            var bks = new List<int>();
            foreach (Room room in _roomCruises)
            {
                var booking = FindBooking(room, dateTime);
                if (booking != null && booking.Id > 0)
                {
                    list.Remove(room);
                    if (bks.IndexOf(booking.Id) < 0)
                    {
                        bks.Add(booking.Id);
                        pax += booking.Pax;
                    }
                    cabins++;
                }
            }
            var keypairsAll = new Dictionary<string, int>();
            foreach (Room room in _roomCruises)
            {
                if (keypairsAll.ContainsKey(room.RoomClass.Name + " " + room.RoomType.Name))
                {
                    keypairsAll[room.RoomClass.Name + " " + room.RoomType.Name] = keypairsAll[room.RoomClass.Name + " " + room.RoomType.Name] + 1;
                }
                else
                {
                    keypairsAll.Add(room.RoomClass.Name + " " + room.RoomType.Name, 1);
                }
            }
            var str = "";

            if (list.Count > 0)
            {
                var keypairs = new Dictionary<string, int>();
                foreach (Room room in list)
                {
                    if (keypairs.ContainsKey(room.RoomClass.Name + " " + room.RoomType.Name))
                    {
                        keypairs[room.RoomClass.Name + " " + room.RoomType.Name] = keypairs[room.RoomClass.Name + " " + room.RoomType.Name] + 1;
                    }
                    else
                    {
                        keypairs.Add(room.RoomClass.Name + " " + room.RoomType.Name, 1);
                    }
                }
                for (int i = 0; i < keypairs.Count; i++)
                {
                    if (i < keypairs.Count - 1)
                        str = str + string.Format("{0}/{2} {1},", keypairs.ElementAt(i).Value, keypairs.ElementAt(i).Key, keypairsAll[keypairs.ElementAt(i).Key]);
                    else
                    {
                        str = str + string.Format("{0}/{2} {1}", keypairs.ElementAt(i).Value, keypairs.ElementAt(i).Key, keypairsAll[keypairs.ElementAt(i).Key]);

                    }
                }
                //foreach (var roomCount in keypairs)
                //{
                //    str = str + string.Format("{0} {1},", roomCount.Value, roomCount.Key);
                //}
            }
            else str = string.Format("0/{0} cabins", _roomCruises.Count);
            litStatus.Text = string.Format("{0} pax / {1} cabins, {2}", pax, cabins, str);

        }
        private Booking FindBooking(Room room, DateTime date)
        {
            Booking booking = null;
            if (_allBooking != null)
            {
                foreach (Booking vBookingRoom in _allBooking)
                {
                    var numberOfDay = vBookingRoom.StartDate.AddDays(vBookingRoom.Trip.NumberOfDay - 2);
                    var roomIsExist = vBookingRoom.BookingRooms.Any(br => br.Room != null && br.Room.Id == room.Id);

                    if (roomIsExist && vBookingRoom.Status != StatusType.Cancelled &&
                        date >= vBookingRoom.StartDate &&
                        date <= numberOfDay)
                    {
                        booking = vBookingRoom;
                        break;
                    }
                    else if (roomIsExist && vBookingRoom.Status != StatusType.Cancelled &&
                             date == vBookingRoom.StartDate && vBookingRoom.Trip.NumberOfDay == 1)
                    {
                        booking = vBookingRoom;
                        break;
                    }
                    else if (roomIsExist && vBookingRoom.Status != StatusType.Cancelled &&
                             date == vBookingRoom.StartDate)
                    {
                        booking = vBookingRoom;
                        break;
                    }

                }
            }
            return booking;
        }
        protected virtual void rptCruises_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var cruise = e.Item.DataItem as Cruise;
            if (cruise != null)
            {
                var hplCruise = e.Item.FindControl("hplCruise") as HyperLink;
                if (hplCruise != null)
                {
                    var rooms = Module.RoomGetAll2(cruise);
                    var totalRoom = rooms.Count();
                    var totalEmptyRooms = totalRoom;
                    var bookings = Module.GetBookingByDate(_currentDate, cruise);
                    foreach (Room room in rooms)
                    {
                        var check = CheckRoomExist(room, bookings);
                        if (check)
                            totalEmptyRooms--;
                    }

                    hplCruise.Text = string.Format("{0} ({1}/{2})", cruise.Name, totalEmptyRooms, totalRoom);
                    var dateStr = Request["date"];
                    if (string.IsNullOrWhiteSpace(dateStr))
                        dateStr = DateTime.Now.ToString("dd/MM/yyyy");
                    hplCruise.NavigateUrl = string.Format("HomeRoomPlan.aspx{0}&cruiseId={1}&date={2}", GetBaseQueryString(),
                        cruise.Id, dateStr);
                    if (cruise.Id == _currentCruise.Id) hplCruise.CssClass = "btn cruiseActive";
                }
            }
        }

        public bool CheckRoomExist(Room room, IList<Booking> bookings)
        {
            var check = false;
            foreach (Booking booking in bookings)
            {
                foreach (BookingRoom bookingRoom in booking.BookingRooms)
                {
                    if (bookingRoom.Room != null && bookingRoom.Room.Id == room.Id)
                    {
                        check = true;
                        break;
                    }
                }
            }
            return check;
        }

        protected void rptFloors_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var floor = e.Item.DataItem as int? ?? 0;
            if (floor > 0)
            {
                var rptRoomsDay = e.Item.FindControl("rptRoomsDay") as Repeater;
                var rooms = _roomCruises.Where(r => r.Floor == floor);
                if (!string.IsNullOrWhiteSpace(Request["status"]))
                {
                    var status = (RoomType)Enum.Parse(typeof(RoomType), Request["status"]);
                    rooms = rooms.Where(r => r.Status == status);
                }
                if (rptRoomsDay != null)
                {
                    rptRoomsDay.DataSource = rooms;
                    rptRoomsDay.DataBind();
                }
            }
        }

        protected void rptRoomsDay_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var room = e.Item.DataItem as Room;
            if (room != null)
            {
                var hplBooking = e.Item.FindControl("hplBooking") as HyperLink;
                var divBooking = e.Item.FindControl("divBooking") as HtmlControl;
                var divConnectRoom = e.Item.FindControl("divConnectRoom") as HtmlControl;
                var litCustomer = e.Item.FindControl("litCustomer") as Literal;
                var litCheckInfo = e.Item.FindControl("litCheckInfo") as Literal;
                var lblR = e.Item.FindControl("lblR") as Literal;
                var lblSR = e.Item.FindControl("lblSR") as Literal;
                var lblEX = e.Item.FindControl("lblEX") as Literal;
                var lblL = e.Item.FindControl("lblL") as Literal;
                var litPax = e.Item.FindControl("litPax") as Literal;
                var litRoomName = e.Item.FindControl("litRoomName") as Literal;
                var lblA = e.Item.FindControl("lblA") as Literal;
                var litAction = e.Item.FindControl("litAction") as Literal;

                if (litRoomName != null)
                {
                    litRoomName.Text = room.Name;
                }
                divConnectRoom.Attributes.Add("rtype", room.RoomType.Name.ToLower());
                var nextDaybooking = FindBooking(room, _currentDate.AddDays(1));
                if (nextDaybooking != null)
                {
                    if (_currentDayBookings.Contains(nextDaybooking))
                    {
                        divConnectRoom.Attributes.Add("afn", "3D");
                    }
                    else
                    {
                        if (litCheckInfo != null) litCheckInfo.Text = "<div class='checkCusInfo'></div>";
                        divConnectRoom.Attributes.Add("afn", "2D");
                    }
                }
                else
                {
                    divConnectRoom.Attributes.Add("afn", "3D");
                }

                if (lblL != null) lblL.Text = string.Format("<div class=\"card-link-left roomClass\">{0}</div>", room.RoomClass.Name);
                if (lblR != null) lblR.Text = string.Format("<div class=\"card-link-roomType\"><img src='/Modules/Sails/Themes/images/{0}.png'/></div>", room.RoomType.Name.ToLower());
                if (hplBooking != null)
                {
                    Booking booking = FindBooking(room, _currentDate);

                    if (booking != null)
                    {
                        if (!BookingColor.ContainsKey(booking.Id))
                        {
                            BookingColor.Add(booking.Id, color[BookingColor.Count]);
                        }
                        hplBooking.Text = string.Format("{0:OS00000} ({1}D)", booking.Id, booking.Trip.NumberOfDay);
                        if (lblSR != null)
                        {
                            lblSR.Text = string.Format("<i class=\"fa fa-info-circle fa-lg\" aria-hidden=\"true\" data-toggle=\"tooltip\" data-placement=\"right\" title=\"{0}\"></i>", booking.SpecialRequest);
                        }
                        if (lblEX != null)
                        {
                            if (booking.BookingRooms.Count > 0)
                            {
                                var bkr = booking.BookingRooms.FirstOrDefault(b => b.Room.Id == room.Id);
                                if (bkr != null && bkr.HasAddExtraBed)
                                    lblEX.Text = "<i class=\"fas fa-plus\" aria-hidden=\"true\" data-toggle=\"tooltip\" data-placement=\"right\" style=\" color: red; \" title=\"Extra bed needed\"></i>";
                            }
                        }
                        if (divBooking != null)
                        {
                            divBooking.Attributes.Add("style",
                               string.Format("background-color:{0}", BookingColor[booking.Id]));
                            divBooking.Attributes.Add("bkd", booking.Trip.NumberOfDay + "D");
                            if (booking.Trip.NumberOfDay > 2 && booking.StartDate < _currentDate)
                            {
                                divBooking.Attributes.Add("bkld", "1");
                            }
                            else if (booking.Trip.NumberOfDay > 2)
                            {
                                divBooking.Attributes.Add("bkld", "0");

                            }
                            divBooking.Attributes.Remove("class");
                            divBooking.Attributes.Add("class", "card-block round booking-room");
                        }
                        var listRoom = booking.BookingRooms.Where(c => c.Room != null && c.Room.Id == room.Id).ToList();

                        if (litCustomer != null)
                        {
                            var cus = "";
                            var listCus = new List<Customer>();
                            foreach (BookingRoom bookingRoom in listRoom)
                            {
                                listCus.AddRange(bookingRoom.Customers);
                            }
                            if (listCus.Count > 0)
                            {
                                for (int i = 0; i < listCus.Count(); i++)
                                {
                                    if (!string.IsNullOrWhiteSpace(listCus[i].Fullname))
                                    {
                                        if (i < listCus.Count - 1)
                                        {
                                            cus += listCus[i].Fullname + "</br>";
                                        }
                                        else
                                        {
                                            cus += listCus[i].Fullname;
                                        }
                                    }

                                }
                            }
                            litCustomer.Text = cus;
                            if (lblA != null) lblA.Text = string.Format("<div class=\"card-link-left\">{0}</div>", booking.Agency.Name);
                            if (litPax != null) litPax.Text = string.Format("<div class=\"card-link-pax\">{0}</div>", listCus.Count);
                            if (listRoom.Count > 0 && litAction != null)
                            {
                                var rtype = listRoom[0].RoomType.Name.ToLower();
                                divBooking.Attributes.Add("bkRtype", rtype.ToLower());
                                divBooking.Attributes.Add("bkrId", listRoom[0].Id.ToString());
                                if (rtype == "double" && room.RoomType.Name.ToLower() == "twin") rtype = "double_c";
                                litAction.Text = string.Format("<div class=\"card-link-action\"><img src='/Modules/Sails/Themes/images/{0}.png' /></div>", rtype);
                                if (listRoom[0].HasAddExtraBed)
                                {
                                    litAction.Text = string.Format("<div class=\"card-link-action\"><img src='/Modules/Sails/Themes/images/{0}Extra.png' /></div>", rtype);
                                }
                            }
                            else if (litAction != null) litAction.Text = string.Format("<div class=\"card-link-action\"><img src='/Modules/Sails/Themes/images/{0}.png' /></div>", room.RoomType.Name.ToLower());
                        }
                    }
                    else
                    {
                        if (divBooking != null)
                        {
                            divBooking.Attributes.Add("style", "display:none");
                        }
                    }
                }

            }
        }
        protected virtual void btnSearch_OnClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("HomeRoomPlan.aspx{0}&cruiseId={1}&date={2}", GetBaseQueryString(), _currentCruise.Id, txtStartDate.Text));
        }

        protected void btnSaveRoom_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(hidChangeRoomIds.Value))
            {
                var ids = hidChangeRoomIds.Value.Split('$');
                var listChange = new Dictionary<int, int>();
                if (!string.IsNullOrWhiteSpace(hidChangeRoomOption.Value))
                {
                    var cids = hidChangeRoomOption.Value.Split('$');
                    foreach (string cid in cids)
                    {
                        if (!string.IsNullOrWhiteSpace(cid))
                        {
                            var bkc = cid.Split('|');
                            listChange.Add(Convert.ToInt32(bkc[0]), Convert.ToInt32(bkc[1]));
                        }
                    }
                }
                foreach (string crids in ids)
                {
                    var rid = crids.Split('|');
                    var bookingRoom = Module.BookingRoomGetById(Convert.ToInt32(rid[0]));
                    var newRoom = Module.RoomGetById(Convert.ToInt32(rid[1]));

                    if (bookingRoom.Room.Id != newRoom.Id)
                    {
                        var oldRoom = bookingRoom.Room;
                        bookingRoom.Room = newRoom;
                        bookingRoom.RoomClass = newRoom.RoomClass;

                        if (listChange.ContainsKey(bookingRoom.Id))
                        {
                            if (listChange[bookingRoom.Id] == 2) // 2 ghep giuong twin
                            {
                                bookingRoom.RoomType = oldRoom.RoomType;
                            }
                            else bookingRoom.RoomType = newRoom.RoomType;
                        }
                        else
                        {
                            bookingRoom.RoomType = newRoom.RoomType;
                        }
                        Module.Update(bookingRoom);

                        // change room export service
                        var exports = Module.GetAllExportService(bookingRoom);
                        if (exports.Count > 0)
                        {
                            // update new room status
                            newRoom.Status = oldRoom.Status;
                            Module.SaveOrUpdate(newRoom);
                            // update current room
                            oldRoom.Status = RoomType.NotCleaned;
                            Module.SaveOrUpdate(oldRoom);

                            foreach (IvExport export in exports)
                            {
                                export.Room = newRoom;
                                Module.SaveOrUpdate(export);
                            }
                        }
                        //save history
                        var bookingHistory = new BookingHistory()
                        {
                            Booking = bookingRoom.Book,
                            Date = DateTime.Now,
                            User = UserIdentity,
                            CabinNumber = bookingRoom.Book.BookingRooms.Count,
                            CustomerNumber = bookingRoom.Book.Pax,
                            StartDate = bookingRoom.Book.StartDate,
                            Status = bookingRoom.Book.Status,
                            Trip = bookingRoom.Book.Trip,
                            Agency = bookingRoom.Book.Agency,
                            Total = bookingRoom.Book.Total,
                            TotalCurrency = bookingRoom.Book.IsTotalUsd ? "USD" : "VND",
                            SpecialRequest = bookingRoom.Book.SpecialRequest,
                            OldRoom = oldRoom,
                            NewRoom = newRoom
                        };
                        Module.SaveOrUpdate(bookingHistory);
                    }
                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "RefreshParentPage", "RefreshParentPage();", true);
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "RefreshParentPage", "RefreshParentPage();", true);
        }
    }
}