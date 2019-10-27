using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Domain
{
    public class GoldenDay
    {
        public virtual int Id { get; set; }
        public virtual DateTime Date { get; set; }
        public virtual string Policy { get; set; }
        public virtual Campaign Campaign { get; set; }
    }
}