using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Repository
{
    public class GoldenDayRepository : RepositoryBase<GoldenDay>
    {
        public GoldenDayRepository() { }
        public GoldenDayRepository(ISession session) : base(session) { }
        public IEnumerable<GoldenDay> GoldenDayGetAllByDateRange(DateTime from, DateTime to)
        {
            var query = _session.QueryOver<GoldenDay>().Where(gd => gd.Date >= from && gd.Date <= to);
            return query.Future().ToList();
        }
    }
}