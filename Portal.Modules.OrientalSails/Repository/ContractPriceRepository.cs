using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Repository
{
    public class ContractPriceRepository:RepositoryBase<ContractPrice>
    {
        public ContractPriceRepository() { }
        public ContractPriceRepository(ISession session) : base(session) { }
    }
}