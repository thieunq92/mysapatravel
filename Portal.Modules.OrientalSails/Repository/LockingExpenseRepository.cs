using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Repository
{
    public class LockingExpenseRepository : RepositoryBase<LockingExpense>
    {
        public LockingExpenseRepository() : base() { }
        public LockingExpenseRepository(ISession session) : base(session) { }

        public IQueryOver<LockingExpense> LockingExpenseGetAllByCriterion(DateTime? date)
        {
            var query = _session.QueryOver<LockingExpense>();
            if (date.HasValue)
            {
                query = query.Where(x => x.Date == date);
            }
            return query;
        }
    }
}