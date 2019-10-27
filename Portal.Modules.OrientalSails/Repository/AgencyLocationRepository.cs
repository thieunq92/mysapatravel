using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Repository
{
    public class AgencyLocationRepository : RepositoryBase<AgencyLocation>
    {
        public AgencyLocationRepository() : base() { }

        public AgencyLocationRepository(ISession session)
            : base(session)
        {
        }

        public IList<AgencyLocation> AgencyLocationGetAll()
        {
            return _session.QueryOver<AgencyLocation>().Where(x => x.Deleted == false)
                .Future()
                .ToList();
        }
    }
}