using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CMS.Core.Domain;

namespace Portal.Modules.OrientalSails.Domain
{
    public class IvRoleCruise
    {
        public virtual int Id { get; set; }
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
        public virtual Cruise Cruise { get; set; }
    }
}