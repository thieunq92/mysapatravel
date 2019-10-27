using CMS.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Domain
{
    public class AgencyNotes
    {
        public virtual int Id { get; set; }
        public virtual string Note { get; set; }
        public virtual Role Role { get; set; }
        public virtual Agency Agency { get; set; }
    }
}