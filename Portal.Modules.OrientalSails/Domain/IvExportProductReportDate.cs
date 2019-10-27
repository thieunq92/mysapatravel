using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Domain
{
    public class IvExportProductReportDate
    {
        public virtual string RID { get; set; }
        public virtual string Name { get; set; }
        public virtual string StorageName { get; set; }
        public virtual DateTime ExportDate { get; set; }
        public virtual double Total { get; set; }
        public virtual string Unit { get; set; }
        public virtual int CruiseId { get; set; }
        public virtual bool IsDebt { get; set; }
    }
}