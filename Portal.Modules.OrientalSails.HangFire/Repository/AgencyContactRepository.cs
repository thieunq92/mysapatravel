using NHibernate;
using Portal.Modules.OrientalSails.HangFire.Domain;
using Portal.Modules.OrientalSails.HangFire.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Linq;

namespace Portal.Modules.OrientalSails.HangFire.Repository
{
    public class AgencyContactRepository : RepositoryBase<AgencyContact>
    {
        public AgencyContactRepository() : base() { }

        public AgencyContactRepository(ISession session) : base(session) { }


        public IList<AgencyContact> AgencyContactGetAllByBirthday()
        {
            return _session.Query<AgencyContact>()
             .Where(x => x.Birthday.Value.Day == DateTime.Today.Day && x.Birthday.Value.Month == DateTime.Today.Month)
             .ToFuture().ToList();
        }
    }
}
