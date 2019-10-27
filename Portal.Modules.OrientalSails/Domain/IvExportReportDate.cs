using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Domain
{
    public class IvExportReportDate
    {
        public virtual DateTime ExportDate { get; set; }
        public virtual double Total { get; set; }
        public virtual double Pay { get; set; }
        public virtual double AverageCost { get; set; }
        public virtual int TotalCustomer { get; set; }
    }
}