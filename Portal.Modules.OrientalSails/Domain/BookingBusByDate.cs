using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Domain
{
    public class BookingBusByDate
    {
        public virtual int Id { get; set; }
        public virtual Booking Booking { get; set; }
        public virtual BusByDate BusByDate { get; set; }
    }
}