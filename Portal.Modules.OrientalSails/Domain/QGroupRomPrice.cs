using System;
using System.Text;
using System.Collections.Generic;


namespace Portal.Modules.OrientalSails.Domain
{

    public class QGroupRomPrice
    {
        public virtual int Id { get; set; }
        public virtual QQuotation QQuotation { get; set; }
        public virtual QCruiseGroup GroupCruise { get; set; }
        public virtual string AgentLevelCode { get; set; }
        public virtual int Trip { get; set; }
        public virtual string RoomType { get; set; }

        public virtual string RoomDoubleCode { get; set; }
        public virtual string RoomDoubleName { get; set; }
        public virtual decimal? PriceDoubleUsd { get; set; }
        public virtual decimal? PriceDoubleVnd { get; set; }

        public virtual string RoomTwinCode { get; set; }
        public virtual string RoomTwinName { get; set; }
        public virtual decimal? PriceTwinUsd { get; set; }
        public virtual decimal? PriceTwinVnd { get; set; }

        public virtual string RoomExtraCode { get; set; }
        public virtual string RoomExtraName { get; set; }
        public virtual decimal? PriceExtraUsd { get; set; }
        public virtual decimal? PriceExtraVnd { get; set; }

        public virtual string RoomChildCode { get; set; }
        public virtual string RoomChildName { get; set; }
        public virtual decimal? PriceChildUsd { get; set; }
        public virtual decimal? PriceChildVnd { get; set; }
        public virtual bool IsDeleted { get; set; }
    }
}
