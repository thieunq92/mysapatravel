using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Domain
{
    public class Transfer
    {
        public virtual int Id { get; set; }
        public virtual int Group { get; set; }
        public virtual Agency Driver { get; set; }
        public virtual Agency Guide { get; set; }
        public virtual Agency Supplier { get; set; }
        public virtual DateTime? Date { get; set; }
        public virtual Route Route { get; set; }
        public virtual BusType BusType { get; set; }
    }
}