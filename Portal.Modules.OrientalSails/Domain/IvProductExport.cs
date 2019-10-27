using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Domain
{
    public class IvProductExport
    {
        public virtual int Id { get; set; }

        public virtual IvProduct Product { get; set; }

        public virtual int Quantity { get; set; }

        public virtual IvExport Export { get; set; }
        public virtual IvStorage Storage { get; set; }

        public virtual double UnitPrice { get; set; }
        public virtual int Discount { get; set; }
        public virtual int DiscountType { get; set; }

        public virtual double Total { get; set; }
        public virtual IvUnit Unit { get; set; }
        public virtual double QuanityRateParentUnit { get; set; }
    }
}