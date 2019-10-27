using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Repository
{
    public class ContractRepository : RepositoryBase<Contracts>
    {
        public ContractRepository() : base() {}
        public ContractRepository(ISession session) : base(session) { }
        public IList<Contracts> ContractGetAll()
        {
            return _session.QueryOver<Contracts>().Future().ToList();
        }

        public Contracts ContractGetById(int contractId)
        {
            return _session.QueryOver<Contracts>().Where(x => x.Id == contractId).FutureValue().Value;
        }
    }
}