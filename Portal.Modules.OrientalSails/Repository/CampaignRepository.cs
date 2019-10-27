using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Repository
{
    public class CampaignRepository : RepositoryBase<Campaign>
    {
        public CampaignRepository() { }
        public CampaignRepository(ISession session)
            : base(session)
        {

        }

        public IEnumerable<Campaign> CampaignGetAll()
        {
            var query = _session.QueryOver<Campaign>();
            return query.Future().ToList();
        }

        public IEnumerable<Campaign> CampaignGetAllPaged(int pageSize, int pageIndex, out int count)
        {
            var query = _session.QueryOver<Campaign>();
            count = query.RowCount();
            return query.Skip(pageSize * pageIndex).Take(pageSize).Future().ToList();
        }

        public Campaign CampaignGetByMonthAndYear(int month, int year)
        {
            var query = _session.QueryOver<Campaign>();
            query = query.Where(x => x.Month == month && x.Year == year);
            return query.FutureValue().Value;
        }
    }
}