using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.DataTransferObject.Inventory
{
    public class BookingRoomDTO
    {
        public int Id { get; set; }
        public int BookingId { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomTypeName { get; set; }
        public int RoomClassId { get; set; }
        public string RoomClassName { get; set; }
        public int RoomId { get; set; }
        public string Customer { get; set; }
        public int CustomerTotal { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}