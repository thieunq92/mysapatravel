using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Castle.Core.Internal;
using GemBox.Spreadsheet;
using Portal.Modules.OrientalSails.BusinessLogic.Share;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Enums;
using Portal.Modules.OrientalSails.Web.UI;
using Portal.Modules.OrientalSails.Web.Util;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    /// <summary>
    /// trang hiển thị tình trạng phòng theo ngày
    /// </summary>
    public partial class Home : SailsAdminBasePage
    {
        private IList<Cruise> _cruises;
        private static IList<Room> _roomCruises;
        private static Dictionary<Room, Booking> _bookingDay = new Dictionary<Room, Booking>();
        private IList<Booking> _currentDayBookings;
        private IList<Booking> _nextDayBookings;
        private IList<Booking> _allBooking;

        public Dictionary<string, int> _currentRoomDic;
        public Dictionary<string, int> _nextRoomDic;

        private List<int> _floors = new List<int>();
        public Cruise _currentCruise = new Cruise();
        public static string _currentRoomStatus = "";
        public static DateTime _currentDate = DateTime.Now;
        public DateTime _nextDate = DateTime.Now.AddDays(1);
        public int _existRooms = 0;
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
            if (!PermissionUtil.UserCheckPermission(UserIdentity.Id, (int)PermissionEnum.VIEW_HOME_BOOKING))
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
            else
            {
                _currentCruise = _cruises.FirstOrDefault();
            }
            if (!IsPostBack)
            {
                _bookingDay = new Dictionary<Room, Booking>();
                _roomCruises = new List<Room>();
                _currentRoomStatus = "";
                var bkc = Request["bkc"];
                if (!string.IsNullOrWhiteSpace(bkc))
                {
                    txtBookingCode.Text = bkc;
                }
                var dateStr = Request["date"];
                if (!string.IsNullOrWhiteSpace(dateStr))
                {
                    txtStartDate.Text = dateStr;
                }
                else txtStartDate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                _currentDate = DateTime.ParseExact(txtStartDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                _nextDate = _currentDate.AddDays(1);

                if (_currentCruise != null && _currentCruise.Id > 0)
                {
                    _roomCruises = Module.RoomGetAll2(_currentCruise);
                    _currentDayBookings = Module.GetBookingByDate(_currentDate, _currentCruise);
                    _nextDayBookings = Module.GetBookingByDate(_nextDate, _currentCruise);
                    ShowHideCharter();
                    //                    _currentBookingsRooms = Module.GetBookingRoomByStartDate(_currentDate, _currentCruise.Id);
                    //_nextBookingsRooms = Module.GetBookingRoomByStartDate(_currentDate, _currentCruise.Id);
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
                rptCruises.DataSource = _cruises;
                rptCruises.DataBind();
            }
        }
        /// <summary>
        /// ẩn hiện nút thêm booking charter
        /// </summary>
        private void ShowHideCharter()
        {
            var listCurrent = new List<Room>(_roomCruises);
            foreach (Room room in _roomCruises)
            {
                var booking = FindBooking(room, _currentDate);
                if (booking != null && booking.Id > 0)
                    listCurrent.Remove(room);
            }
            if (listCurrent.Count != _roomCruises.Count)
            {
                btnCharter2day.Visible = false;
            }
            var listNext = new List<Room>(_roomCruises);
            foreach (Room room in _roomCruises)
            {
                var booking = FindBooking(room, _currentDate);
                if (booking != null && booking.Id > 0)
                    listNext.Remove(room);
            }
            if (!(listNext.Count == listCurrent.Count && listNext.Count == _roomCruises.Count)) btnCharter3day.Visible = false;
            if (!(_currentDate > DateTime.Now.AddDays(-1))) btnCharter2day.Visible = false;
            if (!(_currentDate > DateTime.Now.AddDays(-1) && _nextDate > DateTime.Now)) btnCharter3day.Visible = false;
        }
        /// <summary>
        /// hiển thị thông tin tình trạng phòng trống
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="litStatus"></param>
        /// <param name="isCurrentDay"></param>
        private void ShowEmptyRoomDay(DateTime dateTime, Literal litStatus, bool isCurrentDay)
        {
            var list = new List<Room>(_roomCruises);
            var listUsing = new List<Room>();
            var pax = 0;
            var cabins = 0;
            var bks = new List<int>();
            foreach (Room room in _roomCruises)
            {
                var booking = FindBooking(room, dateTime);
                if (booking != null && booking.Id > 0)
                {
                    list.Remove(room);
                    if (!listUsing.Contains(room)) listUsing.Add(room);
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
                        keypairs[room.RoomClass.Name + " " + room.RoomType.Name] =
                            keypairs[room.RoomClass.Name + " " + room.RoomType.Name] + 1;
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
                        str = str + string.Format("{0}/{2} {1},", keypairs.ElementAt(i).Value,
                                  keypairs.ElementAt(i).Key, keypairsAll[keypairs.ElementAt(i).Key]);
                    else
                    {
                        str = str + string.Format("{0}/{2} {1}", keypairs.ElementAt(i).Value, keypairs.ElementAt(i).Key,
                                  keypairsAll[keypairs.ElementAt(i).Key]);

                    }
                }
                //foreach (var roomCount in keypairs)
                //{
                //    str = str + string.Format("{0} {1},", roomCount.Value, roomCount.Key);
                //}
            }
            else str = string.Format("0/{0} cabins", _roomCruises.Count);
            var status = string.Format("{0} pax / {1} cabins, {2}", pax, cabins, str);
            litStatus.Text = status;

            var strusing = "";
            if (listUsing.Count > 0)
            {
                var keypairs = new Dictionary<string, int>();
                foreach (Room room in listUsing)
                {
                    if (keypairs.ContainsKey(room.RoomClass.Name + " " + room.RoomType.Name))
                    {
                        keypairs[room.RoomClass.Name + " " + room.RoomType.Name] =
                            keypairs[room.RoomClass.Name + " " + room.RoomType.Name] + 1;
                    }
                    else
                    {
                        keypairs.Add(room.RoomClass.Name + " " + room.RoomType.Name, 1);
                    }
                }
                for (int i = 0; i < keypairs.Count; i++)
                {
                    if (i < keypairs.Count - 1)
                        strusing = strusing + string.Format("{0} {1},", keypairs.ElementAt(i).Value,
                                  keypairs.ElementAt(i).Key);
                    else
                    {
                        strusing = strusing + string.Format("{0} {1}", keypairs.ElementAt(i).Value, keypairs.ElementAt(i).Key);

                    }
                }
            }
            if (isCurrentDay) _currentRoomStatus = strusing;

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
                    hplCruise.NavigateUrl = string.Format("Home.aspx{0}&cruiseId={1}&date={2}", GetBaseQueryString(),
                        cruise.Id, dateStr);
                    if (cruise.Id == _currentCruise.Id) hplCruise.CssClass = "btn cruiseActive";
                }
            }
        }
        /// <summary>
        /// check room đã sử dụng
        /// </summary>
        /// <param name="room"></param>
        /// <param name="bookings"></param>
        /// <returns></returns>
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
        /// <summary>
        /// sự kiện các tầng của tàu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        if (hidCurrentDay != null && hidCurrentDay.Value == "1")
                        {
                            _existRooms += 1;
                            _bookingDay.Add(room, booking);
                        }
                        var bkcode = string.Format("{0:OS00000}", booking.Id);
                        hplBooking.Text = bkcode;
                        hplBooking.NavigateUrl =
                            string.Format("BookingView.aspx{0}&bi={1}", GetBaseQueryString(), booking.Id);
                        if (booking.Status == StatusType.Approved) hplBooking.CssClass = "Approved";
                        if (booking.Status == StatusType.CutOff || booking.Status == StatusType.Pending) hplBooking.CssClass = "Pending";
                        var bkc = Request["bkc"];
                        if (!string.IsNullOrWhiteSpace(bkc) && bkcode.Contains(bkc))
                        {
                            hplBooking.CssClass = "booking-room";
                        }
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
                            if (!checkInfo && litCheckInfo != null) litCheckInfo.Text = "<div class='checkCusInfo '></div>";
                            if (lblR != null) lblR.Text = string.Format("<div class=\"card-link-pax room__number-of-pax\">{0}</div>", listCus.Count);
                        }
                        if (lblL != null) lblL.Text = string.Format("<div class=\"card-link-left room__partner\">{0}</div>", booking.Agency.Name);
                        var editRoom = string.Format("editRoom({0},{1},{2})", booking.Id, room.Id, booking.Trip.Id);
                        if (litRoomName != null)
                        {
                            string color = "#0b3bf5";
                            if (booking.Trip.NumberOfDay > 2) color = "yellow";
                            litRoomName.Text = string.Format("<a href='javascript:;' style='color:{2}' onclick='{0}' >{1}</a>", editRoom, room.Name, color);
                        }
                        if (listRoom.Count > 0 && litAction != null)
                        {
                            var rtype = listRoom[0].RoomType.Name.ToLower();
                            if (rtype == "double" && room.RoomType.Name.ToLower() == "twin") rtype = "double_c";
                            litAction.Text = string.Format("<div class=\"card-link-action\"><img src='/Modules/Sails/Themes/images/{0}.png' /></div>", rtype);
                            if (listRoom[0].HasAddExtraBed)
                            {
                                litAction.Text = string.Format("<div class=\"card-link-action\"><img src='/Modules/Sails/Themes/images/{0}Extra.png' /></div>", rtype);
                            }
                        }
                        else if (litAction != null) litAction.Text = string.Format("<div class=\"card-link-action\"><img src='/Modules/Sails/Themes/images/{0}.png' /></div>", room.RoomType.Name.ToLower());

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
                        var addRoom = string.Format("addRoom({0},'{1}')", room.Id, hidCurrentDay != null ? hidCurrentDay.Value : "0");
                        if (litAction != null) litAction.Text = string.Format("<div class=\"card-link-action\"><a href='javascript:;' onclick=\"{0}\" ><img src='/Modules/Sails/Themes/images/addRoom.png' /></a></div>", addRoom);
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
        /// <summary>
        /// tìm kiếm các phòng theo ngày
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnSearch_OnClick(object sender, EventArgs e)
        {
            var url = string.Format("Home.aspx{0}&cruiseId={1}&date={2}", GetBaseQueryString(), _currentCruise.Id,
                txtStartDate.Text);
            if (!string.IsNullOrWhiteSpace(txtBookingCode.Text))
            {
                var bkId = Regex.Match(txtBookingCode.Text.Trim(), @"\d+").Value;
                try
                {
                    var booking = Module.GetById<Booking>(Convert.ToInt32(bkId));
                    if (booking != null && booking.Id > 0)
                    {
                        url = string.Format("Home.aspx{0}&cruiseId={1}&date={2}&bkc={3}", GetBaseQueryString(), booking.Cruise.Id,
                            booking.StartDate.ToString("dd/MM/yyyy"), txtBookingCode.Text);

                    }
                }
                catch (Exception exception)
                {

                }
            }
            Response.Redirect(url);
        }
        /// <summary>
        /// xuất file room plan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void btnExportRoomPlan_OnClick(object sender, EventArgs e)
        {
            ExcelFile excelFile = ExcelFile.Load(Server.MapPath("/Modules/Sails/Admin/ExportTemplates/RoomPlan.xlsx"));
            ExcelWorksheet sheet = excelFile.Worksheets[0];


            var index = 1;
            sheet.Cells["D2"].Value = _currentDate.ToString("dd/MM/yyyy");
            sheet.Cells["E2"].Value = _currentCruise.Name;
            sheet.Cells["C4"].Value = _roomCruises.Count;
            sheet.Cells["D4"].Value = _currentRoomStatus;

            const int firstrow = 6;
            int crow = firstrow;
            int stt = 1;
            var bookingId = 0;
            var bookingS = new Booking();
            var mergeR = 7;
            var list = new List<Booking>();
            foreach (KeyValuePair<Room, Booking> keyValuePair in _bookingDay)
            {
                if (!list.Contains(keyValuePair.Value)) list.Add(keyValuePair.Value);
            }
            list = list.OrderBy(b => b.Id).ToList();
            foreach (Booking booking in list)
            {
                foreach (BookingRoom bookingRoom in booking.BookingRooms)
                {
                    sheet.Cells[crow, 0].Value = stt;
                    sheet.Cells[crow, 1].Value = bookingRoom.Room.Name;

                    sheet.Cells[crow, 2].Value = bookingRoom.RoomType.Name;
                    if (bookingRoom.IsSingle) sheet.Cells[crow, 2].Value = "Single";


                    var cus = "";
                    if (bookingRoom.Customers.Count > 0)
                    {
                        for (int i = 0; i < bookingRoom.Customers.Count(); i++)
                        {
                            if (!string.IsNullOrWhiteSpace(bookingRoom.Customers[i].Fullname))
                            {
                                if (i < bookingRoom.Customers.Count - 1)
                                {
                                    cus += bookingRoom.Customers[i].Fullname + Environment.NewLine;
                                }
                                else
                                {
                                    cus += bookingRoom.Customers[i].Fullname;
                                }
                            }
                        }
                    }
                    sheet.Cells[crow, 3].Value = cus;


                    if (bookingId == 0)
                    {
                        bookingId = booking.Id;
                        bookingS = booking;
                    }
                    if (bookingId > 0 && bookingId != booking.Id)
                    {
                        if (bookingS.BookingRooms.Count > 1)
                        {
                            var mergedRangeE = sheet.Cells.GetSubrange(string.Format("E{0}:E{1}", mergeR, crow));
                            mergedRangeE.Merged = true;
                            mergedRangeE.Value = bookingS.SpecialRequest;

                            var mergedRangeF = sheet.Cells.GetSubrange(string.Format("F{0}:F{1}", mergeR, crow));
                            mergedRangeF.Merged = true;
                            mergedRangeF.Value = bookingS.PickupAddress;

                            var mergedRange = sheet.Cells.GetSubrange(string.Format("G{0}:G{1}", mergeR, crow));
                            mergedRange.Merged = true;
                            mergedRange.Value = string.Format("OS{0:00000}", bookingId);
                        }
                        else
                        {
                            var specialRequest = bookingS.SpecialRequest;
                            if (bookingS.BookingRooms.Count > 0)
                            {
                                var bkex = bookingS.BookingRooms.FirstOrDefault(b => b.HasAddExtraBed);
                                if (bkex != null && bkex.HasAddExtraBed) specialRequest += Environment.NewLine + "Extra bed needed";
                            }
                            sheet.Cells[crow - 1, 4].Value = specialRequest;

                            sheet.Cells[crow - 1, 5].Value = bookingS.PickupAddress;

                            sheet.Cells[crow - 1, 6].Value = string.Format("OS{0:00000}", bookingId);
                        }
                        bookingId = booking.Id;
                        bookingS = booking;
                        mergeR = crow + 1;
                    }
                    if (stt == _bookingDay.Count)
                    {
                        var specialRequest = booking.SpecialRequest;
                        if (booking.BookingRooms.Count > 0)
                        {
                            var bkex = booking.BookingRooms.FirstOrDefault(b => b.HasAddExtraBed);
                            if (bkex != null && bkex.HasAddExtraBed) specialRequest += Environment.NewLine + "Extra bed needed";
                        }

                        var mergedRangeE = sheet.Cells.GetSubrange(string.Format("E{0}:E{1}", mergeR, crow + 1));
                        mergedRangeE.Merged = true;
                        mergedRangeE.Value = specialRequest;

                        var mergedRangeF = sheet.Cells.GetSubrange(string.Format("F{0}:F{1}", mergeR, crow + 1));
                        mergedRangeF.Merged = true;
                        mergedRangeF.Value = booking.PickupAddress;

                        var mergedRange = sheet.Cells.GetSubrange(string.Format("G{0}:G{1}", mergeR, crow + 1));
                        mergedRange.Merged = true;
                        mergedRange.Value = booking.BookingIdOS;
                    }
                    stt++;
                    crow++;
                }
            }
            //foreach (KeyValuePair<Room, Booking> keyValuePair in _bookingDay)
            //{
            //    sheet.Cells[crow, 0].Value = stt;
            //    sheet.Cells[crow, 1].Value = keyValuePair.Key.Name;
            //    var listRoom = keyValuePair.Value.BookingRooms.Where(c => c.Room != null && c.Room.Id == keyValuePair.Key.Id).ToList();
            //    if (listRoom.Count > 0)
            //    {
            //        sheet.Cells[crow, 2].Value = listRoom[0].RoomType.Name;
            //        if (listRoom[0].IsSingle) sheet.Cells[crow, 2].Value = "Single";
            //    }
            //    var listCus = new List<Customer>();
            //    foreach (BookingRoom bookingRoom in listRoom)
            //    {
            //        listCus.AddRange(bookingRoom.Customers);
            //    }
            //    var cus = "";
            //    if (listCus.Count > 0)
            //    {
            //        for (int i = 0; i < listCus.Count(); i++)
            //        {
            //            if (!string.IsNullOrWhiteSpace(listCus[i].Fullname))
            //            {
            //                if (i < listCus.Count - 1)
            //                {
            //                    cus += listCus[i].Fullname + Environment.NewLine;
            //                }
            //                else
            //                {
            //                    cus += listCus[i].Fullname;
            //                }
            //            }
            //        }
            //    }
            //    sheet.Cells[crow, 3].Value = cus;
            //    sheet.Cells[crow, 4].Value = keyValuePair.Value.SpecialRequest;
            //    sheet.Cells[crow, 5].Value = keyValuePair.Value.PickupAddress;

            //    if (bookingId == 0)
            //    {
            //        bookingId = keyValuePair.Value.Id;
            //    }
            //    if (bookingId > 0 && bookingId != keyValuePair.Value.Id)
            //    {
            //        var mergedRange = sheet.Cells.GetSubrange(string.Format("G{0}:G{1}", mergeR, crow));
            //        mergedRange.Merged = true;
            //        mergedRange.Value = keyValuePair.Value.BookingIdOS;
            //        bookingId = keyValuePair.Value.Id;
            //        mergeR = crow + 1;
            //    }
            //    if (stt == _bookingDay.Count)
            //    {
            //        var mergedRange = sheet.Cells.GetSubrange(string.Format("G{0}:G{1}", mergeR, crow + 1));
            //        mergedRange.Merged = true;
            //        mergedRange.Value = keyValuePair.Value.BookingIdOS;
            //    }
            //    stt++;
            //    crow++;
            //}
            excelFile.Save(Response, string.Format("room_plan{0:dd-MM-yyyy}.xlsx", _currentDate));
        }
    }
}
