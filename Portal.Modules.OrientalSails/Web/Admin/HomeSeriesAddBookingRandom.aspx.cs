using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using CMS.Core.Domain;
using CMS.Web.Util;
using NHibernate.Criterion;
using Portal.Modules.OrientalSails.BusinessLogic;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;
using Portal.Modules.OrientalSails.Web.Util;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    /// <summary>
    /// thêm booking series ngẫu nhiên chọn phòng
    /// </summary>
    public partial class HomeSeriesAddBookingRandom : SailsAdminBase
    {
        private DateTime? _date;
        private RoomClass _roomClass;
        private SailsTrip _trip;
        private IList _bookingRooms;
        private IList _policies;
        private Cruise _cruise;
        private IList<Room> _roomCruises;
        protected SailsTrip Trip
        {
            get
            {
                if (_trip == null)
                {
                    _trip = Module.TripGetById(Convert.ToInt32(ddlTrips.SelectedValue));
                }
                return _trip;
            }
        }

        protected DateTime? Date
        {
            get
            {
                if (_date == null)
                {
                    try
                    {
                        _date = DateTime.ParseExact(txtDate.Text, "dd/MM/yyyy",
                            CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                        return null;
                    }
                }
                return _date;
            }
        }

        protected Cruise ActiveCruise
        {
            get
            {
                if (_cruise == null)
                {
                    _cruise = Module.CruiseGetById(Convert.ToInt32(ddlCruises.SelectedValue));
                }
                return _cruise;
            }
        }

        private IList _trips;
        private AddBookingBLL addBookingBLL;
        public AddBookingBLL AddBookingBLL
        {
            get
            {
                if (addBookingBLL == null)
                    addBookingBLL = new AddBookingBLL();
                return addBookingBLL;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Lấy tất cả các hành trình để lọc ra các hành trình có nhiều option, phục vụ cho việc ẩn/hiện hộp chọn option
            _trips = Module.TripGetAll(true);
            string visibleIds = string.Empty;
            foreach (SailsTrip trip in _trips)
            {
                if (trip.NumberOfOptions == 2)
                {
                    visibleIds += "#" + trip.Id + "#";
                }
            }
            if (!IsPostBack)
            {
                ddlStatusType.DataSource = Enum.GetNames(typeof(StatusType));
                ddlStatusType.DataBind();
                ddlStatusType.Items.RemoveAt(2);
                ddlStatusType.SelectedIndex = 0;
                BindTrips();
                BindCruises();
                BindServices();
                var date = Request["date"];
                if (!string.IsNullOrWhiteSpace(date))
                {
                    txtDate.Text = date;
                    txtDate_TextChanged(null, null);
                    lbtCruiseName_Click(null, null);
                }
            }
        }

        protected void Page_Unload(object sender, EventArgs e)
        {
            if (addBookingBLL != null)
            {
                addBookingBLL.Dispose();
                addBookingBLL = null;
            }
        }
        protected void rptClass_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is RoomClass)
            {
                Repeater rptTypes = (Repeater)e.Item.FindControl("rptTypes");
                _roomClass = e.Item.DataItem as RoomClass;
                // Sử dụng biến toàn cục _roomClass để check với tất cả các room type
                rptTypes.DataSource = AllRoomTypes;
                rptTypes.DataBind();
            }
        }

        protected void rptTypes_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is RoomTypex)
            {
                RoomTypex type = (RoomTypex)e.Item.DataItem;
                Label labelName = e.Item.FindControl("labelName") as Label;
                if (labelName != null)
                {
                    labelName.Text = string.Format("{0} {1}", _roomClass.Name, type.Name);
                }

                #region -- thông tin về khách và phòng trống --

                DropDownList ddlAdults = (DropDownList)e.Item.FindControl("ddlAdults");
                DropDownList ddlChild = (DropDownList)e.Item.FindControl("ddlChild");
                DropDownList ddlBaby = (DropDownList)e.Item.FindControl("ddlBaby");
                if (Date != null)
                {
                    ddlAdults.Items.Clear();
                    ddlChild.Items.Clear();
                    ddlBaby.Items.Clear();

                    int roomCount;

                    Locked locked = Module.LockedCheckByDate(ActiveCruise, Date.Value,
                        Date.Value.AddDays(Trip.NumberOfDay - 1));
                    if (locked != null)
                    {
                        if (!string.IsNullOrEmpty(locked.Description))
                        {
                            ShowErrors(
                                string.Format(
                                    "Hành trình này đã bị khóa với lý do: {0}, vẫn có thể add booking như buộc phải chuyển sang tàu khác",
                                    locked.Description));
                        }
                        else
                        {
                            ShowErrors(
                                string.Format(
                                    "Hành trình này đã bị khóa, vẫn có thể add booking như buộc phải chuyển sang tàu khác"));
                        }
                        // Khi ấy thì lấy về số phòng, trong đó bỏ qua lock
                        roomCount = Module.RoomCount(_roomClass, type, ActiveCruise, Date.Value, Trip.NumberOfDay, true,
                            Trip.HalfDay);
                    }
                    else
                    {
                        roomCount = Module.RoomCount(_roomClass, type, ActiveCruise, Date.Value, Trip.NumberOfDay,
                            Trip.HalfDay);
                    }

                    if (roomCount < 0)
                    {
                        e.Item.Visible = false;
                        return;
                    }
                    int maxAdults;

                    // Nếu là phòng ở ghép thì số người = giá trị lấy về, số phòng = giá trị lấy về chia cho sức chứa
                    if (type.IsShared)
                    {
                        maxAdults = roomCount;
                        roomCount = roomCount / type.Capacity;
                    }
                    else
                    {
                        // Nếu không số người = giá gị lấy về nhân với sức chứa
                        maxAdults = roomCount * type.Capacity;
                    }

                    // Nếu là phòng ở ghép thì cho chọn theo số người
                    if (type.IsShared)
                    {
                        // Từ 0 đến số người max
                        for (int i = 0; i <= maxAdults; i++)
                        {
                            ddlAdults.Items.Add(new ListItem(string.Format(Resources.formatPersonItem, i), i.ToString()));
                        }
                    }
                    else
                    {
                        // Nếu không phải phòng ở ghép thì lấy theo số phòng
                        for (int i = 0; i <= roomCount; i++)
                        {
                            ddlAdults.Items.Add(new ListItem(string.Format(Resources.formatRoomItem, i), i.ToString()));
                        }
                    }

                    for (int i = 0; i <= roomCount; i++)
                    {
                        ddlBaby.Items.Add(new ListItem(string.Format(Resources.formatBabyItem, i), i.ToString()));
                        ddlChild.Items.Add(new ListItem(string.Format(Resources.formatChildItem, i), i.ToString()));
                    }

                }

                #endregion

                #region -- Giá thủ công --

                // Nếu hệ thống được cấu hình để nhập giá thủ công cho từng loại phòng ngay khi add booking thì hiển thị khu vực này
                PlaceHolder plhCustomPrice = e.Item.FindControl("plhCustomPrice") as PlaceHolder;
                if (plhCustomPrice != null)
                {
                    if (CustomPriceAddBooking)
                    {
                        plhCustomPrice.Visible = true;
                        TextBox txtPrice = e.Item.FindControl("txtPrice") as TextBox;
                        if (txtPrice != null)
                        {
                            double price;
                            try
                            {
                                price = BookingRoom.Calculate(_roomClass, type, type.Capacity, 0, false, _trip,
                                    ActiveCruise,
                                    TripOption.Option1, Date.Value, Module, _policies,
                                    ChildPrice, AgencySupplement, null);
                            }
                            catch (PriceException ex)
                            {
                                ShowErrors(ex.Message);
                                price = 0;
                            }
                            txtPrice.Text = price.ToString("0.##");
                        }
                    }
                    else
                    {
                        plhCustomPrice.Visible = false;
                    }
                }

                #endregion
            }
        }

        protected void rptExtraServices_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is ExtraOption)
            {
                HtmlInputCheckBox chkService = (HtmlInputCheckBox)e.Item.FindControl("chkService");
                chkService.Checked = ((ExtraOption)e.Item.DataItem).IsIncluded;
                // Mặc định là chọn dịch vụ nếu đã được bao gồm trong giá phòng
            }
        }

        protected void rptCruises_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is Cruise)
            {
                var cruise = (Cruise)e.Item.DataItem;

                if (!cruise.Trips.Contains(Trip))
                {
                    e.Item.Visible = false;
                    return;
                }

                int total = 0;
                string detail = string.Empty;
                for (int i = 0; i < AllRoomClasses.Count; i++)
                {
                    var rclass = AllRoomClasses[i] as RoomClass;
                    for (int j = 0; j < AllRoomTypes.Count; j++)
                    {
                        var rtype = AllRoomTypes[j] as RoomTypex;
                        int avail;
                        if (TripBased)
                        {
                            avail = Module.RoomCount(rclass, rtype, cruise, Date.Value, Trip.NumberOfDay, Trip.HalfDay);
                        }
                        else
                        {
                            avail = Module.RoomCount(rclass, rtype, cruise, Date.Value, Trip.NumberOfDay, Trip.HalfDay);
                        }
                        if (avail > 0)
                        {
                            total += avail;
                            detail += string.Format("{0} {2} {1} ", avail, rtype.Name, rclass.Name);
                        }
                    }
                }

                var lbtCruiseName = (LinkButton)e.Item.FindControl("lbtCruiseName");
                if (lbtCruiseName != null)
                {
                    lbtCruiseName.Text = cruise.Name;
                    lbtCruiseName.CommandArgument = cruise.Id.ToString();
                    lbtCruiseName.Attributes.Add("totalAvaiable", total.ToString());
                }

                if (ViewState["cruiseId"] != null)
                {
                    var cruiseIdViewState = (int)ViewState["cruiseId"];
                    //if (cruise.Id == cruiseIdViewState)
                        //chkCharter.Visible = CheckCruiseForCharter(cruise, total);
                }

                var litRoomCount = e.Item.FindControl("litRoomCount") as Literal;
                if (litRoomCount != null)
                {
                    litRoomCount.Text = total.ToString();
                }

                var litRoomDetail = e.Item.FindControl("litRoomDetail") as Literal;
                if (litRoomDetail != null)
                {
                    litRoomDetail.Text = detail;
                }

                var trCruise = e.Item.FindControl("trCruise") as HtmlTableRow;
                if (trCruise != null)
                {
                    if (total > 0)
                    {
                        trCruise.Attributes.Add("class", "success");
                    }
                    else
                    {
                        trCruise.Attributes.Add("class", "danger");
                    }
                }
            }
        }

        protected void rptPendings_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is Booking)
            {
                var booking = (Booking)e.Item.DataItem;
                var hplCode = e.Item.FindControl("hplCode") as HyperLink;
                if (hplCode != null)
                {
                    hplCode.Text = string.Format(BookingFormat, booking.Id);
                    hplCode.NavigateUrl = string.Format("BookingView.aspx?NodeId={0}&SectionId={1}&bi={2}",
                        Node.Id, Section.Id, booking.Id);
                }

                var hplAgency = e.Item.FindControl("hplAgency") as HyperLink;
                if (hplAgency != null)
                {
                    hplAgency.Text = booking.Agency.Name;
                    hplAgency.NavigateUrl = string.Format("AgencyEdit.aspx?NodeId={0}&SectionId={1}&AgencyId={2}",
                        Node.Id, Section.Id, booking.Agency.Id);
                }

                ValueBinder.BindLiteral(e.Item, "litRooms", booking.RoomName);
                ValueBinder.BindLiteral(e.Item, "litTrip", booking.Trip.Name);
                //ValueBinder.BindLiteral(e.Item, "litPartner", booking.Agency.Name);
                if (booking.Deadline.HasValue)
                    ValueBinder.BindLiteral(e.Item, "litPending", booking.Deadline.Value.ToString("HH:mm dd/MM/yyyy"));

                var lblCreatedBy = e.Item.FindControl("lblCreatedBy") as Label;
                if (lblCreatedBy != null)
                {
                    lblCreatedBy.Text = booking.CreatedBy.FullName;
                    ValueBinder.BindLiteral(e.Item, "litCreatedBy", booking.CreatedBy.FullName);
                    ValueBinder.BindLiteral(e.Item, "litCreatorPhone", booking.CreatedBy.Website);
                    ValueBinder.BindLiteral(e.Item, "litCreatorEmail", booking.CreatedBy.Email);
                }
                if (booking.Agency.Sale != null)
                {
                    ValueBinder.BindLabel(e.Item, "lblSaleInCharge", booking.Agency.Sale.FullName);
                    ValueBinder.BindLiteral(e.Item, "litSale", booking.Agency.Sale.FullName);
                    ValueBinder.BindLiteral(e.Item, "litSalePhone", booking.Agency.Sale.Website);
                    ValueBinder.BindLiteral(e.Item, "litSaleEmail", booking.Agency.Sale.Email);

                }
            }
        }

        protected void lbtCruiseName_Click(object sender, EventArgs e)
        {
            LinkButton lbtCruiseName = (LinkButton)sender;
            if (sender != null)
                _cruise = Module.CruiseGetById(Convert.ToInt32(lbtCruiseName.CommandArgument));
            else _cruise = Module.GetById<Cruise>(Convert.ToInt32(Request["cruiseId"]));
            rptClass.DataSource = AllRoomClasses;
            rptClass.DataBind();
            litCurrentCruise.Text = _cruise.Name;
            ddlCruises.SelectedValue = _cruise.Id.ToString();
            plhCruiseName.Visible = true;

            //---------------------------------------------------
            ViewState["cruiseId"] = _cruise.Id;
            hidCruiseSelect.Value = _cruise.Id.ToString();
            hidDateSelect.Value = Date.Value.ToString("dd/MM/yyyy");
            _roomCruises = Module.RoomGetAll2(_cruise);
            var _currentDayBookings = Module.GetBookingByDate(Date.Value, _cruise);
            var _nextDayBookings = Module.GetBookingByDate(Date.Value.AddDays(1), _cruise);
            var list = new List<Room>();
            var listAvailable = new List<Room>();

            foreach (Room room in _roomCruises)
            {
                var booking = FindBooking(room, Date.Value, _currentDayBookings);
                if (booking != null && booking.Id > 0)
                {
                    var nextBk = FindBooking(room, Date.Value.AddDays(1), _nextDayBookings);
                    if (nextBk == null)
                    {
                        list.Add(room);
                    }
                }
                else listAvailable.Add(room);
            }
            if (list.Count > 0)
            {
                phRoomAvailableNight2.Visible = true;
                rptRoomAvailableNight2.DataSource = list;
                rptRoomAvailableNight2.DataBind();
            }
            else phRoomAvailableNight2.Visible = false;
            var total = 0;
            if (lbtCruiseName != null)
                total = Convert.ToInt32(lbtCruiseName.Attributes["totalAvaiable"]);
            else total = listAvailable.Count;
            //Kiểm tra tàu nếu còn trống tất cả các phòng thì hiện nút charter
            //còn nếu tàu đã có booking thì ẩn nút charter 
            //chkCharter.Visible = CheckCruiseForCharter(_cruise, total);
        }
        private Booking FindBooking(Room room, DateTime date, IList<Booking> _allBooking)
        {
            Booking booking = null;
            if (_allBooking != null)
            {
                foreach (Booking vBookingRoom in _allBooking)
                {
                    var numberOfDay = vBookingRoom.StartDate.AddDays(vBookingRoom.Trip.NumberOfDay - 2);
                    var roomIsExist = vBookingRoom.BookingRooms != null && vBookingRoom.BookingRooms.Any(br => br.Room != null && br.Room.Id == room.Id);

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
        public void RoomCheckAvaiable()
        {
            rptCruises.DataSource = Module.CruiseGetAll();
            rptCruises.DataBind();

            ICriterion criterion = Expression.And(Expression.Eq("Status", StatusType.Pending),
                Expression.Ge("Deadline", DateTime.Now)); // pending và chưa hết hạn

            criterion = Expression.And(criterion, Expression.Eq("Deleted", false));
            criterion = Module.AddDateExpression(criterion, Date.Value);

            var list = Module.BookingGetByCriterion(criterion, null, 0, 0);
            if (list.Count > 0)
            {
                plhPending.Visible = true;
                rptPendings.DataSource = list;
                rptPendings.DataBind();
            }
            else
            {
                plhPending.Visible = false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (!CheckAvailable())
            {
                return;
            }

            // Kiểm tra số phòng đã chọn, nếu chưa chọn phòng nào thì thông báo yêu cầu
            if (_bookingRooms.Count <= 0 && !chkCharter.Checked)
            {
                ShowErrors("Hãy chọn ít nhất một phòng");
                return;
            }

            //2. Lưu thông tin phòng như thế nào
            // Dùng vòng lặp lưu thông tin đơn thuần, không có giá trị đi kèm nào cả

            // Phải lưu thông tin booking trước

            #region -- Booking --

            Booking booking = new Booking();
            booking.CreatedBy = Page.User.Identity as User;
            booking.CreatedDate = DateTime.Now;
            booking.ModifiedDate = DateTime.Now;
            booking.Partner = null;
            booking.Sale = booking.CreatedBy;
            booking.StartDate = Date.Value;


            if (ApprovedDefault)
            {
                booking.Status = StatusType.Approved;
            }
            else
            {
                booking.Status = StatusType.Pending;
            }

            booking.Status = StatusType.Pending;

            if (TripBased)
            {
                booking.Trip = Module.TripGetById(Convert.ToInt32(ddlTrips.SelectedValue));
            }
            else
            {
                booking.Trip = null;
            }
            var cruise = null as Cruise;
            if (ViewState["cruiseId"] != null)
            {
                var cruiseIdViewState = (int)ViewState["cruiseId"];
                cruise = Module.GetObject<Cruise>(cruiseIdViewState);
            }
            booking.Cruise = cruise;
            booking.IsCharter = chkCharter.Checked;
            booking.Total = 0;

            // Xác định custom booking id
            if (UseCustomBookingId)
            {
                int maxId = Module.BookingGenerateCustomId(booking.StartDate);
                booking.Id = maxId;
            }

            //if (booking.Cruise.Name.Contains("Scorpio") && !booking.Cruise.Name.Contains("1"))
            //{
            //    booking.IsCharter = true;
            //}

            // series
            var series = Module.GetById<Series>(Convert.ToInt32(Request["si"]));
            booking.Agency = series.Agency;
            booking.Booker = series.Booker;
            booking.Status = StatusType.Pending;
            booking.Total = 0;
            booking.Series = series;
            booking.PickupAddress = "N/A";
            booking.Deadline = booking.StartDate.Subtract(new TimeSpan(series.CutoffDate, 0, 0, 0));

            booking.AgencyCode = txtAgencyCode.Text;
            booking.SpecialRequest = txtSpecialRequest.Text;

            var statusType = (StatusType)Enum.Parse(typeof(StatusType), ddlStatusType.SelectedValue);
            booking.Status = statusType;
            if (statusType == StatusType.CutOff)
            {
                if (!string.IsNullOrWhiteSpace(txtCutOffDays.Text)) booking.CutOffDays = Int32.Parse(txtCutOffDays.Text.Trim());
            }
            if (statusType == StatusType.Pending)
            {
                //booking.Status = StatusType.Pending;
                if (!string.IsNullOrWhiteSpace(txtDeadline.Text)) booking.Deadline = DateTime.ParseExact(txtDeadline.Text, "dd/MM/yyyy HH:mm",
                    CultureInfo.InvariantCulture);
            }

            Module.Save(booking, UserIdentity);
            #endregion

            // Sau đó mới có thể lưu thông tin phòng
            // Đối với phòng có baby và child theo phân bố từ trước sẽ thêm child, baby
            _roomCruises = Module.RoomGetAll2(booking.Cruise);
            var currentDayBookings = Module.GetBookingByDate(booking.StartDate, booking.Cruise);
            var nextDayBookings = Module.GetBookingByDate(booking.StartDate.AddDays(1), booking.Cruise);

            var listRoomAvailable = new List<Room>();

            foreach (Room room in _roomCruises)
            {
                var bk = FindBooking(room, booking.StartDate, currentDayBookings);
                if (bk == null)
                {
                    if (booking.Trip != null && booking.Trip.NumberOfDay > 2)
                    {
                        var nextBk = FindBooking(room, booking.StartDate.AddDays(1), nextDayBookings);
                        if (nextBk == null)
                        {
                            listRoomAvailable.Add(room);
                        }
                    }
                    else listRoomAvailable.Add(room);
                }
            }
            #region -- Booking Room --
            foreach (BookingRoom room in _bookingRooms)
            {
                var getRandomRoom = listRoomAvailable.First(r => r.RoomType.Id == room.RoomType.Id);
                room.Room = getRandomRoom;
                room.Book = booking;
                Module.Save(room);
                listRoomAvailable.Remove(getRandomRoom);
                // Ngoài ra còn phải lưu thông tin khách hàng

                #region -- Thông tin khách hàng, suy ra từ thông tin phòng --

                for (int ii = 1; ii <= room.Booked; ii++)
                {
                    Customer customer = new Customer();
                    customer.BookingRoom = room;
                    customer.Type = CustomerType.Adult;
                    if (CustomerPrice)
                    {
                        customer.Total = room.Total / 2;
                    }
                    Module.Save(customer);
                    room.Customers.Add(customer);
                }

                if (room.HasBaby)
                {
                    Customer customer = new Customer();
                    customer.BookingRoom = room;
                    customer.Type = CustomerType.Baby;
                    Module.Save(customer);
                    room.Customers.Add(customer);
                }

                if (room.HasChild)
                {
                    Customer customer = new Customer();
                    customer.BookingRoom = room;
                    customer.Type = CustomerType.Children;
                    Module.Save(customer);
                    room.Customers.Add(customer);
                }
                Module.Save(room);
                #endregion
            }

            #endregion

            #region -- Thông tin dịch vụ đi kèm --

            foreach (RepeaterItem serviceItem in rptExtraServices.Items)
            {
                //HiddenField hiddenValue = (HiddenField)serviceItem.FindControl("hiddenValue");
                HiddenField hiddenId = (HiddenField)serviceItem.FindControl("hiddenId");
                HtmlInputCheckBox chkService = (HtmlInputCheckBox)serviceItem.FindControl("chkService");
                if (chkService.Checked)
                {
                    ExtraOption service = Module.ExtraOptionGetById(Convert.ToInt32(hiddenId.Value));
                    BookingExtra extra = new BookingExtra();
                    extra.Booking = booking;
                    extra.ExtraOption = service;
                    Module.Save(extra);
                }
            }

            #endregion

            #region -- Track thêm mới --

            BookingTrack track = new BookingTrack();
            track.Booking = booking;
            track.ModifiedDate = DateTime.Now;
            track.User = UserIdentity;
            Module.SaveOrUpdate(track);

            BookingChanged change = new BookingChanged();
            change.Action = BookingAction.Created;
            change.Parameter = string.Format("{0}", booking.Total);
            change.Track = track;
            Module.SaveOrUpdate(change);

            #endregion

            if (chkCharter.Checked)
            {
                var locked = new Locked();
                locked.Charter = booking;
                locked.Cruise = booking.Cruise;
                locked.Description = "Booking charter";
                locked.Start = booking.StartDate;
                locked.End = booking.EndDate;
                Module.SaveOrUpdate(locked);
            }

            //var smtpClient = new SmtpClient("mail.orientalsails.com", 26)
            //{
            //    Credentials = new NetworkCredential("mo@orientalsails.com", "EGGaXBwuEWa+")

            //};

            //var message = new MailMessage
            //{
            //    From = new MailAddress("mo@orientalsails.com"),
            //    IsBodyHtml = true,
            //    BodyEncoding = Encoding.UTF8,
            //    Body = "alooo",
            //    Subject = "CutOff Date Booking Reminder"
            //};

            //message.To.Add(new MailAddress("it2@atravelmate.com"));
            //if (booking.BookingSale != null)
            //{
            //    if (booking.BookingSale.Sale != null)
            //    {
            //        message.To.Add(new MailAddress(booking.BookingSale.Sale.Email));
            //    }
            //}
            //smtpClient.Send(message);

            //            PageRedirect(string.Format("BookingView.aspx?NodeId={0}&SectionId={1}&bi={2}&Notify=0", Node.Id,
            //                Section.Id, booking.Id));
            Page.ClientScript.RegisterStartupScript(this.GetType(), "RefreshParentPage", "RefreshParentPage();", true);

        }

        /// <summary>
        ///     Kiểm tra số phòng của tàu với số phòng còn trống của tàu.
        /// </summary>
        /// <param name="cruise"></param>
        /// <param name="roomAvaiable"></param>
        /// <returns>True tàu còn trống tất cả các phòng False tàu đã có booking</returns>
        public bool CheckCruiseForCharter(Cruise cruise, int roomAvaiable)
        {
            if (cruise != null)
            {
                int roomOfCruise =
                    Module.CountObjet<Room>(Expression.And(Expression.Eq("Cruise", cruise),
                        Expression.Eq("Deleted", false)));
                return roomOfCruise == roomAvaiable;
            }
            else
            {
                throw new Exception("cruise = null");
            }
        }

        /// <summary>
        /// Load dữ liệu hành trình vào hộp chọn
        /// </summary>
        public void BindTrips()
        {
            _trips.RemoveAt(4);
            _trips.RemoveAt(4);
            _trips.RemoveAt(4);
            _trips.RemoveAt(4);
            ddlTrips.DataSource = _trips; // Danh sách trip luôn được get về trước khi gọi tới hàm bind trips
            ddlTrips.DataTextField = "Name";
            ddlTrips.DataValueField = "Id";
            ddlTrips.DataBind();
        }

        /// <summary>
        /// Load dữ liệu hành tàu vào hộp chọn
        /// </summary>
        public void BindCruises()
        {
            ddlCruises.DataSource = Module.CruiseGetAll();
            ddlCruises.DataTextField = "Name";
            ddlCruises.DataValueField = "Id";
            ddlCruises.DataBind();

            if (ddlCruises.Items.Count == 1)
            {
                ddlCruises.Visible = false;
            }
        }

        /// <summary>
        /// Load các dịch vụ gia tăng vào danh sách hộp đánh dấu
        /// </summary>
        public void BindServices()
        {
            rptExtraServices.DataSource = Module.ExtraOptionGetBooking();
            rptExtraServices.DataBind();
        }

        /// <summary>
        /// Kiểm tra xem có còn đủ phòng trống hay không, đồng thời tạo ra dữ liệu booking room
        /// </summary>
        /// <returns></returns>
        public bool CheckAvailable()
        {
            _bookingRooms = new ArrayList();
            foreach (RepeaterItem classItem in rptClass.Items)
            {
                var hiddenId = (HiddenField)classItem.FindControl("hiddenId");
                var rclass = Module.RoomClassGetById(Convert.ToInt32(hiddenId.Value));
                var rptTypes = (Repeater)classItem.FindControl("rptTypes");
                foreach (RepeaterItem typeItem in rptTypes.Items)
                {
                    if (!typeItem.Visible)
                        continue; // Bỏ qua nếu là đối tượng ẩn (ẩn là do không tồn tại loại phòng này)
                    HiddenField hiddenTypeId = (HiddenField)typeItem.FindControl("hiddenId");
                    RoomTypex rtype = Module.RoomTypexGetById(Convert.ToInt32(hiddenTypeId.Value));
                    DropDownList ddlAdults = (DropDownList)typeItem.FindControl("ddlAdults");
                    DropDownList ddlChild = (DropDownList)typeItem.FindControl("ddlChild");
                    DropDownList ddlBaby = (DropDownList)typeItem.FindControl("ddlBaby");
                    double unitprice = 0;
                    if (CustomPriceAddBooking)
                    {
                        TextBox txtPrice = (TextBox)typeItem.FindControl("txtPrice");
                        unitprice = Convert.ToDouble(txtPrice.Text);
                    }

                    // Tìm số phòng available
                    int roomCount;
                    Locked locked = null;
                    var cruise = null as Cruise;
                    if (ViewState["cruiseId"] != null)
                    {
                        var cruiseIdViewState = (int)ViewState["cruiseId"];
                        cruise = Module.GetObject<Cruise>(cruiseIdViewState);
                        locked = Module.LockedCheckByDate(cruise, Date.Value,
                            Date.Value.AddDays(Trip.NumberOfDay - 1));
                    }
                    if (locked != null)
                    {
                        if (!string.IsNullOrEmpty(locked.Description))
                        {
                            ShowErrors(
                                string.Format(
                                    "Hành trình này đã bị khóa với lý do: {0}, vẫn có thể add booking như buộc phải chuyển sang tàu khác",
                                    locked.Description));
                        }
                        // Khi ấy thì lấy về số phòng, trong đó bỏ qua lock
                        roomCount = Module.RoomCount(rclass, rtype, cruise, Date.Value, Trip.NumberOfDay, true,
                            Trip.HalfDay);
                    }
                    else
                    {
                        roomCount = Module.RoomCount(rclass, rtype, cruise, Date.Value, Trip.NumberOfDay,
                            Trip.HalfDay);
                    }

                    if (roomCount < 0)
                    {
                        continue;
                    }

                    // Lấy về số phòng (đối với phòng thường) và số người đối với phòng ở ghép
                    int adult = Convert.ToInt32(ddlAdults.SelectedValue);
                    int child = Convert.ToInt32(ddlChild.SelectedValue);
                    int baby = Convert.ToInt32(ddlBaby.SelectedValue);
                    int room;

                    // Nếu là phòng ở ghép thì tính theo đơn vị số người chứ không phải số phòng
                    if (rtype.IsShared)
                    {
                        room = adult / rtype.Capacity;
                    }
                    else
                    {
                        room = adult;
                    }

                    if (child > room || baby > room)
                    {
                        ShowErrors(Resources.errorOneChildOneBaby);
                        return false;
                    }

                    if (adult > roomCount)
                    {
                        ShowErrors(Resources.errorNotEnoughAvailable);
                        return false;
                    }

                    // Nếu đủ số phòng trống thì mới thực hiện việc tạo dữ liệu phòng đã book

                    bool isShared = false;
                    if (rtype.IsShared && adult % 2 == 1)
                    {
                        room += 1;
                        isShared = true;
                    }

                    // Với mỗi loại room, căn cứ theo số phòng xác định phòng có child, baby và phòng share
                    for (int ii = 1; ii <= room; ii++)
                    {
                        var booking = new BookingRoom();
                        booking.BookingType = BookingType.Double;
                        booking.RoomType = rtype;
                        booking.RoomClass = rclass;

                        // Nếu là phòng cuối thì cho phòng này là phòng shared
                        if (ii == room && isShared)
                        {
                            booking.Booked = 1;
                        }
                        else
                        {
                            booking.Booked = 2;
                        }

                        // Các phòng đầu sẽ ghép child/baby vào (nếu có) vì có thể đảm bảo rằng không phải ở ghép
                        if (child > 0)
                        {
                            booking.HasChild = true;
                            child--;
                        }

                        if (baby > 0)
                        {
                            booking.HasBaby = true;
                            baby--;
                        }

                        if (CustomPriceAddBooking)
                        {
                            booking.Total = unitprice;
                        }

                        _bookingRooms.Add(booking);
                    }
                }
            }
            return true;
        }
        public bool CheckInputAndShowError()
        {
            string error = "";
            bool haveError = false;
            if (ddlTrips.SelectedIndex == -1)
            {
                error += "Chưa chọn trip <br/>";
                haveError = true;
            }
            if (String.IsNullOrEmpty(txtDate.Text))
            {
                error += "Chưa chọn start date <br/>";
                haveError = true;
            }
            if (haveError)
                ShowErrors(error);

            return haveError;

        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            RoomCheckAvaiable();
        }

        protected void ddlTrips_SelectedIndexChanged(object sender, EventArgs e)
        {
            RoomCheckAvaiable();
        }
    }
}