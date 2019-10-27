using System;
using System.Text;
using System.Collections.Generic;
using CMS.Core.Domain;


namespace Portal.Modules.OrientalSails.Domain {
    
    public class QQuotation {
        public virtual int Id { get; set; }
        public virtual DateTime Validfrom { get; set; }
        public virtual DateTime Validto { get; set; }
        public virtual QCruiseGroup GroupCruise { get; set; }
        public virtual bool? Enable { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual User ModifiedBy { get; set; }
        public virtual DateTime ModifiedDate { get; set; }
    }
}
