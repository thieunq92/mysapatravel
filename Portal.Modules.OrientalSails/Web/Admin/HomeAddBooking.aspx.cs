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
using Portal.Modules.OrientalSails.BusinessLogic.Share;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Enums;
using Portal.Modules.OrientalSails.Web.UI;
using Portal.Modules.OrientalSails.Web.Util;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    /// <summary>
    /// trang add booking khi chọn phòng
    /// </summary>
    public partial class HomeAddBooking : SailsAdminBase
    {
        private DateTime? _date;
        private RoomClass _roomClass;
        private SailsTrip _trip;
        private IList _bookingRooms;
        private IList _policies;
        private Cruise _cruise;
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
                        _date = DateTime.ParseExact(Request["date"], "dd/MM/yyyy",
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
                    _cruise = Module.CruiseGetById(Convert.ToInt32(Request["cruiseId"]));
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
            if (!PermissionUtil.UserCheckPermission(UserIdentity.Id, (int)PermissionEnum.FORM_ADDBOOKING))
            {
                ShowErrors("You don't have permission to perform this action");
                return;
            }
            // Lấy tất cả các hành trình để lọc ra các hành trình có nhiều option, phục vụ cho việc ẩn/hiện hộp chọn option

            var trips = Module.TripGetAll(true);

            _trips = new List<SailsTrip>();
            if (!string.IsNullOrWhiteSpace(Request["d"]) && Request["d"] == "3")
            {
                foreach (SailsTrip trip in trips)
                {
                    if (trip.NumberOfDay == 3)
                    {
                        if (ActiveCruise.Trips.Contains(trip)) _trips.Add(trip);
                    }
                }
            }
            else
            {
                foreach (SailsTrip trip in trips)
                {
                    if (trip.NumberOfDay != 3)
                    {
                        if (ActiveCruise.Trips.Contains(trip)) _trips.Add(trip);
                    }
                }
            }
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
                ddlStatusType.SelectedIndex = 1;
                BindTrips();
                LoadInfo();
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
        protected void rptExtraServices_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.DataItem is ExtraOption)
            {
                HtmlInputCheckBox chkService = (HtmlInputCheckBox)e.Item.FindControl("chkService");
                chkService.Checked = ((ExtraOption)e.Item.DataItem).IsIncluded;
                // Mặc định là chọn dịch vụ nếu đã được bao gồm trong giá phòng
            }
        }
        protected void LoadInfo()
        {
            _cruise = Module.CruiseGetById(Convert.ToInt32(Request["cruiseId"]));
            txtDate.Text = Request["date"];
            var ids = new List<int>();
            var strIds = Request["roomIds"].Split(',');
            foreach (string s in strIds)
            {
                if (!string.IsNullOrEmpty(s) && !ids.Contains(Convert.ToInt32(s))) ids.Add(Convert.ToInt32(s));
            }
            var allRoom = Module.RoomGetAll2(_cruise);
            var rooms = allRoom.Where(r => ids.Contains(r.Id)).ToList();
            bool isTrip3D = !string.IsNullOrWhiteSpace(Request["d"]) && Request["d"] == "3";
            var checkAvailable = Module.CheckExistAddRoom(rooms, _cruise, Date.Value, isTrip3D);
            if (checkAvailable)
            {
                rptRoom.DataSource = rooms;
                rptRoom.DataBind();
                litCurrentCruise.Text = _cruise.Name;
                chkCharter.Checked = chkCharter.Visible = rooms.Count == allRoom.Count;
            }
            else
            {
                ShowErrors("Phòng đã được chọn cho booking khác, vui lòng chọn phòng khác");
                btnSave.Visible = false;
            }

            //---------------------------------------------------
            ViewState["cruiseId"] = _cruise.Id;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(agencySelector.Value))
            {
                lblMsg.Text = ("Hãy chọn đối tác");
                return;
            }
            //2. Lưu thông tin phòng như thế nào
            // Dùng vòng lặp lưu thông tin đơn thuần, không có giá trị đi kèm nào cả

            // Phải lưu thông tin booking trước

            #region -- Booking -- 

            Booking booking = new Booking();
            if (!string.IsNullOrWhiteSpace(agencySelector.Value))
            {
                booking.Agency = AddBookingBLL.AgencyGetById(Int32.Parse(agencySelector.Value));

            }
            booking.CreatedBy = Page.User.Identity as User;
            booking.CreatedDate = DateTime.Now;
            booking.ModifiedDate = DateTime.Now;
            booking.Partner = null;
            booking.Sale = booking.CreatedBy;
            booking.StartDate = Date.Value;



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
            Module.Save(booking, UserIdentity);
            #endregion

            // Sau đó mới có thể lưu thông tin phòng
            // Đối với phòng có baby và child theo phân bố từ trước sẽ thêm child, baby
            GetBookingRoom();
            #region -- Booking Room --
            foreach (BookingRoom room in _bookingRooms)
            {
                room.Book = booking;
                Module.Save(room);

                // Ngoài ra còn phải lưu thông tin khách hàng

                #region -- Thông tin khách hàng, suy ra từ thông tin phòng --

                for (int ii = 1; ii <= room.Booked; ii++)
                {
                    Customer customer = new Customer();
                    customer.BookingRoom = room;
                    customer.Type = CustomerType.Adult;
                    customer.Booking = booking;
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
                    customer.Booking = booking;
                    Module.Save(customer);
                    room.Customers.Add(customer);
                }

                if (room.HasChild)
                {
                    Customer customer = new Customer();
                    customer.BookingRoom = room;
                    customer.Type = CustomerType.Children;
                    customer.Booking = booking;
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
            var url = string.Format("/Modules/Sails/Admin/BookingView.aspx{0}&bi={1}", GetBaseQueryString(), booking.Id);
            var script = string.Format("RefreshParentPage('{0}')", url);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "RefreshParentPage", script, true);

        }
        private void GetBookingRoom()
        {
            _bookingRooms = new ArrayList();
            foreach (RepeaterItem typeItem in rptRoom.Items)
            {
                if (!typeItem.Visible)
                    continue; // Bỏ qua nếu là đối tượng ẩn (ẩn là do không tồn tại loại phòng này)
                HiddenField hiddenTypeId = (HiddenField)typeItem.FindControl("hId");
                Room roomGet = Module.RoomGetById(Convert.ToInt32(hiddenTypeId.Value));
                DropDownList ddlAdults = (DropDownList)typeItem.FindControl("ddlAdults");
                DropDownList ddlChild = (DropDownList)typeItem.FindControl("ddlChild");
                DropDownList ddlBaby = (DropDownList)typeItem.FindControl("ddlBaby");
                double unitprice = 0;
                if (CustomPriceAddBooking)
                {
                    TextBox txtPrice = (TextBox)typeItem.FindControl("txtPrice");
                    unitprice = Convert.ToDouble(txtPrice.Text);
                }
                // Lấy về số phòng (đối với phòng thường) và số người đối với phòng ở ghép
                int adult = Convert.ToInt32(ddlAdults.SelectedValue);
                int child = Convert.ToInt32(ddlChild.SelectedValue);
                int baby = Convert.ToInt32(ddlBaby.SelectedValue);

                // Nếu là phòng ở ghép thì tính theo đơn vị số người chứ không phải số phòng


                // Nếu đủ số phòng trống thì mới thực hiện việc tạo dữ liệu phòng đã book


                var booking = new BookingRoom();
                booking.BookingType = BookingType.Double;
                booking.RoomType = roomGet.RoomType;
                booking.RoomClass = roomGet.RoomClass;
                booking.Room = roomGet;

                // Nếu là phòng cuối thì cho phòng này là phòng shared
                booking.Booked = adult;
                if (adult == 1)
                    booking.IsSingle = true;

                // Các phòng đầu sẽ ghép child/baby vào (nếu có) vì có thể đảm bảo rằng không phải ở ghép
                if (child > 0)
                {
                    booking.HasChild = true;
                }

                if (baby > 0)
                {
                    booking.HasBaby = true;
                }

                if (CustomPriceAddBooking)
                {
                    booking.Total = unitprice;
                }

                _bookingRooms.Add(booking);

            }
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
            //_trips.RemoveAt(4);
            //_trips.RemoveAt(4);
            //_trips.RemoveAt(4);
            //_trips.RemoveAt(4);
            if (string.IsNullOrWhiteSpace(Request["d"]))
            {
                //                _trips.RemoveAt(2);
                //                _trips.RemoveAt(2);
                //                _trips.RemoveAt(2);
                //                _trips.RemoveAt(2);
            }
            ddlTrips.DataSource = _trips; // Danh sách trip luôn được get về trước khi gọi tới hàm bind trips
            ddlTrips.DataTextField = "Name";
            ddlTrips.DataValueField = "Id";
            ddlTrips.DataBind();
        }


        /// <summary>
        /// Kiểm tra xem có còn đủ phòng trống hay không, đồng thời tạo ra dữ liệu booking room
        /// </summary>
        /// <returns></returns>
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
        protected void rptRoom_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Room item = e.Item.DataItem as Room;
            if (item != null)
            {
                #region Name

                using (HyperLink hyperLink_Name = e.Item.FindControl("hyperLink_Name") as HyperLink)
                {
                    if (hyperLink_Name != null)
                    {
                        hyperLink_Name.Text = item.Name;
                        hyperLink_Name.NavigateUrl = "#";
                    }
                }

                #endregion

                #region Edit

                using (HyperLink hyperLinkEdit = e.Item.FindControl("hyperLinkEdit") as HyperLink)
                {
                    if (hyperLinkEdit != null)
                    {
                        hyperLinkEdit.NavigateUrl = string.Format("RoomEdit.aspx?NodeId={0}&SectionId={1}&RoomId={2}",
                                                                  Node.Id, Section.Id, item.Id);
                    }
                }

                #endregion

                #region RoomType

                using (Label label_RoomType = e.Item.FindControl("label_RoomType") as Label)
                {
                    if (label_RoomType != null)
                    {
                        label_RoomType.Text = item.RoomType.Name;
                    }
                }

                #endregion

                #region Room Class

                using (Label label_RoomClass = e.Item.FindControl("label_RoomClass") as Label)
                {
                    if (label_RoomClass != null)
                    {
                        label_RoomClass.Text = item.RoomClass.Name;
                    }
                }
                #endregion

                Label labelCruise = e.Item.FindControl("labelCruise") as Label;
                if (labelCruise != null)
                {
                    try
                    {
                        if (item.Cruise != null)
                        {
                            labelCruise.Text = item.Cruise.Name;
                        }
                    }
                    catch
                    {
                        item.Cruise = null;
                    }
                }

                if (_date != DateTime.MinValue)
                {
                    HtmlTableCell tdAvailable = e.Item.FindControl("tdAvailable") as HtmlTableCell;
                    if (tdAvailable != null)
                    {
                        if (item.IsAvailable)
                        {
                            tdAvailable.InnerText = string.Format("{0} người lớn - {1} trẻ em - {2} trẻ sơ sinh", item.Adult, item.Child, item.Baby);
                        }
                        else
                        {
                            tdAvailable.InnerText = string.Format("{0} người lớn - {1} trẻ em - {2} trẻ sơ sinh", item.Adult, item.Child, item.Baby);
                            tdAvailable.Style[HtmlTextWriterStyle.BackgroundColor] = SailsModule.IMPORTANT;
                        }
                    }
                }
            }
        }

        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "RefreshParentPage", "RefreshParentPage();", true);
        }
    }
}