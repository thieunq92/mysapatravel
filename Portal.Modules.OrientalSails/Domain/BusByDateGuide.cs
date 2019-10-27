using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Domain
{
    public class BusByDateGuide
    {
        public virtual int Id { get; set; }
        public virtual BusByDate BusByDate { get; set; }
        public virtual Guide Guide { get; set; }
        public virtual DateTime? ModifiedDate { get; set; }
        public BusByDateGuide(){
            ModifiedDate = DateTime.Now;
        }
    }
}