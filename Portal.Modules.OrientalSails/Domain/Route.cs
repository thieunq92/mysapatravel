using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Domain
{
    public class Route
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int Group { get; set; }
        public virtual string Way { get; set; }
        public virtual IList<CruiseRoute> ListCruiseRoute { get; set; }
    }
}