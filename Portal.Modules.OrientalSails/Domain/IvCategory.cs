using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Domain
{
    public class IvCategory
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string NameTree { get; set; }
        public virtual IvCategory Parent { get; set; }
        public virtual string Note { get; set; }
    }
}