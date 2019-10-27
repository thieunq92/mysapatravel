using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Domain
{
    public class ContractValid
    {
        public virtual int Id { get; set; }
        public virtual DateTime? ValidFrom { get; set; }
        public virtual DateTime? ValidTo { get; set; }
        public virtual Contracts Contract { get; set; }
        public virtual IList<ContractPrice> ListContractPrice { get; set; }
    }
}