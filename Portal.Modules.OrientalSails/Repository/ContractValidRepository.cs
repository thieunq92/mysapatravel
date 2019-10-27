using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Repository
{
    public class ContractValidRepository:RepositoryBase<ContractValid>
    {
        public ContractValidRepository() { }
        public ContractValidRepository(ISession session) : base(session) { }

    }
}