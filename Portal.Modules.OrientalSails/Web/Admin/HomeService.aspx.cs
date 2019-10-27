using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Org.BouncyCastle.Ocsp;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;
using Portal.Modules.OrientalSails.Web.Util;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    /// <summary>
    /// trang tình trạng dịch vụ phòng
    /// </summary>
    public partial class HomeService : SailsAdminBasePage
    {
        private IList<Cruise> _cruises;
        private IList<Room> _roomCruises;
        private IList<Booking> _currentDayBookings;
        private IList<Booking> _allBooking;

        public Dictionary<string, int> _currentRoomDic;

        private List<int> _floors = new List<int>();
        public Cruise _currentCruise = new Cruise();
        public DateTime _currentDate = DateTime.Now;
        public int _existRooms = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            _cruises = Module.CruiseGetByUser(UserIdentity);

            var cruiseId = Request["cruiseId"];
            if (!string.IsNullOrWhiteSpace(cruiseId))
            {
                _currentCruise = _cruises.FirstOrDefault(c => c.Id == Convert.ToInt32(cruiseId));
            }
            else
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
                _currentDayBookings = Module.GetBookingServiceByDate(_currentDate, _currentCruise);
                _allBooking = _currentDayBookings;
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
                    hplCruise.NavigateUrl = string.Format("HomeService.aspx{0}&cruiseId={1}&date={2}", GetBaseQueryString(),
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
                var lableSelectRoom = e.Item.FindControl("lableSelectRoom") as HtmlControl;
                var litCustomer = e.Item.FindControl("litCustomer") as Literal;
                var litCheckInfo = e.Item.FindControl("litCheckInfo") as Literal;
                var lblR = e.Item.FindControl("lblR") as Literal;
                var lblL = e.Item.FindControl("lblL") as Literal;
                var btnStatus = e.Item.FindControl("btnStatus") as ImageButton;
                var litRoomName = e.Item.FindControl("litRoomName") as Literal;
                var hidCurrentDay = e.Item.FindControl("hidCurrentDay") as HiddenField;

                if (litRoomName != null)
                {
                    litRoomName.Text = room.Name;
                }
                if (hplBooking != null && _currentDate > DateTime.Now.AddDays(-1) && _currentDate < DateTime.Now.AddDays(1))
                {
                    hplBooking.CssClass = room.Status.ToString();
                    if (btnStatus != null)
                    {
                        btnStatus.ImageUrl = string.Format("/Modules/Sails/Themes/images/{0}.png", room.Status);
                        btnStatus.AlternateText = room.Status.ToString();
                        switch (room.Status)
                        {
                            case RoomType.Cleaned:
                                btnStatus.ToolTip = "Check in";
                                break;
                            case RoomType.InUsed:
                                btnStatus.ToolTip = "Check out";
                                break;
                            case RoomType.NotCleaned:
                                btnStatus.ToolTip = "Cleaned";
                                break;
                            default:
                                btnStatus.ToolTip = "Check in";
                                break;
                        }
                    }

                    Booking booking = null;
                    var ivExport = Module.GetNewestExportService(room, _currentDate);
                    if (ivExport != null && ivExport.BookingRoom != null && ivExport.BookingRoom.Book != null)
                    {
                        booking = ivExport.BookingRoom.Book;
                    }
                    if (booking != null)
                    {
                        if (hidCurrentDay != null && hidCurrentDay.Value == "1")
                        {
                            _existRooms += 1;
                        }
                        hplBooking.Text = string.Format("{0:OS00000}", booking.Id);
                        //hplBooking.NavigateUrl =
                        //    string.Format("BookingView.aspx{0}&bi={1}", GetBaseQueryString(), booking.Id);

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
                        var ivExportId = 0;
                        if (ivExport.Status == IvExportType.Created || ivExport.Status == IvExportType.Pay)
                        {
                            ivExportId = ivExport.Id;
                        }
                        if (lblL != null) lblL.Text = string.Format("<div class=\"card-link-left link-service\" onclick='viewService({0},{1})'>Services</div>", ivExport.BookingRoom.Id, ivExportId);
                        if (ivExport.Status == IvExportType.Paid || ivExport.Status == IvExportType.Pay)
                        {
                            var txtPay = ivExport.Status == IvExportType.Paid ? "PAID" : "PAY NOW";
                            if (lblR != null) lblR.Text = string.Format("<div class=\"card-link-roomType export-{0}\">{1}</div>", ivExport.Status.ToString(), txtPay);
                        }
                        var editRoom = string.Format("editRoom({0},{1},{2})", booking.Id, room.Id, booking.Trip.Id);
                        if (litRoomName != null)
                        {
                            string color = "#0b3bf5";
                            if (booking.Trip.NumberOfDay > 2) color = "yellow";
                            litRoomName.Text = string.Format("<a href='javascript:;' style='color:{2}' onclick='{0}' >{1}</a>", editRoom, room.Name, color);
                        }

                    }
                    else
                    {
                        if (lblL != null) lblL.Text = string.Format("<div class=\"card-link-left roomClass\">{0}</div>", room.RoomClass.Name);
                        if (lblR != null) lblR.Text = string.Format("<div class=\"card-link-roomType\"><img src='/Modules/Sails/Themes/images/{0}.png'/></div>", room.RoomType.Name.ToLower());
                    }
                }
                else
                {
                    if (btnStatus != null) btnStatus.Visible = false;
                }
            }
        }
        protected void btnAll_OnClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("HomeService.aspx{0}&cruiseId={1}&date={2}", GetBaseQueryString(), _currentCruise.Id, txtStartDate.Text));
        }
        protected virtual void btnSearch_OnClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("HomeService.aspx{0}&cruiseId={1}&date={2}", GetBaseQueryString(), _currentCruise.Id, txtStartDate.Text));
        }

        protected void btnCleaned_OnClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("HomeService.aspx{0}&cruiseId={1}&date={2}&status={3}", GetBaseQueryString(), _currentCruise.Id, txtStartDate.Text, RoomType.Cleaned));

        }

        protected void btnInUsed_OnClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("HomeService.aspx{0}&cruiseId={1}&date={2}&status={3}", GetBaseQueryString(), _currentCruise.Id, txtStartDate.Text, RoomType.InUsed));
        }

        protected void btnNotCleaned_OnClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("HomeService.aspx{0}&cruiseId={1}&date={2}&status={3}", GetBaseQueryString(), _currentCruise.Id, txtStartDate.Text, RoomType.NotCleaned));
        }

        protected void rptRoomsDay_OnItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "updateStatus")
            {
                var id = Convert.ToInt32(e.CommandArgument);
                var room = Module.RoomGetById(id);
                if (room != null)
                {
                    var booking = FindBookingApproved(room, _currentDate);
                    if (room.Status != RoomType.Cleaned)
                    {

                        if (room.Status == RoomType.InUsed)
                            room.Status = RoomType.NotCleaned;
                        else if (room.Status == RoomType.NotCleaned) room.Status = RoomType.Cleaned;
                        else room.Status = RoomType.InUsed;
                        Module.SaveOrUpdate(room);
                    }
                    else if (room.Status == RoomType.Cleaned && booking != null)
                    {
                        if (room.Status == RoomType.Cleaned) room.Status = RoomType.InUsed;
                        Module.SaveOrUpdate(room);
                        if (room.Status == RoomType.InUsed)
                        {
                            IvExport ivExport = new IvExport();
                            ivExport.ExportDate = DateTime.Now;
                            ivExport.Room = room;
                            ivExport.Status = IvExportType.Created;
                            ivExport.BookingRoom = booking.BookingRooms.FirstOrDefault(r => r.Room.Id == room.Id);
                            Module.SaveOrUpdate(ivExport, UserIdentity);
                        }
                    }
                    else
                    {
                        ShowErrors("Không có booking nào cho phòng này ngày hôm nay");
                    }
                    GetDataRoomInFloor();

                }
            }
        }
        private Booking FindBookingApproved(Room room, DateTime date)
        {
            Booking booking = null;
            if (_allBooking != null)
            {
                foreach (Booking vBookingRoom in _allBooking)
                {
                    var roomIsExist = vBookingRoom.BookingRooms.Any(br => br.Room != null && br.Room.Id == room.Id);

                    if (roomIsExist && vBookingRoom.Status == StatusType.Approved && date == vBookingRoom.StartDate)
                    {
                        booking = vBookingRoom;
                        break;
                    }

                }
            }
            return booking;
        }


    }
}