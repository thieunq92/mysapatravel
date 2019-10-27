using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Domain
{
    public class ContractPrice
    {
        public virtual int Id { get; set; }
        public virtual int TripId { get; set; }
        public virtual int CruiseId { get; set; }
        public virtual int RoomClassId { get; set; }
        public virtual int RoomTypeId { get; set; }
        public virtual bool IsCharter { get; set; }
        public virtual int NumberOfPassenger { get; set; }
        public virtual double Price { get; set; }
        public virtual ContractValid ContractValid { get; set; }
    }
}