using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Domain
{
    public class BusByDateAgency
    {
        public virtual int Id { get; set; }
        public virtual BusByDate BusByDate { get; set; }
        public virtual Agency Agency { get; set; }
        public virtual DateTime ModifiedDate { get; set; }
        public BusByDateAgency()
        {
            ModifiedDate = DateTime.Now;
        }
    }
}