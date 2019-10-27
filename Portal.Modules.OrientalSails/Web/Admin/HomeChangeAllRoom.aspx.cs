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
    /// trang thay đổi toàn bộ phòng
    /// </summary>
    public partial class HomeChangeAllRoom : SailsAdminBasePage
    {
        private IList<Cruise> _cruises;
        private IList<Room> _roomCurrentCruises;
        private IList<Booking> _currentDayBookings;
        private IList<Booking> _nextDayBookings;
        private IList<Booking> _allBooking;

        public Dictionary<string, int> _currentRoomDic = new Dictionary<string, int>();
        public Dictionary<string, int> _nextRoomDic = new Dictionary<string, int>();

        private List<int> _floors = new List<int>();
        public Cruise _currentCruise = new Cruise();

        public Booking _booking = new Booking();
        public Cruise _nextCruise = new Cruise();
        public SailsTrip _trip = new SailsTrip();
        private IList<Room> _roomNextCruises;

        public int _numberOfDay = 2;
        public DateTime _currentDate = DateTime.Now;
        public DateTime _nextDate = DateTime.Now.AddDays(1);
        public int _numberOfRoom = 0;

        protected virtual void Page_Load(object sender, EventArgs e)
        {
            var tripId = Request["tripId"];
            if (!string.IsNullOrWhiteSpace(tripId))
            {
                _trip = Module.TripGetById(Convert.ToInt32(tripId));
                _numberOfDay = _trip.NumberOfDay;
            }
            var cruiseId = Request["cruiseId"];
            if (!string.IsNullOrWhiteSpace(cruiseId))
            {
                _nextCruise = Module.CruiseGetById(Convert.ToInt32(cruiseId));
                litNextCruise.Text = _nextCruise.Name;
            }
            var bookingId = Request["bookingId"];
            if (!string.IsNullOrWhiteSpace(bookingId))
            {
                _booking = Module.BookingGetById(Convert.ToInt32(bookingId));
                _currentCruise = _booking.Cruise;
                litCurrentCruise.Text = _currentCruise.Name;
                lblBookingCode.Text = _booking.BookingIdOS;
            }
            else
            {
                _currentCruise = _cruises.FirstOrDefault();
            }
            if (!IsPostBack)
            {
                LoadDropdowData();
                FillQuerForm();
                _currentDate = _booking.StartDate;
                if (!string.IsNullOrWhiteSpace(Request["startdate"]))
                    _nextDate = DateTime.ParseExact(Request["startdate"], "dd/MM/yyyy",
                        CultureInfo.InvariantCulture);
                else _nextDate = _currentDate;

                if (_currentCruise != null && _currentCruise.Id > 0)
                {
                    _roomCurrentCruises = Module.RoomGetAll2(_currentCruise);
                    var currentDayBookings = Module.GetBookingByDate(_currentDate, _currentCruise);
                    var nextDayBookings = Module.GetBookingByDate(_currentDate.AddDays(1), _currentCruise);
                    _currentDayBookings = currentDayBookings.Concat(nextDayBookings).ToList();
                    ShowEmptyRoomDay(_roomCurrentCruises, _currentDayBookings, _currentDate, litCurrentRooms, true);
                    for (int i = 1; i <= _currentCruise.NumberOfFloors; i++)
                    {
                        _floors.Add(i);
                    }
                    rptFloors.DataSource = _floors;
                    rptFloors.DataBind();
                }
                if (_nextCruise != null && _nextCruise.Id > 0)
                {
                    _roomNextCruises = Module.RoomGetAll2(_nextCruise);
                    var nextBooking = Module.GetBookingByDate(_nextDate, _nextCruise);
                    var bookingFeature = Module.GetBookingByDate(_nextDate.AddDays(1), _nextCruise);
                    _nextDayBookings = nextBooking.Concat(bookingFeature).ToList();
                    ShowEmptyRoomDay(_roomNextCruises, _nextDayBookings, _nextDate, litNextRooms, false);
                    _floors = new List<int>();
                    for (int i = 1; i <= _nextCruise.NumberOfFloors; i++)
                    {
                        _floors.Add(i);
                    }
                    rptNextFloors.DataSource = _floors;
                    rptNextFloors.DataBind();
                }
                if (!string.IsNullOrWhiteSpace(Request["change"]))
                {
                    if (Request["change"] == "trip") ddlCruises.Enabled = false;
                    if (Request["change"] == "boat") ddlTrips.Enabled = false;
                }
            }
        }

        private void FillQuerForm()
        {
            var tripId = Request["tripId"];
            if (!string.IsNullOrWhiteSpace(tripId))
            {
                ddlTrips.ClearSelection();
                ddlTrips.SelectedValue = tripId;
            }
            else
            {
                ddlTrips.SelectedValue = _booking.Trip.Id.ToString();

            }
            var cruiseId = Request["cruiseId"];
            if (!string.IsNullOrWhiteSpace(cruiseId))
            {
                ddlCruises.SelectedValue = cruiseId;
                if (!_nextCruise.Trips.Contains(_booking.Trip))
                {
                    foreach (SailsTrip trip in _nextCruise.Trips)
                    {
                        if (trip.NumberOfDay == _booking.Trip.NumberOfDay)
                        {
                            ddlTrips.SelectedValue = trip.Id.ToString();
                            break;
                        }
                    }
                }
            }
            var startDate = Request["startdate"];
            if (!string.IsNullOrWhiteSpace(startDate))
            {
                txtStartDate.Text = startDate;
            }
        }

        private void LoadDropdowData()
        {
            //var trips = Module.TripGetAll(true);
            //foreach (SailsTrip trip in trips)
            //{
            foreach (SailsTrip trip in _nextCruise.Trips)
            {
                ddlTrips.Items.Add(new ListItem(trip.Name, trip.Id.ToString()));
            }
            //}
            ddlCruises.DataSource = Module.CruiseGetAll();
            ddlCruises.DataTextField = "Name";
            ddlCruises.DataValueField = "Id";
            ddlCruises.DataBind();
        }

        private void ShowEmptyRoomDay(IList<Room> roomCruises, IList<Booking> bookings, DateTime dateTime, Literal litStatus, bool isCurrentDay)
        {
            var list = new List<Room>(roomCruises);
            var pax = 0;
            var cabins = 0;
            var bks = new List<int>();

            foreach (Room room in roomCruises)
            {
                var booking = FindBooking(room, bookings, dateTime);
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
            foreach (Room room in roomCruises)
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
                        str = str + string.Format("{0}/{2} {1},", keypairs.ElementAt(i).Value, keypairs.ElementAt(i).Key, keypairsAll[keypairs.ElementAt(i).Key]);



                }
                //foreach (var roomCount in keypairs)
                //{
                //    str = str + string.Format("{0} {1},", roomCount.Value, roomCount.Key);
                //}
            }
            else str = string.Format("0/{0} cabins", roomCruises.Count);
            litStatus.Text = string.Format("{0} pax / {1} cabins, {2}", pax, cabins, str);

        }
        protected void rptFloors_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var floor = e.Item.DataItem as int? ?? 0;
            if (floor > 0)
            {
                var rptRoomsDay = e.Item.FindControl("rptRoomsDay") as Repeater;
                var rooms = _roomCurrentCruises.Where(r => r.Floor == floor);
                if (rptRoomsDay != null)
                {
                    rptRoomsDay.DataSource = rooms;
                    rptRoomsDay.DataBind();
                }
            }
        }
        protected void rptNextFloors_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            var floor = e.Item.DataItem as int? ?? 0;
            if (floor > 0)
            {
                var rooms = _roomNextCruises.Where(r => r.Floor == floor);
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
                if (litRoomName != null)
                {
                    litRoomName.Text = room.Name;
                }
                if (hplBooking != null)
                {
                    var booking = FindBooking(room, _currentDayBookings, _currentDate);
                    if (booking != null)
                    {
                        hplBooking.Text = string.Format("{0:OS00000}", booking.Id);
                        hplBooking.NavigateUrl = "javascript:;";
                        if (booking.Status == StatusType.Approved) hplBooking.CssClass = "Approved";
                        if (booking.Status == StatusType.CutOff || booking.Status == StatusType.Pending) hplBooking.CssClass = "Pending";

                        if (booking.Id == _booking.Id)
                        {

                            if (_booking.Cruise.Id != _nextCruise.Id)
                            {
                                _numberOfRoom++;
                                var divRoom = e.Item.FindControl("divRoom") as HtmlControl;
                                if (divRoom != null)
                                {
                                    divRoom.Attributes.Add("class", "card dragbox");
                                    divRoom.Attributes.Add("rName", room.Name);
                                    divRoom.Attributes.Add("rId", room.Id.ToString());

                                }
                                if (booking.Id == _booking.Id && _roomCurrentCruises.Contains(room))
                                {
                                    hplBooking.CssClass = "booking-room";
                                }
                            }
                            else
                            {
                                if (_trip.NumberOfDay > 2)
                                {
                                    var bookingN = FindBooking(room, _currentDayBookings, _currentDate.AddDays(1));
                                    if (bookingN != null && bookingN.Id != _booking.Id)
                                    {
                                        _numberOfRoom++;
                                        var divRoom = e.Item.FindControl("divRoom") as HtmlControl;
                                        if (divRoom != null)
                                        {
                                            divRoom.Attributes.Add("class", "card dragbox");
                                            divRoom.Attributes.Add("rName", room.Name);
                                            divRoom.Attributes.Add("rId", room.Id.ToString());

                                        }
                                        if (booking.Id == _booking.Id && _roomCurrentCruises.Contains(room))
                                        {
                                            hplBooking.CssClass = "booking-room";
                                        }
                                    }
                                }
                                if (Request["startDate"] != booking.StartDate.ToString("dd/MM/yyyy"))
                                {
                                    _numberOfRoom++;
                                    var divRoom = e.Item.FindControl("divRoom") as HtmlControl;
                                    if (divRoom != null)
                                    {
                                        divRoom.Attributes.Add("class", "card dragbox");
                                        divRoom.Attributes.Add("rName", room.Name);
                                        divRoom.Attributes.Add("rId", room.Id.ToString());

                                    }
                                    if (booking.Id == _booking.Id && _roomCurrentCruises.Contains(room))
                                    {
                                        hplBooking.CssClass = "booking-room";
                                    }
                                }
                            }
                        }



                        if (chkSelectRoom != null) chkSelectRoom.Visible = false;
                        if (lableSelectRoom != null) lableSelectRoom.Visible = false;
                        if (litCustomer != null)
                        {
                            var cus = "";
                            var listRoom = booking.BookingRooms.Where(c => c.Room != null && c.Room.Id == room.Id).ToList();
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
                        if (litAction != null) litAction.Text = string.Format("<div class=\"card-link-action\"><img src='/Modules/Sails/Themes/images/{0}.png' /></div>", room.RoomType.Name.ToLower());
                    }
                    else
                    {

                        hplBooking.CssClass = "";
                        if (chkSelectRoom != null)
                        {
                            if (_currentDate <= DateTime.Now.AddDays(-1))
                            {
                                chkSelectRoom.Visible = false;
                                if (lableSelectRoom != null) lableSelectRoom.Visible = false;
                                if (litAction != null) litAction.Visible = false;
                            }
                        }
                        if (lblL != null) lblL.Text = string.Format("<div class=\"card-link-left roomClass\">{0}</div>", room.RoomClass.Name);
                        if (lblR != null) lblR.Text = string.Format("<div class=\"card-link-roomType\"><img src='/Modules/Sails/Themes/images/{0}.png'/></div>", room.RoomType.Name.ToLower());
                    }
                }
            }
        }
        protected void rptRoomsNextDay_OnItemDataBound(object sender, RepeaterItemEventArgs e)
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
                if (litRoomName != null)
                {
                    litRoomName.Text = room.Name;
                }
                if (hplBooking != null)
                {
                    var booking = FindBooking(room, _nextDayBookings, _nextDate);
                    if (booking != null)
                    {

                        hplBooking.Text = string.Format("{0:OS00000}", booking.Id);
                        hplBooking.NavigateUrl = "javascript:;";
                        if (booking.Status == StatusType.Approved) hplBooking.CssClass = "Approved";
                        if (booking.Status == StatusType.CutOff || booking.Status == StatusType.Pending) hplBooking.CssClass = "Pending";


                        if (chkSelectRoom != null) chkSelectRoom.Visible = false;
                        if (lableSelectRoom != null) lableSelectRoom.Visible = false;
                        if (litCustomer != null)
                        {
                            var cus = "";
                            var listRoom = booking.BookingRooms.Where(c => c.Room != null && c.Room.Id == room.Id).ToList();
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
                        if (litAction != null) litAction.Text = string.Format("<div class=\"card-link-action\"><img src='/Modules/Sails/Themes/images/{0}.png' /></div>", room.RoomType.Name.ToLower());
                    }
                    else
                    {

                        hplBooking.CssClass = "";
                        var divRoom = e.Item.FindControl("divRoom") as HtmlControl;
                        if (divRoom != null)
                        {
                            divRoom.Attributes.Add("rName", room.Name);
                            divRoom.Attributes.Add("rId", room.Id.ToString());
                            if (_trip.NumberOfDay > 2)
                            {
                                var nextBooking = FindBooking(room, _nextDayBookings, _nextDate.AddDays(1));
                                if (nextBooking == null)
                                {
                                    divRoom.Attributes.Add("class", "card dropbox");
                                }
                                else
                                {
                                    if (litCheckInfo != null) litCheckInfo.Text = "<div class='checkCusInfo'></div>";
                                }
                            }
                            else divRoom.Attributes.Add("class", "card dropbox");
                        }
                        if (chkSelectRoom != null)
                        {
                            if (_currentDate <= DateTime.Now.AddDays(-1))
                            {
                                chkSelectRoom.Visible = false;
                                if (lableSelectRoom != null) lableSelectRoom.Visible = false;
                                if (litAction != null) litAction.Visible = false;
                            }
                        }
                        if (lblL != null) lblL.Text = string.Format("<div class=\"card-link-left roomClass\">{0}</div>", room.RoomClass.Name);
                        if (lblR != null) lblR.Text = string.Format("<div class=\"card-link-roomType\"><img src='/Modules/Sails/Themes/images/{0}.png'/></div>", room.RoomType.Name.ToLower());
                    }
                }
            }
        }

        private Booking FindBooking(Room room, IList<Booking> bookings, DateTime date)
        {
            Booking booking = null;
            if (bookings != null)
            {
                foreach (Booking vBookingRoom in bookings)
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
            if (!string.IsNullOrWhiteSpace(Request["startdate"]))
            {
                var startDate = DateTime.ParseExact(Request["startdate"], "dd/MM/yyyy",
                    CultureInfo.InvariantCulture);
                _booking.StartDate = startDate;
            }
            
            _booking.Trip = Module.TripGetById(Convert.ToInt32(ddlTrips.SelectedValue));
            _booking.EndDate = _booking.StartDate.AddDays(_booking.Trip.NumberOfDay - 1);
            _booking.Transfer_DateTo = _booking.StartDate;
            _booking.Transfer_DateBack = _booking.EndDate;
            Module.SaveOrUpdate(_booking);
            if (!string.IsNullOrWhiteSpace(hidChangeRoomIds.Value))
            {
                _booking.Cruise = Module.CruiseGetById(Convert.ToInt32(ddlCruises.SelectedValue));
                Module.SaveOrUpdate(_booking);
                var ids = hidChangeRoomIds.Value.Split('$');
                foreach (string crids in ids)
                {
                    var rid = crids.Split('|');
                    var oldRoom = Module.RoomGetById(Convert.ToInt32(rid[0]));
                    var newRoom = Module.RoomGetById(Convert.ToInt32(rid[1]));
                    foreach (var bookingRoom in _booking.BookingRooms)
                    {
                        if (bookingRoom.Room.Id == oldRoom.Id)
                        {
                            bookingRoom.Room = newRoom;
                            bookingRoom.RoomClass = newRoom.RoomClass;
                            bookingRoom.RoomType = newRoom.RoomType;
                            Module.Update(bookingRoom);

                            // change room export service
                            var exports = Module.GetAllExportService(oldRoom, bookingRoom);
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
                }

                Page.ClientScript.RegisterStartupScript(this.GetType(), "RefreshParentPage", "RefreshParentPage();", true);
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "RefreshParentPage", "RefreshParentPage();", true);

        }

        protected void btnSearch_OnClick(object sender, EventArgs e)
        {
            var url = string.Format("HomeChangeAllRoom.aspx{0}&bookingId={1}&change={2}", base.GetBaseQueryString(), Request["bookingId"], Request["change"]);
            if (!string.IsNullOrWhiteSpace(ddlTrips.SelectedValue))
            {
                url += "&tripId=" + ddlTrips.SelectedValue;
            }
            if (!string.IsNullOrWhiteSpace(ddlCruises.SelectedValue))
            {
                url += "&cruiseId=" + ddlCruises.SelectedValue;
            }
            if (!string.IsNullOrWhiteSpace(txtStartDate.Text))
            {
                url += "&startdate=" + txtStartDate.Text;
            }
            Response.Redirect(url);
        }
    }
}