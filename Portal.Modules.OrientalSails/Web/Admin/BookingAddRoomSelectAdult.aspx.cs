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
using NHibernate.Criterion;
using Portal.Modules.OrientalSails.BusinessLogic;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;
using Portal.Modules.OrientalSails.Web.Util;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class BookingAddRoomSelectAdult : SailsAdminBase
    {
        private DateTime? _date;
        private RoomClass _roomClass;
        private SailsTrip _trip;
        private IList _bookingRooms;
        private IList _policies;
        private Booking _booking;


        protected void Page_Load(object sender, EventArgs e)
        {
            _booking = Module.GetById<Booking>(Convert.ToInt32(Request["bookingId"]));
            if (!IsPostBack)
            {
                LoadInfo();
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
            var ids = new List<int>();
            var strIds = Request["roomIds"].Split(',');
            foreach (string s in strIds)
            {
                if (!string.IsNullOrEmpty(s) && !ids.Contains(Convert.ToInt32(s))) ids.Add(Convert.ToInt32(s));
            }
            var allRoom = Module.RoomGetAll2(_booking.Cruise);
            var rooms = allRoom.Where(r => ids.Contains(r.Id)).ToList();
            bool isTrip3D = _booking.Trip.NumberOfDay == 3;
            var checkAvailable = Module.CheckExistAddRoom(rooms, _booking.Cruise, _booking.StartDate, isTrip3D);
            if (checkAvailable)
            {
                rptRoom.DataSource = rooms;
                rptRoom.DataBind();
            }
            else
            {
                ShowErrors("Phòng đã được chọn cho booking khác, vui lòng chọn phòng khác");
                btnSave.Visible = false;
            }

            //---------------------------------------------------
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            //2. Lưu thông tin phòng như thế nào
            // Dùng vòng lặp lưu thông tin đơn thuần, không có giá trị đi kèm nào cả

            // Phải lưu thông tin booking trước

            #region -- Booking -- 



            #endregion

            // Sau đó mới có thể lưu thông tin phòng
            // Đối với phòng có baby và child theo phân bố từ trước sẽ thêm child, baby
            GetBookingRoom();

            #region -- Booking Room --

            foreach (BookingRoom room in _bookingRooms)
            {
                room.Book = _booking;
                Module.Save(room);

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

            #region -- Track thêm mới --

            BookingTrack track = new BookingTrack();
            track.Booking = _booking;
            track.ModifiedDate = DateTime.Now;
            track.User = UserIdentity;
            Module.SaveOrUpdate(track);

            BookingChanged change = new BookingChanged();
            change.Action = BookingAction.Created;
            change.Parameter = string.Format("{0}", _booking.Total);
            change.Track = track;
            Module.SaveOrUpdate(change);

            #endregion
            Page.ClientScript.RegisterStartupScript(this.GetType(), "RefreshParentPage", "RefreshParentPage();", true);

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
                            tdAvailable.InnerText = string.Format("{0} người lớn - {1} trẻ em - {2} trẻ sơ sinh",
                                item.Adult, item.Child, item.Baby);
                        }
                        else
                        {
                            tdAvailable.InnerText = string.Format("{0} người lớn - {1} trẻ em - {2} trẻ sơ sinh",
                                item.Adult, item.Child, item.Baby);
                            tdAvailable.Style[HtmlTextWriterStyle.BackgroundColor] = SailsModule.IMPORTANT;
                        }
                    }
                }
            }
        }
    }
}