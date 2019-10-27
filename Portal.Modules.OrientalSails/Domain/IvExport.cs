using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMS.Core.Domain;
using Portal.Modules.OrientalSails.Web.Util;

namespace Portal.Modules.OrientalSails.Domain
{
    public class IvExport
    {
        public virtual int Id { get; set; }
        public virtual bool Deleted { get; set; }
        public virtual string Name { get; set; }

        public virtual User CreatedBy { get; set; }

        public virtual DateTime CreatedDate { get; set; }

        public virtual User ModifiedBy { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }

        public virtual double Total { get; set; }
        public virtual double Pay { get; set; }
        public virtual double AverageCost { get; set; }
        public virtual int TotalCustomer { get; set; }

        public virtual string Detail { get; set; }

        public virtual string Code { get; set; }
        public virtual string CustomerName { get; set; }

        public virtual string ExportedBy { get; set; }

        public virtual BookingRoom BookingRoom { get; set; }
        public virtual Room Room { get; set; }
        public virtual IvExportType Status { get; set; }

        public virtual DateTime ExportDate { get; set; }
        public virtual IvStorage Storage { get; set; }
        public virtual Cruise Cruise { get; set; }
        //public virtual List<IvProductExport> ProductExports { get; set; }
        public virtual bool IsDebt { get; set; }
        public virtual string Agency { get; set; }
    }
}