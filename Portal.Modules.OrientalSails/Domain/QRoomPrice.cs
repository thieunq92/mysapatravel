using System;
using System.Text;
using System.Collections.Generic;


namespace Portal.Modules.OrientalSails.Domain {
    
    public class QRoomPrice
    {
        public virtual int Id { get; set; }
        public virtual QCruiseGroup Group { get; set; }

        public virtual QQuotation QQuotation { get; set; }
        public virtual QRoomClass QRoomClass { get; set; }
        public virtual QRoomType QRoomType { get; set; }

        public virtual int? Trip { get; set; }
        public virtual decimal? Pricevnd { get; set; }
        public virtual decimal? Priceusd { get; set; }
    }
}
