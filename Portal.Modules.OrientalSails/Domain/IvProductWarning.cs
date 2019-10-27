using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Domain
{
    public class IvProductWarning
    {
        public virtual int Id { get; set; }
        public virtual int WarningLimit { get; set; }
        public virtual IvStorage Storage { get; set; }
        public virtual IvProduct Product { get; set; }
    }
}