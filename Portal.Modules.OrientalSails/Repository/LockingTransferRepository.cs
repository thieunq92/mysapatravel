using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Repository
{
    public class LockingTransferRepository : RepositoryBase<LockingTransfer>
    {
        public LockingTransferRepository() { }
        public LockingTransferRepository(ISession session) : base(session) { }

        public IQueryOver<LockingTransfer, LockingTransfer> LockingTransferGetAllByCriterion(DateTime? date)
        {
            var query = _session.QueryOver<LockingTransfer>();
            if (date.HasValue)
            {
                query = query.Where(x => x.Date == date);
            }
            return query;
        }
    }
}