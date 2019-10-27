using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Repository
{
    public class AgencyContractRepository : RepositoryBase<AgencyContract>
    {
        public AgencyContractRepository() : base() { }

        public AgencyContractRepository(ISession session) : base(session) { }

        public IList<AgencyContract> AgencyContractGetAllByAgency(int agencyId)
        {
            return _session.QueryOver<AgencyContract>()
                .Where(x => x.Agency.Id == agencyId).Future().ToList();
        }

        public AgencyContract AgencyContractGetById(int agencyContractId)
        {
            return _session.QueryOver<AgencyContract>()
                .Where(x => x.Id == agencyContractId).FutureValue().Value;
        }
    }
}