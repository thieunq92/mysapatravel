using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Domain
{
    public class DebtReceivable
    {
        public Agency Agency { get; set; }
        public double TotalReceivableUSD { get; set; }
        public double TotalReceivableVND { get; set; }
    }
}