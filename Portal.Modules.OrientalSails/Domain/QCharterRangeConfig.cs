using System;
using System.Text;
using System.Collections.Generic;


namespace Portal.Modules.OrientalSails.Domain {
    
    public class QCharterRangeConfig
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int? Charterfrom { get; set; }
        public virtual int? Charterto { get; set; }
    }
}
