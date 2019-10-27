using System;
using System.Text;
using System.Collections.Generic;


namespace Portal.Modules.OrientalSails.Domain {
    
    public class QCharterPrice {
        public virtual int Id { get; set; }
        public virtual QQuotation QQuotation { get; set; }
        public virtual QCruiseGroup GroupCruise { get; set; }
        public virtual Cruise Cruise { get; set; }
        public virtual string AgentLevelCode { get; set; }
        public virtual int Trip { get; set; }
        public virtual string Validname { get; set; }
        public virtual int? Validfrom { get; set; }
        public virtual int? Validto { get; set; }
        public virtual decimal? Priceusd { get; set; }
        public virtual decimal? Pricevnd { get; set; }
        public virtual bool IsDeleted { get; set; }
    }
}
