using System;
using System.Collections;
using CMS.Core.Domain;
using Portal.Modules.OrientalSails.Web.Util;

namespace Portal.Modules.OrientalSails.Domain
{
    public class BookingHistory
    {
        public virtual int Id { get; set; }
        public virtual Booking Booking { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual User User { get; set; }
        public virtual SailsTrip Trip { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual StatusType Status { get; set; }
        public virtual Agency Agency { get; set; }
        public virtual double Total { get; set; }
        public virtual string TotalCurrency { get; set; }
        public virtual int CabinNumber { get; set; }
        public virtual int CustomerNumber { get; set; }
        public virtual string SpecialRequest { get; set; }
        public virtual Room OldRoom { get; set; }
        public virtual Room NewRoom { get; set; }
        public virtual string PickupAddress { get; set; }
        public virtual int NumberOfPax { get; set; }
        public virtual string CustomerInfo { get; set; }
    }
}
