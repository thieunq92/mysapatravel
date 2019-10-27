using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Repository
{
    public class CostTypeRepository : RepositoryBase<CostType>
    {
        public CostTypeRepository() : base() { }
        public CostTypeRepository(ISession session) : base(session) { }

        public IQueryOver<CostType, CostType> CostTypeGetAll()
        {
            return _session.QueryOver<CostType>();
        }
    }
}