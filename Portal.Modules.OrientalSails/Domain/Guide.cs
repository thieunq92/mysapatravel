using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Domain
{
    public class Guide : Agency
    {
        public virtual ICollection<BusByDateGuide> BusByDatesGuides { get; set; }
    }
}