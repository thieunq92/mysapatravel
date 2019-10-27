using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Domain
{
    public class IvInStock
    {
        public virtual string RID { get; set; }
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
        public virtual int CategoryId { get; set; }
        public virtual string CategoryName { get; set; }
        public virtual int UnitId { get; set; }
        public virtual string UnitName { get; set; }
        public virtual int StorageId { get; set; }
        public virtual int CruiseId { get; set; }
        public virtual string StorageName { get; set; }
        public virtual int WarningLimit { get; set; }
        public virtual double Quantity { get; set; }
        public virtual bool IsTool { get; set; }
    }
}