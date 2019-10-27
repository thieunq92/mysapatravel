using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.DataTransferObject
{
    public class BusTypeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<BusByDateDTO> ListBusByDateDTO { get;set;}
        public bool HaveBookingNoGroup { get; set; }
        public bool HaveNoBooking { get; set; }
    }
}