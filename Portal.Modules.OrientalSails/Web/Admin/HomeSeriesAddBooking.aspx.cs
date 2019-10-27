using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal.Modules.OrientalSails.Domain;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    /// <summary>
    /// trang thêm booking series
    /// </summary>
    public partial class HomeSeriesAddBooking : Home
    {
        protected override void btnSearch_OnClick(object sender, EventArgs e)
        {
            Response.Redirect(string.Format("HomeSeriesAddBooking.aspx{0}&cruiseId={1}&date={2}&si={3}", GetBaseQueryString(), _currentCruise.Id, txtStartDate.Text,Request["si"]));
        }
        protected override void rptCruises_OnItemDataBound(object sender, RepeaterItemEventArgs e)
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
                    hplCruise.NavigateUrl = string.Format("HomeSeriesAddBooking.aspx{0}&cruiseId={1}&date={2}", GetBaseQueryString(),
                        cruise.Id, dateStr);
                    if (cruise.Id == _currentCruise.Id) hplCruise.CssClass = "btn cruiseActive";
                }
            }
        }
    }
}