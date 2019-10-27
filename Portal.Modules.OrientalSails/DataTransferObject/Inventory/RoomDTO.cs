using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.DataTransferObject.Inventory
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RoomTypeId { get; set; }
        public string RoomTypeName { get; set; }
        public int RoomClassId { get; set; }
        public string RoomClassName { get; set; }
        public int CruiseId { get; set; }
        public int Floor { get; set; }
        public int Status { get; set; }
        public int Order { get; set; }

    }
}