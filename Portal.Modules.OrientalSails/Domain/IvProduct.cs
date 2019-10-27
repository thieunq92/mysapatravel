using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Domain
{
    public class IvProduct
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string Code { get; set; }
        public virtual string Note { get; set; }
        public virtual IvCategory Category { get; set; }
        public virtual bool InRoomService { get; set; }
        public virtual int SumQuantity { get; set; }

        public virtual double SumTotal { get; set; }
        public virtual bool Deleted { get; set; }
        public virtual DateTime? CreatedDate { get; set; }
        public virtual IvUnit Unit { get; set; }
        public virtual int WarningLimit { get; set; }
        public virtual double NumberInStock { get; set; }
        public virtual bool IsTool { get; set; }
    }
}