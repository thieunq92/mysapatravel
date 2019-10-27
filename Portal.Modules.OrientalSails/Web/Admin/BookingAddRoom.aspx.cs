using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.Util;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class BookingAddRoom : HomeChangeRoom
    {
        protected override void Page_Load(object sender, EventArgs e)
        {
            base.Page_Load(sender, e);
            if (!string.IsNullOrWhiteSpace(Request["addBooking"]))
            {
                btnAddBooking.Visible = false;
            }
            else btnAddRoom.Visible = false;
        }

        protected void btnAddRoom_OnClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace("addBooking") && string.IsNullOrWhiteSpace(Request["roomId"]))
            {
                var bookingRooms = new ArrayList();

                Room roomGet = Module.RoomGetById(Convert.ToInt32(hidRoomId.Value));


                // Lấy về số phòng (đối với phòng thường) và số người đối với phòng ở ghép
                //                int adult = Convert.ToInt32(ddlAdults.SelectedValue);
                //                int child = Convert.ToInt32(ddlChild.SelectedValue);
                //                int baby = Convert.ToInt32(ddlBaby.SelectedValue);
                int adult = 0;
                int child = 0;
                int baby = 0;

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
                    booking.Total = 0;
                }
                bookingRooms.Add(booking);
                foreach (BookingRoom room in bookingRooms)
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

                Page.ClientScript.RegisterStartupScript(this.GetType(), "RefreshParentPage", "RefreshParentPage();", true);

            }
        }
    }
}