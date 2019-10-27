using System;
using System.Collections;
using CMS.Core.Domain;
using System.Collections.Generic;

namespace Portal.Modules.OrientalSails.Domain
{
    public class Series
    {
        public virtual int Id { get; set; }
        public virtual string SeriesCode { get; set; }
        public virtual int CutoffDate { get; set; }
        public virtual int NoOfDays { get; set; }
        public virtual Agency Agency { get; set; }
        public virtual AgencyContact Booker { get; set; }
        public virtual IList<Booking> ListBooking { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual DateTime? CreatedDate { get; set; }
        public virtual User LastEditedBy { get; set; }
        public virtual DateTime? LastEditedDate { get; set; }
        public virtual string SpecialRequest { get; set; }
    }
}