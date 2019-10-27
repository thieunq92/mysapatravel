using System;
using System.Text;
using System.Collections.Generic;


namespace Portal.Modules.OrientalSails.Domain {
    
    public class QCruiseCharterRange
    {
        public virtual int Id { get; set; }
        public virtual Cruise Cruise { get; set; }
        public virtual QCruiseGroup Group { get; set; }
        public virtual QCharterRangeConfig CharterRangeConfig { get; set; }
    }
}
