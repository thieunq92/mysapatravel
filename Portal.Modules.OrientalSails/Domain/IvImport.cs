using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMS.Core.Domain;

namespace Portal.Modules.OrientalSails.Domain
{
    public class IvImport
    {
        public virtual int Id { get; set; }

        public virtual bool Deleted { get; set; }

        public virtual string Name { get; set; }

        public virtual User CreatedBy { get; set; }

        public virtual DateTime CreatedDate { get; set; }

        public virtual User ModifiedBy { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }


        public virtual string Code { get; set; }

        public virtual double Total { get; set; }

        public virtual string Detail { get; set; }

        public virtual Agency Agency { get; set; }

        public virtual DateTime ImportDate { get; set; }

        public virtual string ImportedBy { get; set; }
        public virtual IvStorage Storage { get; set; }
        public virtual Cruise Cruise { get; set; }

    }
}