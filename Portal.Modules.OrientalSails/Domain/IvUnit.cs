using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Domain
{
    public class IvUnit
    {
        public virtual int Id { get; set; }
        public virtual IvUnit Parent { get; set; }
        public virtual string Name { get; set; }
        public virtual string NameTree { get; set; }
        public virtual int Rate { get; set; }
        public virtual string Math { get; set; }
        public virtual string Note { get; set; }
        public virtual IList<IvUnit> ListChild { get; set; }

    }

}