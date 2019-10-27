using System;
using System.Collections.Generic;
using System.Web;
using CMS.Core.Domain;

namespace Portal.Modules.OrientalSails.Domain
{
    public class TransactionGroup
    {

        public virtual int Id { get; set; }
        public virtual double USDAmount { get; set; }
        public virtual double VNDAmount { get; set; }
        public virtual string Note { get; set; }
        public virtual DateTime CreatedDate { get; set; }
        public virtual User CreatedBy { get; set; }
    }
}