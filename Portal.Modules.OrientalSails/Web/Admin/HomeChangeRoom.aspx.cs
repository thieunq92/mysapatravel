using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;
using Portal.Modules.OrientalSails.Web.Util;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    /// <summary>
    /// trang đổi phòng
    /// </summary>
    public partial class HomeChangeRoom : SailsAdminBasePage
    {
        private IList<Cruise> _cruises;
        private IList<Room> _roomCruises;
        private IList<Booking> _currentDayBookings;
        private IList<Booking> _nextDayBookings;
        private IList<Booking> _allBooking;

        public Dictionary<string, int> _currentRoomDic = new Dictionary<string, int>();
        public Dictionary<string, int> _nextRoomDic = new Dictionary<string, int>();

        private List<int> _floors = new List<int>();
        public Cruise _currentCruise = new Cruise();
        public Booking _booking = new Booking();
        public Room _room = new Room();
        public BookingRoom _bookingRoom = new BookingRoom();
        public int _numberOfDay = 2;
        public DateTime _currentDate = DateTime.Now;
        public DateTime _nextDate = DateTime.Now.AddDays(1);

        protected virtual void Page_Load(object sender, EventArgs e)
        {
            var tripId = Request["tripId"];
            if (!string.IsNullOrWhiteSpace(tripId))
            {
                _numberOfDay = Module.TripGetById(Convert.ToInt32(tripId)).NumberOfDay;
            }
            var roomId = Request["roomId"];
            if (!string.IsNullOrWhiteSpace(roomId))
            {
                _room = Module.RoomGetById(Convert.ToInt32(roomId));
            }
            var bookingRoomId = Request["bookingRoomId"];
            if (!string.IsNullOrWhiteSpace(bookingRoomId))
            {
                _bookingRoom = Module.BookingRoomGetById(Convert.ToInt32(bookingRoomId));
            }
            var bookingId = Request["bookingId"];
            if (!string.IsNullOrWhiteSpace(bookingId))
            {
                _booking = Module.BookingGetById(Convert.ToInt32(bookingId));
                _numberOfDay = _booking.Trip.NumberOfDay;
                _currentCruise = _booking.Cruise;
            }
            else
            {
                _currentCruise = _cruises.FirstOrDefault();
            }
            if (!IsPostBack)
            {
                _currentDate = _booking.StartDate;
                _nextDate = _currentDate.AddDays(1);

                if (_currentCruise != null && _currentCruise.Id > 0)
                {
                    _roomCruises = Module.RoomGetAll2(_currentCruise);
                    _currentDayBookings = Module.GetBookingByDate(_currentDate, _currentCruise);
                    _nextDayBookings = Module.GetBookingByDate(_nextDate, _currentCruise);
                    _allBooking = _currentDayBookings.Concat(_nextDayBookings).ToList();
                    ShowEmptyRoomDay(_currentDate, litCurrentRooms, true);
                    ShowEmptyRoomDay(_nextDate, litNextRooms, false);

                    for (int i = 1; i <= _currentCruise.NumberOfFloors; i++)
                    {
                        _floors.Add(i);
                    }
                    rptFloors.DataSource = _floors;
                    rptFloors.DataBind();
                }
            }
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
                if (isCurrentDay) _currentRoomDic = keypairs;
                else _nextRoomDic = keypairs;
                for (int i = 0; i < keypairs.Count; i++)
                {
                    if (i < keypairs.Count - 1)
                        str = str + string.Format("{0}/{2} {1},", keypairs.ElementAt(i).Value, keypairs.ElementAt(i).Key, keypairsAll[keypairs.ElementAt(i).Key]);
                    else
                        str = str + string.Format("{0}/{2} {1}", keypairs.ElementAt(i).Value, keypairs.ElementAt(i).Key, keypairsAll[keypairs.ElementAt(i).Key]);
                }
                //foreach (var roomCount in keypairs)
                //{
                //    str = str + string.Format("{0} {1},", roomCount.Value, roomCount.Key);
                //}
                litStatus.Text = str;
            }
            else str = string.Format("0/{0} cabins", _roomCruises.Count);
            litStatus.Text = string.Format("{0} pax / {1} cabins, {2}", pax, cabins, str);

        }
        protected void rptFloors_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var floor = e.Item.DataItem as int? ?? 0;
            if (floor > 0)
            {
                var rptRoomsDay = e.Item.FindControl("rptRoomsDay") as Repeater;
                var rooms = _roomCruises.Where(r => r.Floor == floor);
                if (rptRoomsDay != null)
                {
                    rptRoomsDay.DataSource = rooms;
                    rptRoomsDay.DataBind();
                }
                var rptRoomsNextDay = e.Item.FindControl("rptRoomsNextDay") as Repeater;
                if (rptRoomsNextDay != null)
                {
                    rptRoomsNextDay.DataSource = rooms;
                    rptRoomsNextDay.DataBind();
                }
            }
        }

        protected void rptRoomsDay_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var room = e.Item.DataItem as Room;
            if (room != null)
            {
                var hplBooking = e.Item.FindControl("hplBooking") as HyperLink;
                var chkSelectRoom = e.Item.FindControl("chkSelectRoom") as HtmlInputCheckBox;
                var lableSelectRoom = e.Item.FindControl("lableSelectRoom") as HtmlControl;
                if (lableSelectRoom != null)
                    if (chkSelectRoom != null) lableSelectRoom.Attributes.Add("for", chkSelectRoom.ClientID);
                var litCustomer = e.Item.FindControl("litCustomer") as Literal;
                var litCheckInfo = e.Item.FindControl("litCheckInfo") as Literal;
                var lblR = e.Item.FindControl("lblR") as Literal;
                var lblL = e.Item.FindControl("lblL") as Literal;
                var litAction = e.Item.FindControl("litAction") as Literal;
                var litRoomName = e.Item.FindControl("litRoomName") as Literal;
                var hidCurrentDay = e.Item.FindControl("hidCurrentDay") as HiddenField;
                if (litRoomName != null)
                {
                    litRoomName.Text = room.Name;
                }
                if (hplBooking != null)
                {
                    var booking = FindBooking(room, hidCurrentDay != null && hidCurrentDay.Value == "1" ? _currentDate : _nextDate);
                    if (booking != null)
                    {

                        hplBooking.Text = string.Format("{0:OS00000}", booking.Id);
                        hplBooking.NavigateUrl = "#";
                        if (booking.Status == StatusType.Approved) hplBooking.CssClass = "Approved";
                        if (booking.Status == StatusType.CutOff || booking.Status == StatusType.Pending) hplBooking.CssClass = "Pending";
                        if (_room != null && _room.Id == room.Id && booking.Id == _booking.Id) hplBooking.CssClass = "booking-room";

                        if (chkSelectRoom != null) chkSelectRoom.Visible = false;
                        if (lableSelectRoom != null) lableSelectRoom.Visible = false;
                        var listRoom = booking.BookingRooms.Where(c => c.Room != null && c.Room.Id == room.Id).ToList();

                        if (litCustomer != null)
                        {
                            var cus = "";
                            var listCus = new List<Customer>();
                            foreach (BookingRoom bookingRoom in listRoom)
                            {
                                listCus.AddRange(bookingRoom.Customers);
                            }
                            var checkInfo = true;
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
                                    if (string.IsNullOrWhiteSpace(listCus[i].Fullname)) checkInfo = false;
                                    if (listCus[i].IsMale == null) checkInfo = false;
                                    if (listCus[i].Birthday == null) checkInfo = false;
                                    if (listCus[i].Nationality == null) checkInfo = false;
                                    if (string.IsNullOrWhiteSpace(listCus[i].VisaNo)) checkInfo = false;
                                    if (string.IsNullOrWhiteSpace(listCus[i].Passport)) checkInfo = false;
                                    if (string.IsNullOrWhiteSpace(listCus[i].NguyenQuan)) checkInfo = false;
                                    if (listCus[i].VisaExpired == null) checkInfo = false;
                                }
                            }
                            litCustomer.Text = cus;
                            if (!checkInfo && litCheckInfo != null) litCheckInfo.Text = "<div class='checkCusInfo'></div>";
                            if (lblR != null) lblR.Text = string.Format("<div class=\"card-link-pax\">{0}</div>", listCus.Count);
                        }
                        if (lblL != null) lblL.Text = string.Format("<div class=\"card-link-left\">{0}</div>", booking.Agency.Name);
                        if (litRoomName != null)
                        {
                            string color = "#0b3bf5";
                            if (booking.Trip.NumberOfDay > 2) color = "yellow";
                            litRoomName.Text = string.Format("<a href='javascript:;' style='color:{1}'>{0}</a>", room.Name, color);
                        }
                        if (litAction != null)
                        {
                            var rtype = listRoom[0].RoomType.Name.ToLower();
                            if (rtype == "double" && room.RoomType.Name.ToLower() == "twin") rtype = "double_c";
                            litAction.Text = string.Format("<div class=\"card-link-action\"><img src='/Modules/Sails/Themes/images/{0}.png' /></div>", rtype);
                        }
                    }
                    else
                    {

                        hplBooking.CssClass = "";
                        if (chkSelectRoom != null)
                        {
                            //if (_currentDate <= DateTime.Now.AddDays(-1))
                            //{
                            //    chkSelectRoom.Visible = false;
                            //    if (lableSelectRoom != null) lableSelectRoom.Visible = false;
                            //    if (litAction != null) litAction.Visible = false;
                            //}
                        }
                        if (lblL != null) lblL.Text = string.Format("<div class=\"card-link-left roomClass\">{0}</div>", room.RoomClass.Name);
                        if (lblR != null) lblR.Text = string.Format("<div class=\"card-link-roomType\"><img src='/Modules/Sails/Themes/images/{0}.png'/></div>", room.RoomType.Name.ToLower());
                    }
                }
            }
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

        protected void btnAddBooking_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(hidRoomId.Value))
            {
                if (_room != null && _room.Id > 0)
                {
                    var newRoom = Module.RoomGetById(Convert.ToInt32(hidRoomId.Value));
                    var bookingRoom = _booking.BookingRooms.FirstOrDefault(r => r.Room.Id == _room.Id);
                    if (bookingRoom != null)
                    {
                        var oldRoom = bookingRoom.Room;
                        bookingRoom.Room = newRoom;
                        bookingRoom.RoomClass = newRoom.RoomClass;
                        bookingRoom.RoomType = newRoom.RoomType;
                        Module.Update(bookingRoom);

                        // change room export service
                        var exports = Module.GetAllExportService(_room, bookingRoom);
                        if (exports.Count > 0)
                        {
                            // update new room status
                            newRoom.Status = _room.Status;
                            Module.SaveOrUpdate(newRoom);
                            // update current room
                            _room.Status = RoomType.NotCleaned;
                            Module.SaveOrUpdate(_room);

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

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "RefreshParentPage", "RefreshParentPage();", true);
                }
                if (_bookingRoom != null && _bookingRoom.Id > 0)
                {
                    var newRoom = Module.RoomGetById(Convert.ToInt32(hidRoomId.Value));
                    var bkr = _booking.BookingRooms.FirstOrDefault(b => b.Id == _bookingRoom.Id);

                    if (bkr != null)
                    {
                        var oldRoom = bkr.Room;
                        bkr.Room = newRoom;
                        bkr.RoomClass = newRoom.RoomClass;
                        bkr.RoomType = newRoom.RoomType;
                        Module.Update(bkr);
                        //save history
                        var bookingHistory = new BookingHistory()
                        {
                            Booking = bkr.Book,
                            Date = DateTime.Now,
                            User = UserIdentity,
                            CabinNumber = bkr.Book.BookingRooms.Count,
                            CustomerNumber = bkr.Book.Pax,
                            StartDate = bkr.Book.StartDate,
                            Status = bkr.Book.Status,
                            Trip = bkr.Book.Trip,
                            Agency = bkr.Book.Agency,
                            Total = bkr.Book.Total,
                            TotalCurrency = bkr.Book.IsTotalUsd ? "USD" : "VND",
                            SpecialRequest = bkr.Book.SpecialRequest,
                            OldRoom = oldRoom,
                            NewRoom = newRoom
                        };
                        Module.SaveOrUpdate(bookingHistory);
                    }

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "RefreshParentPage", "RefreshParentPage();", true);
                }
            }
        }
    }
}