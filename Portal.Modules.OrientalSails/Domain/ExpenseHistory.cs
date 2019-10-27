using CMS.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Domain
{
    public class ExpenseHistory
    {
        public virtual int Id { get; set; }
        public virtual string ColumnName { get; set; }
        public virtual string OldValue { get; set; }
        public virtual string NewValue { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual DateTime? CreatedDate { get; set; }
        public virtual Expense Expense { get; set; }
    }
}