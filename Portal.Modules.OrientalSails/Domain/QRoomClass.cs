using System;
using System.Text;
using System.Collections.Generic;


namespace Portal.Modules.OrientalSails.Domain {
    
    public class QRoomClass
    {
        public virtual int Id { get; set; }
        public virtual string Roomclass { get; set; }
        public virtual QCruiseGroup Group { get; set; }
    }
}
