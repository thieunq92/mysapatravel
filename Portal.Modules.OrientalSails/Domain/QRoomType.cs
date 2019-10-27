using System;
using System.Text;
using System.Collections.Generic;


namespace Portal.Modules.OrientalSails.Domain
{

    public class QRoomType
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual QCruiseGroup Group { get; set; }
        public virtual QRoomClass QRoomClass { get; set; }

    }
}
