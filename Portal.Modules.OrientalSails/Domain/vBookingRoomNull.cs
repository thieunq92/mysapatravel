using System;
using System.Collections.Generic;
using System.Web;
using Portal.Modules.OrientalSails.Web.Util;

namespace Portal.Modules.OrientalSails.Domain
{
    public class vBookingRoomNull
    {
        public virtual int Id { get; set; }
        //public virtual Room Room { get;  }
        //public virtual Booking Booking { get;  }
        public virtual DateTime StartDate { get; set; }
        public virtual int NumberOfDay { get; set; }
        public virtual DateTime TripDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual int BookingId { get; set; }
        public virtual int RoomId { get; set; }
        public virtual StatusType Status { get; set; }
        public virtual int CruiseId { get; set; }
        public virtual int Adult { get; set; }
        public virtual int Child { get; set; }
        public virtual bool IsCharter { get; set; }
        public virtual bool HasBaby { get; set; }
        public virtual int CustomBookingId { get; set; }
        public virtual double Total { get; set; }

        public virtual int Baby
        {
            get
            {
                if (HasBaby) return 1;
                else return 0;
            }
        }

        public virtual int Pax
        {
            get { return Adult + Child + Baby; }
        }
        public virtual string RTName { get; set; }
        public virtual int RoomTypeId { get; set; }
        public virtual string RCName { get; set; }
        public virtual string RName { get; set; }
        public virtual string AgencyCode { get; set; }
        public virtual string AgencyName { get; set; }
        public virtual string SaleName { get; set; }
        public virtual string TripCode { get; set; }
    }
}