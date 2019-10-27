using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Domain
{
    public class LockingExpense
    {
        public virtual int Id { get; set; }
        public virtual DateTime? Date { get; set; }
    }
}