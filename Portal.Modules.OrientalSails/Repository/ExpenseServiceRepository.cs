using NHibernate;
using Portal.Modules.OrientalSails.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Portal.Modules.OrientalSails.Repository
{
    public class ExpenseServiceRepository:RepositoryBase<ExpenseService>
    {
        public ExpenseServiceRepository() : base() { }
        public ExpenseServiceRepository(ISession session) : base(session) { }

        public IQueryOver<ExpenseService, ExpenseService> ExpenseServiceGetAllByCriterion(int expenseIdRef)
        {
            var query = _session.QueryOver<ExpenseService>();
            if (expenseIdRef != -1)
            {
                query = query.Where(x => x.ExpenseIdRef == expenseIdRef);
            }
            return query;
        }

        public ExpenseService ExpenseServiceGetByExpenseId(int expenseId)
        {
            return _session.QueryOver<ExpenseService>().Where(x => x.ExpenseIdRef == expenseId).FutureValue().Value;
        }
    }
}