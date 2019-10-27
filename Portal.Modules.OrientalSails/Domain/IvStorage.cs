using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Domain
{
    public class IvStorage
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string NameTree { get; set; }
        public virtual IvStorage Parent { get; set; }
        public virtual string Note { get; set; }
        public virtual Cruise Cruise { get; set; }
        public virtual bool IsInventoryTracking { get; set; }
    }
}