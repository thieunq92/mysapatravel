using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Portal.Modules.OrientalSails.Domain;
using Portal.Modules.OrientalSails.Web.UI;
using Portal.Modules.OrientalSails.Web.Util;

namespace Portal.Modules.OrientalSails.Web.Admin
{
    public partial class AddRoomToBooking : SailsAdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAdd_OnClick(object sender, EventArgs e)
        {
            var rooms = Module.BookingRoomGetRoomNull();
            var roomAll = Module.RoomGetAll();

            lblTotal.Text = rooms.Count.ToString();
            int updateCount = 0;
            foreach (BookingRoom bkroom in rooms)
            {
                if (bkroom.Book.Status != StatusType.Cancelled)
                {
                    var roomCruises = roomAll.Where(r => r.Cruise == bkroom.Book.Cruise).ToList();
                    var currentDayBookings = Module.GetBookingByDate(bkroom.Book.StartDate, bkroom.Book.Cruise);

                    var listRoomAvailable = new List<Room>();
                    foreach (Room room in roomCruises)
                    {
                        var bk = FindBooking(room, bkroom.Book.StartDate, currentDayBookings);
                        if (bk == null)
                        {
                            if (bkroom.Book.Trip != null && bkroom.Book.Trip.NumberOfDay > 2)
                            {
                                var nextDayBookings = Module.GetBookingByDate(bkroom.Book.StartDate.AddDays(1), bkroom.Book.Cruise);
                                var nextBk = FindBooking(room, bkroom.Book.StartDate.AddDays(1), nextDayBookings);
                                if (nextBk == null)
                                {
                                    listRoomAvailable.Add(room);
                                }
                            }
                            else listRoomAvailable.Add(room);
                        }
                    }
                    var getRandomRoom = listRoomAvailable.FirstOrDefault(r => r.RoomType.Id == bkroom.RoomType.Id);
                    if (getRandomRoom != null)
                    {
                        bkroom.Room = getRandomRoom;
                        Module.Save(bkroom);
                        updateCount++;
                    }
                }
            }
            lblMsg.Text = "Done " + updateCount;
        }
        private Booking FindBooking(Room room, DateTime date, IList<Booking> allBooking)
        {
            Booking booking = null;
            if (allBooking != null)
            {
                foreach (Booking vBookingRoom in allBooking)
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

        protected void btnGet_OnClick(object sender, EventArgs e)
        {
            var bookings = Module.BookingGetAllFromToDay();
            lblTotal.Text = bookings.Count + " booking";
        }

        protected void btnAddRoom_OnClick(object sender, EventArgs e)
        {
            var bookings = Module.BookingGetAllFromToDay();
            var roomAll = Module.RoomGetAll();
            foreach (Booking booking in bookings)
            {
                if (booking.BookingRooms.Count > 0)
                {
                    var roomCruises = roomAll.Where(r => r.Cruise == booking.Cruise);
                    var roomNulls = booking.BookingRooms.Where(r => r.Room == null);
                    IEnumerable<BookingRoom> bookingRooms = roomNulls as IList<BookingRoom> ?? roomNulls.ToList();
                    if (bookingRooms.Any())
                    {
                        var currentDayBookings = Module.GetBookingByDate(booking.StartDate, booking.Cruise);

                        var listRoomAvailable = new List<Room>();
                        foreach (Room room in roomCruises)
                        {
                            var bk = FindBooking(room, booking.StartDate, currentDayBookings);
                            if (bk == null)
                            {
                                if (booking.Trip != null && booking.Trip.NumberOfDay > 2)
                                {
                                    var nextDayBookings = Module.GetBookingByDate(booking.StartDate.AddDays(1), booking.Cruise);
                                    var nextBk = FindBooking(room, booking.StartDate.AddDays(1), nextDayBookings);
                                    if (nextBk == null)
                                    {
                                        listRoomAvailable.Add(room);
                                    }
                                }
                                else listRoomAvailable.Add(room);
                            }
                        }
                        foreach (BookingRoom bookingRoom in bookingRooms)
                        {
                            var bkRoom = Module.BookingRoomGetById(bookingRoom.Id);
                            var getRandomRoom = listRoomAvailable.FirstOrDefault(r => r.RoomType.Id == bookingRoom.RoomType.Id);
                            if (getRandomRoom != null)
                            {
                                bookingRoom.Room = getRandomRoom;
                                Module.Update(bookingRoom);
                                listRoomAvailable.Remove(getRandomRoom);
                            }
                        }
                    }
                }
            }
            var roomNull = 0;
            foreach (Booking booking in bookings)
            {
                if (booking.BookingRooms != null)
                {
                    foreach (BookingRoom bookingRoom in booking.BookingRooms)
                    {
                        if (bookingRoom.Room == null)
                        {
                            roomNull++;
                        }
                    }
                }
            }
            lblMsg.Text = "roomNull " + roomNull;

        }
        public IList<Booking> GetBookingByDate(DateTime date, Cruise cruise, IList<Booking> bookings)
        {
            bookings = bookings.Where(r => (r.EndDate > date && r.StartDate > date.AddDays(-1) && r.StartDate < date.AddDays(1)) || (r.StartDate < date && r.EndDate > date)).ToList();
            bookings = bookings.Where(r => r.Cruise.Id == cruise.Id).ToList();
            bookings = bookings.Where(r => r.Status != StatusType.Cancelled).ToList();

            return bookings.ToList();
        }

        protected void btnAddRoomNull_OnClick(object sender, EventArgs e)
        {
            var bkRooms = Module.GetBookingRoomNull();
            var roomAll = Module.RoomGetAll();

            lblTotal.Text = bkRooms.Count.ToString();
            var updateCount = 0;
            foreach (vBookingRoomNull vBookingRoomNull in bkRooms)
            {
                if (vBookingRoomNull.Status != StatusType.Cancelled)
                {
                    var roomCruises = roomAll.Where(r => r.Cruise.Id == vBookingRoomNull.CruiseId).ToList();
                    var currentDayBookings = Module.GetBookingByDate(vBookingRoomNull.StartDate, vBookingRoomNull.CruiseId);

                    var listRoomAvailable = new List<Room>();
                    foreach (Room room in roomCruises)
                    {
                        var bk = FindBooking(room, vBookingRoomNull.StartDate, currentDayBookings);
                        if (bk == null)
                        {
                            if (vBookingRoomNull.NumberOfDay > 2)
                            {
                                var nextDate = vBookingRoomNull.StartDate;
                                var nextDayBookings = Module.GetBookingByDate(nextDate.AddDays(1), vBookingRoomNull.CruiseId);
                                var nextBk = FindBooking(room, nextDate.AddDays(1), nextDayBookings);
                                if (nextBk == null)
                                {
                                    listRoomAvailable.Add(room);
                                }
                            }
                            else listRoomAvailable.Add(room);
                        }
                    }
                    var getRandomRoom = listRoomAvailable.FirstOrDefault(r => r.RoomType.Id == vBookingRoomNull.RoomTypeId);
                    if (getRandomRoom != null)
                    {
                        var bkroom = Module.BookingRoomGetById(vBookingRoomNull.Id);
                        bkroom.Room = getRandomRoom;
                        Module.Update(bkroom);
                        updateCount++;
                    }
                }
            }
            lblMsg.Text = "Done " + updateCount;

        }

        protected void btnGetTotalNull_OnClick(object sender, EventArgs e)
        {
            var bkRooms = Module.GetBookingRoomNull();
            lblTotal.Text = bkRooms.Count + " bkRooms";
        }

        protected void btnDic_OnClick(object sender, EventArgs e)
        {
            var bookings = Module.BookingGetAllFromToDay();
            var roomAll = Module.RoomGetAll();
            var dic = new Dictionary<DateCruise, IList<Booking>>();
            foreach (Booking booking in bookings)
            {
                var dateCruise = new DateCruise() { StartDate = booking.StartDate, Cruise = booking.Cruise };
                if (dic.ContainsKey(dateCruise))
                {
                    dic[dateCruise].Add(booking);
                }
                else
                {
                    dic.Add(dateCruise, new List<Booking>() { booking });
                }
            }
            lblTotal.Text = dic.Count + " dic";
            int updateCount = 0;
            foreach (KeyValuePair<DateCruise, IList<Booking>> keyValuePair in dic)
            {
                var dateCruise = keyValuePair.Key;
                var listBooking = keyValuePair.Value;
                var roomCruises = roomAll.Where(r => r.Cruise == dateCruise.Cruise);
                var currentDayBookings = Module.GetBookingByDate(dateCruise.StartDate, dateCruise.Cruise);

                var listRoomAvailable = new List<Room>();
                foreach (Room room in roomCruises)
                {
                    var bk = FindBooking(room, dateCruise.StartDate, currentDayBookings);
                    if (bk == null)
                    {
                        var nextDayBookings = Module.GetBookingByDate(dateCruise.StartDate.AddDays(1), dateCruise.Cruise);
                        var nextBk = FindBooking(room, dateCruise.StartDate.AddDays(1), nextDayBookings);
                        if (nextBk == null)
                        {
                            room.IsAvailable3D = true;
                            listRoomAvailable.Add(room);
                        }
                    }
                    else listRoomAvailable.Add(room);
                }
                foreach (Booking booking in listBooking)
                {
                    if (booking.BookingRooms.Count > 0)
                    {
                        foreach (BookingRoom bookingRoom in booking.BookingRooms)
                        {
                            if (bookingRoom.Room == null && listRoomAvailable.Count > 0)
                            {
                                Room getRandomRoom = null;
                                if (booking.Trip.NumberOfDay > 2)
                                    getRandomRoom = listRoomAvailable.FirstOrDefault(r => r.RoomType.Id == bookingRoom.RoomType.Id && r.IsAvailable3D);
                                else getRandomRoom = listRoomAvailable.FirstOrDefault(r => r.RoomType.Id == bookingRoom.RoomType.Id);

                                if (getRandomRoom != null)
                                {
                                    bookingRoom.Room = getRandomRoom;
                                    Module.Update(bookingRoom);
                                    listRoomAvailable.Remove(getRandomRoom);
                                    updateCount++;
                                }
                            }
                        }
                    }
                }
            }
            lblMsg.Text = updateCount + " done";
        }
    }


    public class DateCruise
    {
        public DateTime StartDate { get; set; }
        public Cruise Cruise { get; set; }
    }
}