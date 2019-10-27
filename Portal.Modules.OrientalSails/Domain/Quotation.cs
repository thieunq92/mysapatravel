using CMS.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Domain
{
    public class Quotation
    {
        public virtual int Id { get; set; }
        public virtual DateTime? CreatedDate { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual DateTime? ValidFrom { get; set; }
        public virtual DateTime? ValidTo { get; set; }
        public virtual string Name { get; set; }
        public virtual int Currency { get; set; }
        public virtual IList<QuotationPrice> ListQuotationPrice { get; set; }
        public virtual IList<AgencyContract> ListAgencyContract { get; set; }
    }
}